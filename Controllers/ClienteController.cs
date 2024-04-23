using Microsoft.AspNetCore.Mvc;
using ModuleAPI.Context;
using ModuleAPI.Entities;
using System.Text.Json;

namespace ModuleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly PrincipalContext _context;

        public ClienteController(PrincipalContext context){
            _context = context;
        }


        [HttpPost("AdicionarCliente")]
        public IActionResult AdicionarCliente(Cliente cliente){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            _context.Add(cliente);
            _context.SaveChanges();
            return Ok($"O contato do {cliente.Nome} foi salvo com sucesso!");
            // return CreatedAtAction(nameof(ConsultarListaClientesPorId), new {id = cliente.Id}, cliente)
        }

        [HttpGet("ConsultarListaClientes")]
        public IActionResult ConsultarListaClientes(){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            List<Cliente> clientes = _context.Clientes.ToList();
            return Ok(JsonSerializer.Serialize<List<Cliente>>(clientes));
        }

        [HttpGet("ConsultarListaClientesPorId")]
        public IActionResult ConsultarListaClientesPorId(int id){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            Cliente cliente = _context.Clientes.Find(id);

            if (cliente is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize<Cliente>(cliente));
        }

        [HttpGet("ConsultarListaClientesPorNome")]
        public IActionResult ConsultarListaClientesPorNome(string Nome){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            List<Cliente> clientes = _context.Clientes.Where(x => x.Nome.Contains(Nome)).ToList();

            if (clientes is null || clientes.Count == 0)
                return NotFound();

            return Ok(JsonSerializer.Serialize<List<Cliente>>(clientes));
        }

        [HttpPut("RetificarCliente")]
        public IActionResult RetificarCliente(int id, Cliente clienteAlterado){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            Cliente cliente = _context.Clientes.Find(id);

            if (cliente is null)
                return NotFound();

            cliente.Nome = clienteAlterado.Nome;
            cliente.Documento = clienteAlterado.Documento;
            cliente.Ativo = clienteAlterado.Ativo;
            _context.Clientes.Update(cliente);
            _context.SaveChanges();

            return Ok(JsonSerializer.Serialize<Cliente>(cliente));
        }

        [HttpDelete("DeletarCliente")]
        public IActionResult DeletarCliente(int id){
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            Cliente cliente = _context.Clientes.Find(id);

            if (cliente is null)
                return NotFound();
            
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok($"Cliente de Id {id} deletado com sucesso!");
        }
    }
}