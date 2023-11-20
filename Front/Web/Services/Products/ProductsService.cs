using Mardev.Arq.Services.Product.Contracts;
using Mardev.Arq.Shared.Contracts;
using Microsoft.Extensions.Options;

namespace Mardev.Arq.Front.Web.Services.Product
{
    public class ProductsService : BaseService, IProductsService
    {
        private readonly ProductsServiceOptions _options;

        public ProductsService(
            IOptions<ProductsServiceOptions> options,
            IHttpClientFactory httpClientFactory) : base(httpClientFactory, nameof(ProductsService))
        {
            _options = options.Value;
        }

        public async Task<ServiceResponse<ProductGetResponse>> GetAllProducts()
        {
            return await SendAsync<ProductGetResponse>(
                new ServiceRequest()
                {
                    ApiType = ApiType.GET,
                    Data = null,
                    Url = _options.BaseUrl + "/api/v1/products"
                },
                withBearer: false);
        }
    }
}
