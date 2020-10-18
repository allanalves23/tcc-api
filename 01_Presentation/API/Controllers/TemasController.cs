using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemasController : ControllerBase
    {
        public ITemaService _temaService;

        public TemasController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        [HttpGet]
        public IActionResult GetTemas(string termo, int? skip = 0, int? take = 10) => 
            Ok(_temaService.Obter(termo, skip, take).Select(item => new TemaModel(item)));

        [HttpGet("{id:int}")]
        public IActionResult GetTema(int id) => Ok(new TemaModel(_temaService.Obter(id)));

        [HttpPost]
        public IActionResult CreateTema([FromBody] TemaModel tema) =>
            CreatedAtAction("CreateTema", new TemaModel(_temaService.Criar(tema?.Nome, tema?.Descricao)));

        [HttpPut("{id:int}")]
        public IActionResult UpdateTema([FromBody] TemaModel tema, int id)
        {
            _temaService.Atualizar(id, tema?.Nome, tema?.Descricao);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTema(int id) => Ok(_temaService.Remover(id));
    }
}
