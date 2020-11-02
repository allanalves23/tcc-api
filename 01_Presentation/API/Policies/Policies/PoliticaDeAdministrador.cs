using API.Extensions;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API.Policies.Policies
{
    public class PoliticaDeAdministrador : IAuthorizationRequirement
    {
        public PoliticaDeAdministrador() { }

        public bool Respected(
            IHttpContextAccessor httpContextAccessor,
            AuthorizationHandlerContext context,
            IPerfilDeAcessoService perfilDeAcessoService)
        {
            try
            {
                return perfilDeAcessoService.Obter(context.User.UserIdSession())?.Perfil == TipoUsuario.Admin;
            }
            catch
            {
                return false;
            }
        }
    }
}