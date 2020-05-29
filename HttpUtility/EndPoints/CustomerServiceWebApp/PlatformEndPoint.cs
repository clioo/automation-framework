using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.CustomerServiceWebApp
{
    //Couldnt get through windows Auth, insted used LoginEndPoint
    public class PlatformEndPoint : EndPoint<ChoosePlatformResponse>
    {
        public PlatformEndPoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<string> SetPlatform(ChoosePlatformRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            return await PostNoJson(stringPayload);
        }
    }

    public class ChoosePlatformRequest
    {
        public Guid PlatformIdentifier { get; set; }
    }

    public class ChoosePlatformResponse
    {
    }
}