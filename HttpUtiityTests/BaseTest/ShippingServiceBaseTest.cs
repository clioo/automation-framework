using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.TestBase
{
    public abstract class ShippingServiceBaseTest<TEntity>
        where TEntity : class
    {
        protected IShippingServiceClient Client;
        public TestContext TestContext
        {
            get => _testContext;
            set => _testContext = value;
        }
        static TestContext _testContext { get; set; }

        public ShippingServiceBaseTest(string shippingServiceApiUrl, string tenantExternalId, ShippingAuthRequest authRequest)
        {
            ClientSetup(shippingServiceApiUrl, tenantExternalId, authRequest);
        }

        public ShippingServiceBaseTest(string tenantExtId)
        {
            var authRequest = new ShippingAuthRequest
            {
                Name = ServiceConstants.AuthName,
                Password = ServiceConstants.AuthPassword,
                PublicKey = ServiceConstants.AuthPublicKey
            };
            ClientSetup(ServiceConstants.ShippingServiceApiUrl, tenantExtId, authRequest);
        }

        protected void ClientSetup(string shippingServiceApiUrl, string tenantExternalId, ShippingAuthRequest authRequest)
        {
            Client = new ShippingServiceClient(FixUrl(shippingServiceApiUrl), tenantExternalId, shippingServiceApiUrl.Contains("https"));

            //auth credentials validation
            if (CheckStringNullOrEmpty(authRequest.Name, nameof(authRequest.Name)) != null)
            {
                throw new System.Exception($"{nameof(authRequest.Name)} does not have a value");
            }
            if (CheckStringNullOrEmpty(authRequest.Password, nameof(authRequest.Password)) != null)
            {
                throw new System.Exception($"{nameof(authRequest.Password)} does not have a value");
            }
            if (CheckStringNullOrEmpty(authRequest.PublicKey, nameof(authRequest.PublicKey)) != null)
            {
                throw new System.Exception($"{nameof(authRequest.PublicKey)} does not have a value");
            }

            var authResponse = Client.Token.Authenticate(authRequest).Result;
            string bearerToken = authResponse.Result;

            Client = new ShippingServiceClient(FixUrl(shippingServiceApiUrl), tenantExternalId, bearerToken, shippingServiceApiUrl.Contains("https"));
        }

        protected abstract Task TestScenarioSetUp(TEntity data);
        protected abstract Task TestScenarioCleanUp(TEntity data);

        private string FixUrl(string url)
        {
            if (url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }

        private string CheckStringNullOrEmpty(string text, string fieldName)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                return fieldName;
            }
            //positive scenario returns true
            return null;
        }
    }
}
