using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using API.Security;
using API.Models.Identity;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(
            this IServiceCollection services,
            SigningConfigurations signingConfigurations,
            TokenConfigurationsModel tokenConfigurations)
        {
            services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                    {
                        var paramsValidation = bearerOptions.TokenValidationParameters;
                        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                        paramsValidation.ValidAudience = tokenConfigurations.Audience;
                        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                        paramsValidation.ValidateIssuerSigningKey = true;
                        paramsValidation.ValidateLifetime = true;
                        paramsValidation.ClockSkew = TimeSpan.Zero;
                    });

            return services;
        }

        public static SigningConfigurations AddSigningSettings(this IServiceCollection services)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            return signingConfigurations;
        }

        public static TokenConfigurationsModel AddTokenSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = new TokenConfigurationsModel();
            new ConfigureFromConfigurationOptions<TokenConfigurationsModel>(
                configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);

            return tokenConfigurations;
        }
    }
}