using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]"), Authorize]
    public class CategoriasController : ControllerBase
    {
        public ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult GetCategorias(string termo, int? skip = 0, int? take = 10) => 
            Ok(_categoriaService.Obter(termo, skip, take).Select(item => new CategoriaModel(item)));

        [HttpGet("quantidade")]
        public IActionResult CountCategorias(string termo) =>
            Ok(_categoriaService.ObterQuantidade(termo));

        [HttpGet("temas/{temaId:int}")]
        public IActionResult GetCategoriasPorTema(string termo, int temaId, int? skip = 0, int? take = 10) => 
            Ok(_categoriaService.Obter(termo, temaId, skip, take).Select(item => new CategoriaModel(item)));

        [HttpGet("{id:int}")]
        public IActionResult GetCategoria(int id) => Ok(new CategoriaModel(_categoriaService.Obter(id)));

        [HttpPost, Authorize("Administrador")]
        public IActionResult CreateCategoria([FromBody] CategoriaModel categoria) =>
            CreatedAtAction("CreateCategoria", new CategoriaModel(_categoriaService.Criar(categoria?.Nome, categoria?.Descricao, categoria?.Tema?.Id)));

        [HttpPut("{id:int}"), Authorize("Administrador")]
        public IActionResult UpdateCategoria([FromBody] CategoriaModel categoria, int id)
        {
            _categoriaService.Atualizar(id, categoria?.Nome, categoria?.Descricao, categoria?.Tema?.Id);
            return NoContent();
        }

        [HttpDelete("{id:int}"), Authorize("Administrador")]
        public IActionResult DeleteCategoria(int id) => Ok(_categoriaService.Remover(id));
    }
}
