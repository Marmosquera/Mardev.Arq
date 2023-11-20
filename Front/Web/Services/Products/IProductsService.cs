using Mardev.Arq.Services.Product.Contracts;
using Mardev.Arq.Shared.Contracts;

namespace Mardev.Arq.Front.Web.Services.Product
{
    public interface IProductsService
    {
        Task<ServiceResponse<ProductGetResponse>> GetAllProducts();
    }
}
