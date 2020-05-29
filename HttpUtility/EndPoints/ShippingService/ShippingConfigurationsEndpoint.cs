using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingConfigurationsEndpoint : EndpointBase<ShippingConfigurationRequest, HttpEssResponse<ShippingConfigurationResponse>>, IShippingConfigurationEndpoint
    {
        public ShippingConfigurationsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}
