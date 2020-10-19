using Microsoft.AspNetCore.Mvc;
using API.Security;
using API.Models.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] UserModel user, [FromServices]AccessManager accessManager)
        {
            if (accessManager.ValidateCredentials(user))
            {
                return Ok(accessManager.GenerateToken(user));
            }
            else
            {
                return Unauthorized(new
                {
                    Error = "Acesso não autorizado"
                });
            }
        }
    }
}