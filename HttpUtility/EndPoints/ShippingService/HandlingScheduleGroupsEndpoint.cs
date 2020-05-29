using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService
{
    public class HandlingScheduleGroupsEndpoint : EndpointBase<HandlingScheduleGroupRequest, HttpEssResponse<HandlingScheduleGroupResponse>>, IHandlingScheduleGroupsEndpoint
    {
        public HandlingScheduleGroupsEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }        
    }
}
