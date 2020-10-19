using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using API.Models;
using API.Security;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] User user, [FromServices]AccessManager accessManager)
        {
            if (accessManager.ValidateCredentials(user))
            {
                return Ok(accessManager.GenerateToken(user));
            }
            else
            {
                return BadRequest(new
                {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                });
            }
        }
    }
}