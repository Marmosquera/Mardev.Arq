using Mardev.Arq.Services.Identity.Contracts;
using Mardev.Arq.Shared.Contracts;

namespace Mardev.Arq.Front.Web.Services.Identity
{
    public interface IIdentityService
    {
        Task<ServiceResponse<AuthLoginResponse>> Login(AuthLoginRequest request);
    }
}
