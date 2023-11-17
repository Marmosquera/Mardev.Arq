using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mardev.Arq.Shared.Api.Automapper
{
    public static class AutomapperInstaller
    {
        public static void AddCustomAutomapper(this WebApplicationBuilder builder, IEnumerable<Profile> profiles)
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });

            IMapper mapper = new Mapper(mapperConfig);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(mapperConfig);

            builder.Services.Add(new ServiceDescriptor(typeof(IMapper),
                sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService),
                ServiceLifetime.Transient));
        }
    }

}
