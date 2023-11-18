﻿using Mardev.Arq.Front.Web.Services.Identity;

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

        }
    }
}
