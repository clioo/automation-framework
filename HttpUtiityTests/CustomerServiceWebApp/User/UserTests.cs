using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.CustomerServiceWebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtiityTests.CustomerServiceWebApp.User
{
    [TestClass]
    public class UserTests
    {
        public string GenericGuid = "f47de712-abf3-4ab3-8ea9-f0ef663d6491";

        [TestMethod]
        public void GetUserByInternalId()
        {
            CustomerServiceWebAppClient customerServiceClient = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);

            GetUserRequest getUserRequest = new GetUserRequest
            {
                Id = new Guid(GenericGuid)
            };

            var getUserResponse = customerServiceClient.Users.GetByInternalIdentifier(getUserRequest).Result;

            Assert.IsNotNull(getUserResponse, $"{nameof(getUserResponse)} should not be null");
            Assert.IsNotNull(getUserResponse.Result, $"{nameof(getUserResponse.Result)} should not be null");
            Assert.IsNotNull(getUserResponse.Result.Identifier, $"{nameof(getUserResponse.Result.Identifier)} cannot be null");
        }

        [TestMethod]
        public void CreateUserAssignAddressAsDefault()
        {
            CustomerServiceWebAppClient customerServiceClient = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);
            IntegrationsWebAppClient integrationsClient = new IntegrationsWebAppClient(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformId);
            CreateUserRequest createRequest = new CreateUserRequest
            {
                AccountIdentifier = Guid.NewGuid(),
                ContactIdentifier = Guid.NewGuid(),
                CookieString = string.Empty,
                DefaultAddressIdentifier = Guid.NewGuid(),
                DefaultCreditCardPaymentIdentifier = Guid.NewGuid(),
                Identifier = Guid.NewGuid(),
                IsAnonymous = false,
                IsEnabled = true,
                LoginIdentifier = Guid.NewGuid(),
                PlatformIdentifier = Guid.NewGuid(),
                RoleGuids = new List<string>
                {
                    Guid.NewGuid().ToString()
                },
                UseAccountTermsAsDefaultPayment = false
            };
            HttpResponse<UserResponse> createUserResponse = customerServiceClient.Users.Create(createRequest).Result;

            Assert.IsNotNull(createUserResponse, "Response is null");
            Assert.IsNotNull(createUserResponse.Result, $"{nameof(createUserResponse.Result)} is null");
        }

        [TestMethod]
        public void EditUserAssignAddressAsDefault()
        {
            CustomerServiceWebAppClient customerServiceClient = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);
            IntegrationsWebAppClient integrationsClient = new IntegrationsWebAppClient(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformId);
            UpdateUserRequest updateRequest = new UpdateUserRequest
            {
                AccountIdentifier = Guid.NewGuid(),
                ContactIdentifier = Guid.NewGuid(),
                CookieString = string.Empty,
                DefaultAddressIdentifier = Guid.NewGuid(),
                DefaultCreditCardPaymentIdentifier = Guid.NewGuid(),
                Identifier = Guid.NewGuid(),
                IsAnonymous = false,
                IsEnabled = true,
                LoginIdentifier = Guid.NewGuid(),
                PlatformIdentifier = Guid.NewGuid(),
                RoleGuids = new List<string>
                {
                    Guid.NewGuid().ToString()
                },
                UseAccountTermsAsDefaultPayment = true
            };
            HttpResponse<UserResponse> updateResponse = customerServiceClient.Users.Update(updateRequest).Result;
            Assert.IsNotNull(updateResponse, "Response is null");
            Assert.IsNotNull(updateResponse.Result, $"{nameof(updateResponse.Result)} is null");
        }

        [TestMethod]
        public void EditUserRemoveDefaultAddress()
        {

        }

        [TestMethod]
        public void DeleteUser()
        {
            CustomerServiceWebAppClient customerServiceClient = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);
            DeleteUserRequest deleteRequest = new DeleteUserRequest
            {
                Id = "f47de712-abf3-4ab3-8ea9-f0ef663d6491"
            };
            HttpResponse<UserResponse> deleteResponse = customerServiceClient.Users.Remove(deleteRequest).Result;
            Assert.IsNotNull(deleteResponse, "Response is null");
            Assert.IsNotNull(deleteResponse.Result, $"{nameof(deleteResponse.Result)} is null");
        }        
    }
}
