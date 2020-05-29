using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpUtiityTests.BaseTest
{
    public abstract class ApiBaseTest
    {
        IShippingServiceClient BaseClient;

        public TestContext TestContext
        {
            get => _testContext;
            set => _testContext = value;
        }
        static TestContext _testContext { get; set; }

        protected string FixUrl(string url)
        {
            if (url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }
    
        protected string GetBearerToken(string authName, string password, string publicKey, bool shippingApiHasHttps)
        {
            ShippingAuthRequest authRequest = new ShippingAuthRequest
            {
                Name = authName,
                Password = password,
                PublicKey = publicKey
            };
            BaseClient = new ShippingServiceClient(FixUrl(ServiceConstants.ShippingServiceApiUrl), ServiceConstants.AllPointsPlatformExtId, shippingApiHasHttps);
            var authResponse = BaseClient.Token.Authenticate(authRequest).Result;
            string bearerToken = authResponse.Result;
            return bearerToken;
        }
    }
}
