using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using API.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]"), Authorize]
    public class ArtigosController : ControllerBase
    {
        private IArtigoService _artigoService;

        public ArtigosController(IArtigoService artigoService) { 
            _artigoService = artigoService;
        }

        [HttpGet]
        public IActionResult GetArtigos(string termo, int? skip = 0, int? take = 10) => 
            Ok(_artigoService.Obter(termo, skip, take).Select(item => new ArtigoModel(item)));

        [HttpGet("{id:int}")]
        public IActionResult GetArtigo(int id) => Ok(new ArtigoModel(_artigoService.Obter(id)));

        [HttpPost]
        public IActionResult CreateArtigo([FromBody] ArtigoModel artigo) =>
            CreatedAtAction("CreateArtigo", new ArtigoModel(_artigoService.Criar(artigo?.Titulo, User.UserIdSession())));
    }
}
