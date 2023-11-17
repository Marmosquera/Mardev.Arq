namespace Mardev.Arq.Services.Product.Api.Installers
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