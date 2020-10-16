using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;

namespace _02_DependencyInjection
{
    public class Bootstrap
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseContext>(options => 
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                options.UseLoggerFactory(MyLoggerFactory);
                options.EnableSensitiveDataLogging();
                options.UseLazyLoadingProxies();
            });
        }
    }
}