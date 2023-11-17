
using Mardev.Arq.Services.Product.Api.Installers;

namespace Mardev.Arq.Services.Product.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //Infra
            builder.AddCustomAutomapper();

            builder.Services.AddControllers();
            builder.AddApiVersioning();
            builder.AddSwagger();

            var app = builder.Build();

            app.SetupSwagger();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
