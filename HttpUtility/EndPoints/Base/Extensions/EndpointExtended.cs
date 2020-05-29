using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.Base.Extensions
{
    public class EndpointExtended<TEntity> : EndPoint<TEntity>
        where TEntity : class
    {
        readonly string _relativeUrl;

        public EndpointExtended(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
            _relativeUrl = url;
        }

        protected async Task<TModel> GetMany<TModel>(string baseUrl, ICollection<IFilter> filters = null)
            where TModel : class
        {
            var httpClient = new HttpClient();

            //TODO
            //handle filters
            //if (filters != null)

            //temporal
            string fullUrl = $"{baseUrl}{_relativeUrl}";

            using(var res = await httpClient.GetAsync(fullUrl))
            {
                var content = await res.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TModel>(content);

                return result;
            }
        }
    }
}
