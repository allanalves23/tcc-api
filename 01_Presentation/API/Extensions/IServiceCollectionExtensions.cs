using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseMyServices(this IServiceCollection services, IConfiguration configuration) =>
            _02_DependencyInjection.Bootstrap.Configure(services, configuration);

        public static void UseMyAuthorization(this IServiceCollection services) =>
            Policies.Bootstrap.Configure(services);

        public static void UseMyCors(this IServiceCollection services, IConfiguration configuration) =>
            services.AddCors(options =>
                options.AddPolicy(
                    "AllowedOrigins",
                    builder =>
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(configuration["Origins"].Split(";"))
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowCredentials()
                )
            );
    }
}