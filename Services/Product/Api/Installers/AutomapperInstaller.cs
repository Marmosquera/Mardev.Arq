using AutoMapper;
using Mardev.Arq.Services.Product.Contracts.Dtos;

namespace Mardev.Arq.Services.Product.Api.Installers
{
    public static class AutomapperInstaller
    {
        public static void AddCustomAutomapper(this WebApplicationBuilder builder)
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new AutoMapperContractsProfile());
                    //cfg.AddProfile(new AutoMapperMessagesProfile());
                    //cfg.AddProfile(new AutoMapperExternalServicesProfile());
                });

            IMapper mapper = new Mapper(mapperConfig);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(mapperConfig);

            builder.Services.Add(new ServiceDescriptor(typeof(IMapper),
                sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService),
                ServiceLifetime.Transient));
        }
    }
    public class AutoMapperContractsProfile : Profile
    {
        public AutoMapperContractsProfile()
        {
            CreateMap<ProductDto, Contracts.ProductGetByIdResponse>().ReverseMap();
        }
    }

}
