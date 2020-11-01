using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]"), Authorize]
    public class AutoresController : ControllerBase
    {
        public IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("{usuarioId}")]
        public IActionResult GetAutor(string usuarioId) => 
            Ok(new AutorModel(_autorService.Obter(usuarioId)));

        [HttpPut("{id:int}")]
        public IActionResult UpdateAutor([FromBody] AutorModel autor, int id) =>
            Ok(new AutorModel(_autorService.Atualizar(id, autor?.Nome, autor?.Email, autor?.Genero)));
    }
}