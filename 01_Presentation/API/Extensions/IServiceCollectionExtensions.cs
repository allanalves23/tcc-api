using _02_DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseMyServices(this IServiceCollection services, IConfiguration configuration) =>
            Bootstrap.Configure(services, configuration);
    }
}