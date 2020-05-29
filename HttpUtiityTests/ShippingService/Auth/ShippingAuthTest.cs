using HttpUtiityTests.BaseTest;
using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Auth
{
    [TestClass]
    public class ShippingAuthTest : ApiBaseTest
    {
        readonly IShippingServiceClient Client;

        public ShippingAuthTest()
        {
            string fixedUrl = FixUrl(ServiceConstants.ShippingServiceApiUrl);
            bool hasHttps = ServiceConstants.ShippingServiceApiUrl.Contains("https");
            Client = new ShippingServiceClient(fixedUrl, ServiceConstants.AllPointsPlatformExtId, hasHttps);
        }

        [TestMethod]
        public async Task POST_AuthToken()
        {
            int expectedCode = 200;
            var authRequest = new ShippingAuthRequest
            {
                Name = "aceventura",
                Password = "$Up3rS3cur3",
                PublicKey = "1cf463c2-3d38-27c2-a47e-cdfdb6f88e84"
            };

            var authResponse = await Client.Token.Authenticate(authRequest);

            Assert.IsNotNull(authResponse, $"{nameof(authResponse)} is null");
            Assert.IsNotNull(authResponse.Result, $"{nameof(authResponse.Result)} is null");
            Assert.AreEqual(expectedCode, authResponse.StatusCode);
        }
    }
}
