using System.Threading.Tasks;
using API.Policies.Policies;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API.Policies.Handlers
{
    public class PoliticaDeAdministradorHandler : AuthorizationHandler<PoliticaDeAdministrador>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPerfilDeAcessoService _perfilDeAcessoService;

        public PoliticaDeAdministradorHandler(IHttpContextAccessor httpContextAccessor, IPerfilDeAcessoService perfilDeAcessoService)
        {
            _httpContextAccessor = httpContextAccessor;
            _perfilDeAcessoService = perfilDeAcessoService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PoliticaDeAdministrador requirement)
        {
            if(requirement.Respected(_httpContextAccessor, context, _perfilDeAcessoService))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}