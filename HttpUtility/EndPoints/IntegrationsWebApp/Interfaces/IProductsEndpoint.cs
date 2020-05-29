using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IProductsEndpoint : IGenericEndpoint<ProductRequest, HttpResponseExtended<ProductResponse>>
    {
    }
}
