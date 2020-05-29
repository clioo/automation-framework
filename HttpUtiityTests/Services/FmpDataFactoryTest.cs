using HttpUtiityTests.EnvConstants;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtiityTests.Services
{

    [TestClass]
    public class FmpDataFactoryTest
    {
        readonly IAutomationDataFactory DataFactory;

        public FmpDataFactoryTest()
        {
            var config = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.FmpPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.FMPPlatformId,
            };
            DataFactory = new AutomationDataFactory(config);
        }

        [TestMethod]
        public void Create_Fmp_UserAccount_Default_Test()
        {
            var testUser = DataFactory.Users.CreateTestUser();
        }

        [TestMethod]
        public void Create_Fmp_UserAccount_Specific_Test()
        {
            var user = new TestUserAccount
            {
                Email = "fmpUserSpecific@mail.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "stk",
                    Email = "sample@mail.com",
                    FirstName = "ivan",
                    LastName = "trujillo",
                    PhoneNumber = "1234567890"
                }
            };
            DataFactory.Users.CreateTestUser(user);
        }

        [TestMethod]
        public void Create_Fmp_UserAccountOnlyIdentifier()
        {
            var testUser = DataFactory.Users.CreateTestUser("fmpUserIdOnly01");
        }
    }
}
