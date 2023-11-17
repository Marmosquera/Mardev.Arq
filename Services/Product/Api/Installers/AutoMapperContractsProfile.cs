using AutoMapper;
using Mardev.Arq.Services.Product.Contracts.Dtos;

namespace Mardev.Arq.Services.Product.Api.Installers
{
    public class AutoMapperContractsProfile : Profile
    {
        public AutoMapperContractsProfile()
        {
            CreateMap<ProductDto, Contracts.ProductGetByIdResponse>().ReverseMap();
        }
    }

}
