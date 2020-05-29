using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateSchedulesEndpoint : EndpointBase<FlatRateScheduleRequest, HttpEssResponse<FlatRateScheduleResponse>>, IFlatRateSchedulesEndpoint
    {
        public FlatRateSchedulesEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }        
    }
}
