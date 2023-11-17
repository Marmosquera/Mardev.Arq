
using Microsoft.OpenApi.Models;
using Mardev.Arq.Shared.Api.Swagger.Installers;
using Mardev.Arq.Shared.Api.ApiVersioning;


namespace Mardev.Arq.Services.Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.AddApiVersioning();
            builder.AddSwagger(new OpenApiInfo()
            {
                Title = "Identity API",
                Description = @"Exposes endpoints to handle identities.",
            });

            var app = builder.Build();

            app.SetupSwagger();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
