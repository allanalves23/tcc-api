using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace API.Policies
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services)
        {
            DefineDefaultPolicy(services);
        }

        private static void DefineDefaultPolicy(IServiceCollection services) =>
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build();
            });
    }
}