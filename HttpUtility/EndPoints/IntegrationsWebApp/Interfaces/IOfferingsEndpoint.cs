using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IOfferingsEndpoint : IGenericEndpoint<OfferingRequest, HttpResponseExtended<OfferingResponse>>
    {
    }
}
