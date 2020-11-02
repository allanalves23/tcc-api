using API.Policies.Handlers;
using API.Policies.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace API.Policies
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services) =>
            DefinePolicies(services);

        private static void DefinePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy("Administrador", policy => 
                {
                    policy.Requirements.Add(new PoliticaDeAdministrador());
                });
            });

            services.AddScoped<IAuthorizationHandler, PoliticaDeAdministradorHandler>();
        }
    }
}