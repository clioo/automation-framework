using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.MultiClients.DataSeed.Helpers;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
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
    public class AllpointsUsersDataFactoryTest
    {
        IAutomationDataFactory DataFactory;

        public AllpointsUsersDataFactoryTest()
        {
            var config = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.AllPointsPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.AllPointsPlatformId
            };
            DataFactory = new AutomationDataFactory(config);
        }

        [TestMethod]
        public void Create_Allpoints_DefaultUserAccount_Success()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_Implicit_Success()
        {
            string identifier = "asdasd123";

            var testUser = new TestUserAccount
            {
                Email = "allpointsSpecific01@mail.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "implicit",
                    Email = "implicit@mail.com",
                    FirstName = "firstName",
                    LastName = "lastName",
                    PhoneNumber = "0123456789"
                },
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = identifier,
                    ContactExtId = identifier,
                    LoginExtId = identifier,
                    UserExtId = identifier
                }
            };
            DataFactory.Users.CreateTestUser(testUser);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_GivenIdentifier_Success()
        {
            string identifier = "allpointsUserIdOnly01";

            var testUser = DataFactory.Users.CreateTestUser(identifier);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_DefaultUserAccount_Withterms_Success()
        {
            string termsDescription = "Net days ;3 default user";

            var testUser = DataFactory.Users.CreateTestUserWithTerms(termsDescription);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_Withterms_Success()
        {
            string termsDescription = "Net days ;3";
            string identifier = "termsUserNoDefault01abc";

            var testUser = new TestUserAccount
            {
                Email = "termsUser@mail.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "dfs",
                    Email = "allpoints@dfs.com",
                    FirstName = "firstName",
                    LastName = "lastName",
                    PhoneNumber = "0123456789"
                },
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = identifier,
                    ContactExtId = identifier,
                    LoginExtId = identifier,
                    UserExtId = identifier
                }
            };

            DataFactory.Users.CreateTestUserWithTerms(termsDescription, testUser);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Remove_AllPoints_UserAccount_ByIdentifier_Success()
        {
            string identifier = "removedUserByIdentifier01";

            var testUser = DataFactory.Users.CreateTestUser(identifier);
            DataFactory.Users.RemoveUserAccount(identifier);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Remove_AllPoints_UserAccount_ByInstance_Success()
        {
            string identifier = "removedUserByIdentifier01";

            var testUser = DataFactory.Users.CreateTestUser(identifier);
            DataFactory.Users.RemoveUserAccount(testUser);

            Console.WriteLine(testUser.Username);
        }
    }
}
