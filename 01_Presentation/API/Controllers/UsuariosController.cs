using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]"), Authorize]
    public class UsuariosController : ControllerBase
    {
        public IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult GetUsuarios(string termo, int skip = 0, int take = 15) => 
            Ok(_usuarioService.Obter(termo, skip, take).Select(item => new UsuarioModel(item)));

        [HttpGet("{usuarioId}")]
        public IActionResult GetUsuario(string usuarioId) => 
            Ok(new UsuarioModel(_usuarioService.Obter(usuarioId).Result));
    }
}