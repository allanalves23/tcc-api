using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemaController : ControllerBase
    {
        public ITemaService _temaService;

        public TemaController(ITemaService temaService)
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
            Created("", new TemaModel(_temaService.Criar(tema?.Nome, tema?.Descricao)));

        [HttpDelete("{id:int}")]
        public IActionResult UpdateTema(int id) => Ok(_temaService.Remover(id));
    }
}
