using Mardev.Arq.Services.Identity.Business;

namespace Mardev.Arq.Services.Identity.Api.Installers
{
    public static class DependenciesInstaller
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection(nameof(JwtOptions)));

            builder.Services.AddTransient<IAuthenticationBusiness, AuthenticationBusiness>();
            builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        }
    }
}
