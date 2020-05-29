using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;

namespace HttpUtility.EndPoints.ShippingService
{
    public class HandlingSchedulesEndpoint : EndpointBase<HandlingScheduleRequest, HttpEssResponse<HandlingScheduleResponse>>, IHandlingSchedulesEndpoint
    {
        public HandlingSchedulesEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }
    }
}
