using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.EcommerceWebApp
{
    public class AuthEndPoint : EndPoint<string>
    {
        public AuthEndPoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<object> GetCookie(AuthCookieRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            return await PostNoJson(stringPayload);
        }
    }

    public class AuthCookieRequest
    {
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
    }
}