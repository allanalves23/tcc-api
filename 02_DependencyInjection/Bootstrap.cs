using API.Security;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Services;

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

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IArtigoService, ArtigoService>();
            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<ITemaService, TemaService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
        }
    }
}