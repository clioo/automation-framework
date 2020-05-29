using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateScheduleGroupsEndpoint : EndpointBase<FlatRateScheduleGroupsRequest, HttpEssResponse<FlatRateScheduleGroupsResponse>>, IFlatRateScheduleGroupsEndpoint
    {
        public FlatRateScheduleGroupsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}
