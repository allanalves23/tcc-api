using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace _02_DependencyInjection
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseContext>(options => 
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}