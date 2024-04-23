using Microsoft.AspNetCore.Mvc;
using ModuleAPI.Context;
using ModuleAPI.Entities;

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

        [HttpGet("AutenticarUsuario")]
        public IActionResult AutenticarUsuario(string login, string senha)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Login.Equals(login));

            if (usuario is null || !usuario.Senha.Equals(senha))
                return Unauthorized("Login e/ou Senha Incorreto(s)");

            return Ok("Usuário Autenticado");
        }

        [HttpPost("CadastrarUsuario")]
        public IActionResult AdicionarCliente(Usuario usuario)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            try
            {
                usuario.ValidateAndThrow();

                _context.Add(usuario);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
