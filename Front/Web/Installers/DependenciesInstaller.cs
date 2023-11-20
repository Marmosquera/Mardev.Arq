using Mardev.Arq.Front.Web.Services.Identity;
using Mardev.Arq.Front.Web.Services.Product;

namespace Mardev.Arq.Front.Web.Installers
{
    public static class DependenciesInstaller
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient(nameof(IdentityService));
            builder.Services.Configure<IdentityServiceOptions>(
                builder.Configuration.GetSection(nameof(IdentityServiceOptions)));
            builder.Services.AddScoped<IIdentityService, IdentityService>();

            builder.Services.AddHttpClient(nameof(ProductsService));
            builder.Services.Configure<ProductsServiceOptions>(
                builder.Configuration.GetSection(nameof(ProductsServiceOptions)));
            builder.Services.AddScoped<IProductsService, ProductsService>();


        }
    }
}
