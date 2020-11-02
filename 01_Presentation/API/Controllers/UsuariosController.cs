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
            Ok(new UsuarioModel(_usuarioService.ObterAsync(usuarioId).Result));

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UsuarioModel usuario) => 
            Ok(new UsuarioModel(_usuarioService.Criar(usuario?.Email, usuario?.Senha, usuario?.PerfilDeAcesso)));

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(string id, [FromBody] UsuarioModel usuario) =>
            Ok(new UsuarioModel(_usuarioService.AtualizarAsync(id, usuario?.Email, usuario?.PerfilDeAcesso).Result));

        [HttpDelete("{id}")]
        public IActionResult RemoverUsuario(string id)
        {
            _usuarioService.Remover(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AlterarSenha(string id, [FromBody] TrocaDeSenhaModel trocaDeSenha)
        {
            _usuarioService.AlterarSenhaAsync(
                id,
                trocaDeSenha?.SenhaAtual,
                trocaDeSenha?.NovaSenha,
                trocaDeSenha?.ConfirmacaoDeSenha
            ).Wait();

            return NoContent();
        }

        [HttpPut("{id}/restauracoes")]
        public IActionResult RestaurarUsuario(string id)
        {
            _usuarioService.Reativar(id);
            return NoContent();
        }
    }
}