using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModuleAPI.Context;
using ModuleAPI.Entities;
using ModuleAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ModuleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly PrincipalContext _context;

        public AutenticacaoController(PrincipalContext context)
        {
            _context = context;
        }

        // Deixar Método Centralizado em uma Classe de Utilitário
        private Mensagem ValidarToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                
                // Entender Parâmetros de Validação
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = System.TimeSpan.Zero
                };

                ClaimsPrincipal tokenHandler = handler.ValidateToken(token, validationParameters, out _);

                string usuarioLogin = tokenHandler.Identity.Name;
                string usuarioSenha = (tokenHandler.Identity as ClaimsIdentity).Claims.FirstOrDefault(x => x.Type.Equals("Secret"))?.Value;

                Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Login.Equals(usuarioLogin) && x.Senha.Equals(usuarioSenha));

                if (usuario is null)
                    return new Mensagem("Dados inválidos na assinatura do token.");

                return Mensagem.MensagemSucesso;
            }
            catch (Exception ex)
            {
                return new Mensagem(ex.Message);
            }
        }  
        
        private Mensagem GerarToken(Usuario usuario, out string tokenString)
        {
            tokenString = string.Empty;

            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                // TEMPORÁRIO: A chave privada deve ser armazenada de forma segura
                SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)), SecurityAlgorithms.HmacSha256);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    SigningCredentials = credentials,
                    Expires = System.DateTime.Now.AddHours(8),

                    Subject = new System.Security.Claims.ClaimsIdentity(new System.Security.Claims.Claim[]
                    {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.Login),
                    new System.Security.Claims.Claim("Secret", usuario.Senha)
                    }),
                };

                SecurityToken token = handler.CreateToken(tokenDescriptor);
                tokenString = handler.WriteToken(token);

                return Mensagem.MensagemSucesso;
            }
            catch (Exception ex)
            {
                return new Mensagem(ex.Message);
            }
        }

        [HttpGet("AutenticarUsuario")]
        public IActionResult AutenticarUsuario(string login, string senha)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Login.Equals(login));

            if (usuario is null || !usuario.Senha.Equals(senha))
                return Unauthorized("Login e/ou Senha Incorreto(s)");

            Mensagem mensagem = GerarToken(usuario, out string token);
            if (!mensagem.Sucesso)
                return BadRequest(mensagem.Descricao);

            //Mensagem mensagem = ValidarToken(tokenString);
            //if (!mensagem.Sucesso)
            //    return BadRequest(mensagem.Descricao);

            return Ok(token);
        }

        [HttpPost("CadastrarUsuario")]
        public IActionResult AdicionarCliente(Usuario usuario)
        {
            try
            {
                usuario.ValidateAndThrow();

                _context.Add(usuario);
                _context.SaveChanges();

                Mensagem mensagem = GerarToken(usuario, out string token);
                if (!mensagem.Sucesso)
                    return BadRequest(mensagem.Descricao);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
