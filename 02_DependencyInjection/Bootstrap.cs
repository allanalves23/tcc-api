using System;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Contexts;
using Services;

namespace _02_DependencyInjection
{
    public class Bootstrap
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DomainContext>(
                DbContextOptions(configuration.GetConnectionString("DomainConnection"))
            );

            services.AddDbContext<ApiContext>(
                DbContextOptions(configuration.GetConnectionString("ApiConnection"))
            );

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IArtigoService, ArtigoService>();
            services.AddTransient<IPerfilDeAcessoService, PerfilDeAcessoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
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