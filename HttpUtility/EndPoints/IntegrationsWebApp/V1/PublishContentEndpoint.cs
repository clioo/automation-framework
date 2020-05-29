using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class PublishContentEndpoint : EndPoint<HttpResponse<PublishContentResponse>>
    {
        public PublishContentEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponse<PublishContentResponse>> Publish(PublishContentRequest request)
        {
            ICollection<IFilter> filters = null;

            if (!string.IsNullOrEmpty(request.Hostname))
            {
                filters = new List<IFilter>()
                {
                    new Filter("hostname", request.Hostname)
                };
            }

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(new { }));
            HttpResponse<PublishContentResponse> result =  await Post(stringPayload, filters);
            return result;
        }
    }

    public class PublishContentRequest
    {
        public string Hostname { get; set; }
    }

    public class PublishContentResponse
    {
    }
}
