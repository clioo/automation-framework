using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Catalogs;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface ICatalogsEndpoint : IGenericEndpoint<CatalogRequest, HttpResponseExtended<CatalogResponse>>
    {
    }
}
