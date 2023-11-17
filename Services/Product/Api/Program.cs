
using AutoMapper;
using Mardev.Arq.Services.Product.Api.Installers;
using Mardev.Arq.Shared.Api.ApiVersioning;
using Mardev.Arq.Shared.Api.Automapper;
using Mardev.Arq.Shared.Api.Swagger.Installers;
using Microsoft.OpenApi.Models;

namespace Mardev.Arq.Services.Product.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //Infra
            builder.AddCustomAutomapper(new List<Profile> { new AutoMapperContractsProfile() });

            builder.Services.AddControllers();
            builder.AddApiVersioning();
            builder.AddSwagger(new OpenApiInfo()
            {
                Title = "Product API",
                Description = @"Exposes endpoints to handle products.",
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
