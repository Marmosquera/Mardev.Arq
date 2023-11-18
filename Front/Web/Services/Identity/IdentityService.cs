using Mardev.Arq.Services.Identity.Contracts;
using Mardev.Arq.Shared.Contracts;
using Microsoft.Extensions.Options;

namespace Mardev.Arq.Front.Web.Services.Identity
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IdentityServiceOptions _options;

        public IdentityService(
            IOptions<IdentityServiceOptions> options,
            IHttpClientFactory httpClientFactory) : base(httpClientFactory, nameof(IdentityService))
        {
            _options = options.Value;
        }

        public async Task<ServiceResponse<AuthLoginResponse>> Login(AuthLoginRequest request)
        {
            return await SendAsync<AuthLoginResponse>(
                new ServiceRequest()
                {
                    ApiType = ApiType.POST,
                    Data = request,
                    Url = _options.BaseUrl + "/api/auth/login"
                },
                withBearer: false);
        }
    }
}
