using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseMyServices(this IServiceCollection services, IConfiguration configuration) =>
            _02_DependencyInjection.Bootstrap.Configure(services, configuration);

        public static void UseMyPolicies(this IServiceCollection services) =>
            Policies.Bootstrap.Configure(services);
    }
}