using Microsoft.AspNetCore.Mvc;
using ModuleAPI.Context;
using ModuleAPI.Entities;
using System.Text.Json;

namespace ModuleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreinoController : ControllerBase
    {
        private readonly PrincipalContext _context;

        public TreinoController(PrincipalContext context)
        {
            _context = context;
        }

        #region Exercícios
        [HttpGet("ConsultarExercicios")]
        public IActionResult ConsultarExercicios()
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
            
            List<Exercicio> exercicios = _context.Exercicios.ToList();

            return Ok(JsonSerializer.Serialize<List<Exercicio>>(exercicios));
        }

        [HttpPost("AdicionarExercicio")]
        public IActionResult AdicionarExercicio(Exercicio exercicio)
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");

            _context.Add(exercicio);
            _context.SaveChanges();

            return Ok("Exercício Adicionado");
        }

        [HttpPut("AlterarExercicio")]
        public IActionResult AlterarExercicio()
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");

            return Ok("Exercício Retificado");
        }

        [HttpDelete("DeletarExercicio")]
        public IActionResult DeletarExercicio()
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");

            return Ok("Exercício Deletado");
        }

        #endregion

        #region Ficha Usuário

        [HttpPost("CriarFicha")]
        public IActionResult CriarFicha(string FichaNome, int UsuarioId)
        {
            Usuario usuario = _context.Usuarios.Find(UsuarioId);

            if (usuario is null)
                return NotFound("Usuário não encontrado");

            _context.Fichas.Add(new Ficha { Nome = FichaNome, UsuarioId = usuario.Id });
            _context.SaveChanges();

            return Ok("Ficha Criada");
        }

        [HttpPost("AdicionarExercicioFicha")]
        public IActionResult AdicionarExercicioFicha(int FichaId, int ExercicioId, int Series, int Repeticoes, double Carga, string Observacao)
        {
            Ficha ficha = _context.Fichas.Find(FichaId);
            Exercicio exercicio = _context.Exercicios.Find(ExercicioId);

            if (ficha is null || exercicio is null)
                return NotFound("Ficha ou Exercício não encontrado");

            if (ficha.Exercicios.FirstOrDefault(x => x.ExercicioId == ExercicioId) != null)
                return BadRequest("Exercício já adicionado à ficha");

            ExercicioAplicado exercicioAplicado = new ExercicioAplicado();

            exercicioAplicado.FichaId = ficha.Id;
            exercicioAplicado.ExercicioId = exercicio.Id;
            exercicioAplicado.Series = Series;
            exercicioAplicado.Repeticoes = Repeticoes;
            exercicioAplicado.Carga = Carga;
            exercicioAplicado.Observacao = Observacao;

            _context.ExerciciosAplicados.Add(exercicioAplicado);
            _context.SaveChanges();

            return Ok("Exercício Adicionado à Ficha");
        }   

        [HttpPost("ConsultarFichas")]
        public IActionResult ConsultarFichas(int UsuarioId)
        {
            List<Ficha> fichas = _context.Fichas.Where(x => x.UsuarioId == UsuarioId).ToList();

            foreach(Ficha ficha in fichas)
                ficha.Exercicios = _context.ExerciciosAplicados.Where(x => x.FichaId == ficha.Id).ToArray();

            return Ok(JsonSerializer.Serialize<List<Ficha>>(fichas));
        }   

        #endregion

    }
}
