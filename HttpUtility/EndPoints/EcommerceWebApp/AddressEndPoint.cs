using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.EcommerceWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.EcommerceWebApp
{
    public class AddressEndPoint : EndPoint<HttpResponse>
    {
        public AddressEndPoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponse> Remove(string id)
        {
            return await Delete(id);
        }

        public async Task<HttpResponse> Create(AddressRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse response = await Post(stringPayload);
            return response.TryParseEntity<AddressResponse>();
        }

        public async Task<HttpResponse> Update(AddressRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse response = await Put(request.ExternalIdentifier, stringPayload);
            return response.TryParseEntity<AddressResponse>();
        }

        public async Task<HttpResponse> GetAddresses(string owner)
        {
            ICollection<IFilter> apiFilters = new List<IFilter>();
            apiFilters.Add(new Filter("owner", owner));
            HttpResponse response = await Get(apiFilters);
            return response.TryParseEntity<List<AddressResponse>>();
        }        

        public async Task<HttpResponse> GetDefaultAddress()
        {
            HttpResponse response = await Get("default");
            return response.TryParseEntity<AddressResponse>();
        }
    }

    public class AddressResponse : Address
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedUtc { get; set; }
    }

    public class AddressRequest : Address
    {
    }
}