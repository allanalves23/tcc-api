using System;
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
            services.AddDbContext<BaseContext>(
                DbContextOptions(configuration.GetConnectionString("DomainConnection"))
            );

            services.AddDbContext<ApplicationDbContext>(
                DbContextOptions(configuration.GetConnectionString("ApiConnection"))
            );

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IArtigoService, ArtigoService>();
            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<ITemaService, TemaService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
        }

        private static Action<DbContextOptionsBuilder> DbContextOptions(string connectionString)
        {
            return new Action<DbContextOptionsBuilder>(options => 
            {
                options.UseMySql(connectionString);
                options.UseLoggerFactory(MyLoggerFactory);
                options.EnableSensitiveDataLogging();
                options.UseLazyLoadingProxies();
            });
        }
    }
}