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
            CredentialModel credenciais = accessManager.ValidateCredentials(user).Result;

            if (credenciais.IsOk)
            {
                return Ok(accessManager.GenerateToken(credenciais.User));
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