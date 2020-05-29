using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class PublishMerchandiseEndpoint : EndPoint<HttpResponse<PublishMerchandiseResponse>>, IPublishMerchandiseEndpoint
    {
        public PublishMerchandiseEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponse<PublishMerchandiseResponse>> Publish(PublishMerchandiseRequest request)
        {
            ICollection<IFilter> filters = null;

            if (!string.IsNullOrEmpty(request.Hostname))
            {
                filters = new List<IFilter>
                {
                    new Filter("hostname", request.Hostname),
                    new Filter("promoteonsuccess", request.PromoteOnSuccess.ToString()),
                    new Filter("clearUnusedCache", request.ClearUnusedCache.ToString()),
                    new Filter("purgeOnSuccess", request.PurgeOnSuccess.ToString())
                };
            }

            HttpResponse<PublishMerchandiseResponse> response = await Post(string.Empty, filters);
            return response;
        }
    }

    public class PublishMerchandiseRequest
    {
        //query parameters
        public string Hostname { get; set; }
        public bool PromoteOnSuccess { get; set; } = false;
        public bool ClearUnusedCache { get; set; } = false;
        public bool PurgeOnSuccess { get; set; } = false;
    }

    public class PublishMerchandiseResponse
    {
    }
}
