using AutoMapper;

namespace Mardev.Arq.Services.Identity.Api.Installers
{
    public class AutoMapperContractsProfile : Profile
    {
        public AutoMapperContractsProfile()
        {
            CreateMap<Business.Models.LoginResult, Contracts.AuthLoginResponse>().ReverseMap();
        }
    }

}
