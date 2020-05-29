using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.Base.Implementations
{
    public abstract class EndpointBase<TRequest, TResponse> : EndPoint<TResponse>, IGenericEndpoint<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public EndpointBase(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public virtual async Task<TResponse> Create(TRequest postRequest)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(postRequest));
            var response = await Post(stringPayload);

            return response;
        }

        public virtual async Task<TResponse> GetSingle(string externalIdentifier)
        {         
            var response =  await Get(externalIdentifier);
            return response;
        }

        public virtual async Task<TResponse> PatchEntity(string externalIdentifier, object patchRequest)
        {
            MethodUrl = "/" + externalIdentifier;

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(patchRequest));
            var response = await Patch(stringPayload);

            //clean the url
            MethodUrl = "";

            return response;
        }

        public virtual async Task<TResponse> Remove(string externalIdentifier)
        {
            var response = await Delete(externalIdentifier);
            return response;
        }

        public virtual async Task<TResponse> Update(string externalIdentifier, TRequest putRequest)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(putRequest));
            var response = await Put(externalIdentifier, stringPayload);

            return response;
        }
    }
}
