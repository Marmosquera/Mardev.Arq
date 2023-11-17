using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Mardev.Arq.Shared.Api.ApiVersioning
{
    public static class ApiVersioningInstaller
    {
        public static void AddApiVersioning(this WebApplicationBuilder builder)
        {
            var apiVersioningBuilder =  builder.Services.AddApiVersioning(s =>
            {
                s.DefaultApiVersion = new  Asp.Versioning.ApiVersion(1, 0);
                s.AssumeDefaultVersionWhenUnspecified = true;
                s.ReportApiVersions = true;
            });
            apiVersioningBuilder.AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddEndpointsApiExplorer();
        }
    }
}