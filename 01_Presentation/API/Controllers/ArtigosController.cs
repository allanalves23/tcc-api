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

        [HttpGet("{urlPersonalizada}")]
        public IActionResult GetArtigo(string urlPersonalizada) => Ok(new ArtigoModel(_artigoService.Obter(urlPersonalizada)));

        [HttpPost]
        public IActionResult CreateArtigo([FromBody] ArtigoModel artigo) =>
            CreatedAtAction("CreateArtigo", new ArtigoModel(_artigoService.Criar(artigo?.Titulo, User.UserIdSession())));

        [HttpPut("{id:int}")]
        public IActionResult UpdateArtigo(int id, [FromBody] ArtigoModel artigo) =>
            Ok(new ArtigoModel(_artigoService.Atualizar(id, artigo?.Titulo, artigo?.Descricao, artigo?.Conteudo)));

        [HttpPatch("{id:int}")]
        public IActionResult UpdateDetalhesArtigo(int id, [FromBody] ArtigoModel artigo) =>
            Ok(new ArtigoModel(_artigoService.Atualizar(id, artigo?.Url)));

        [HttpDelete("{id:int}")]
        public IActionResult RemoverArtigo(int id) =>
            Ok(new ArtigoModel(_artigoService.Remover(id)));

        [HttpPatch("{id:int}/temas/{idTema:int}/categorias/{idCategoria:int}")]
        public IActionResult UpdateTemaCategoriaArtigo(int id, int idTema, int idCategoria) =>
            Ok(new ArtigoModel(_artigoService.Atualizar(id, idTema, idCategoria)));

        [HttpPut("{id:int}/publicacoes")]
        public IActionResult PublicarArtigo(int id) =>
            Ok(new ArtigoModel(_artigoService.Publicar(id)));

        [HttpDelete("{id:int}/publicacoes")]
        public IActionResult InativarArtigo(int id) =>
            Ok(new ArtigoModel(_artigoService.Inativar(id)));

        [HttpPatch("{id:int}/publicacoes")]
        public IActionResult ImpulsoesArtigo(int id) =>
            Ok(new ArtigoModel(_artigoService.Impulsionar(id)));
    }
}
