using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Extensions;
using API.Models.Identity;
using API.Security;
using Repository;
using Repository.Contexts;
using Core.Entities;
using Elastic.Apm.NetCoreAll;
using Serilog;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.UseMyAuthorization();
            services.UseMyServices(Configuration);

            services.AddControllers();

            services.AddDbContext<ApiContext>();

            services.AddIdentity<Usuario, IdentityRole>(options => 
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = true
                };
            })
                .AddEntityFrameworkStores<ApiContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();

            TokenConfigurationsModel tokenConfigurations = services.AddTokenSettings(Configuration);
            services.AddSingleton(tokenConfigurations);

            SigningConfigurations signingConfigurations = services.AddSigningSettings();
            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);

            services.UseMyCors(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            DomainContext domainContext,
            ApiContext apiContext,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
            });

            app.UseMyMiddlewares();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            new IdentityInitializer(apiContext, userManager, roleManager).Initialize();

            domainContext.Database.Migrate();


            app.UseCors("AllowedOrigins");

            app.UseAllElasticApm(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
