using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateSchedulesConfigurationEndpoint : EndPoint<HttpEssResponse<FlatRatesSchedulesConfigurationResponse>>, IFlatRateSchedulesConfigurationEndpoint
    {
        public FlatRateSchedulesConfigurationEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<FlatRatesSchedulesConfigurationResponse>> Create(string groupId, string scheduleId, FlatRateScheduleConfigurationRequest request)
        {
            MethodUrl = $"/flatRateScheduleGroups/{groupId}/schedules/{scheduleId}";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));

            var response = await Post(stringPayload);
            return response;
        }

        public async Task<HttpEssResponse<FlatRatesSchedulesConfigurationResponse>> Remove(string groupId, string scheduleId)
        {
            MethodUrl = $"/flatRateScheduleGroups/{groupId}/schedules/{scheduleId}";

            var response = await Delete("");
            return response;
        }
    }
}
