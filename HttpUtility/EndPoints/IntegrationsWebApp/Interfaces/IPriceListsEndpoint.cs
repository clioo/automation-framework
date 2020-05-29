using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.PriceLists;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IPriceListsEndpoint : IGenericEndpoint<PriceListRequest, HttpResponseExtended<PriceListResponse>>
    {
    }
}
