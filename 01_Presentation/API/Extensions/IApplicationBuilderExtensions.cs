using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app) =>
            app.UseExceptionHandler(builder =>
                builder.Run(async context =>
                {
                    IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = exceptionHandlerFeature != null ? (int)exceptionHandlerFeature.Error.ToStatusHttp() : 500;
                    context.Response.ContentType = "application/json";
                    var error = new { Error = exceptionHandlerFeature?.Error?.GetMessage() };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                })
            );

        public static void UseMyMiddlewares(this IApplicationBuilder app) 
        {
            app.UseGlobalExceptionHandler();
        }
    }
}
