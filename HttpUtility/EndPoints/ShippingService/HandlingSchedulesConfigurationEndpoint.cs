using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService
{
    public class HandlingSchedulesConfigurationEndpoint : EndPoint<HttpEssResponse<HandlingSchedulesConfigurationResponse>>, IHandlingSchedulesConfigurationEndpoint
    {
        public HandlingSchedulesConfigurationEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<HandlingSchedulesConfigurationResponse>> Create(string groupId, string scheduleId, HandlingSchedulesConfigurationRequest request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            MethodUrl = $"/handlingScheduleGroups/{groupId}/schedules/{scheduleId}";

            var response = await Post(stringPayload);
            return response;
        }

        public async Task <HttpEssResponse<HandlingSchedulesConfigurationResponse>> Remove(string groupId, string scheduleId)
        {
            MethodUrl = $"/handlingScheduleGroups/{groupId}/schedules/{scheduleId}";

            var response = await Delete("");
            return response;
        }
    }
}
