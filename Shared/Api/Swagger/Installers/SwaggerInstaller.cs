using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Mardev.Arq.Shared.Api.Swagger.Installers
{
    public static class SwaggerInstaller
    {

        public static void AddSwagger(this WebApplicationBuilder builder, OpenApiInfo info)
        {
            var sp = builder.Services.BuildServiceProvider();
            var apiVersionProvider = sp.GetRequiredService<IApiVersionDescriptionProvider>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen(c =>
            {
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(
                        description.GroupName,
                        CreateVersionInfo(description, info));
                }

                //c.AddServer(new OpenApiServer
                //{
                //    Description = "local",
                //    Url = "https://localhost:{port}/api",
                //    Variables = new Dictionary<string, OpenApiServerVariable>()
                //    {
                //        {
                //            "port", new OpenApiServerVariable() { Default = "7100", Enum = new List<string>{ "7100", "5000"} }
                //        }
                //    }
                //});

                // Enable XML comments
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile =>
                {
                    //c.SchemaFilter<ExamplesSchemaFilter>(new System.Xml.XPath.XPathDocument(xmlFile));
                    c.IncludeXmlComments(xmlFile);
                });
                //c.SchemaFilter<ContractsExamplesSchemaFilter>();
                //c.CustomSchemaIds(x => x.Name.ToSnakeCase());
                c.EnableAnnotations();
                /*c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                        ? (methodInfo.DeclaringType?.Name.Replace("Controller", string.Empty) + methodInfo.Name).ToSnakeCase()
                        : string.Empty;
                });
                c.OperationFilter<SwaggerInternalOperationFilter>();
                c.DocumentFilter<RemoveApiPathDocumentFilter>();
                c.ParameterFilter<CaseQueryParameterFilter>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter Bearer authorisation token (JWT or opaque)",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "Bearer {token}",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        // defines scope - without a protocol use an empty array for global scope
                        { securityScheme, Array.Empty<string>() }
                    });
                */
            });

        }

        public static void SetupSwagger(this WebApplication app)
        {
            var apiVersionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        o.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName);
                    }
                    o.DisplayOperationId();
                    o.ShowExtensions(); //To show x-internal extension
                });
            }

        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description, OpenApiInfo info)
        {
            info.Version = description.ApiVersion.ToString();
            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
