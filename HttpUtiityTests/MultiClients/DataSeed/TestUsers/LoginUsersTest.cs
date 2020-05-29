using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.MultiClients.DataSeed.Helpers;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.MultiClients.DataSeed
{
    [TestCategory(TestingCategories.DataSeed)]
    [TestClass]
    public class LoginUsersTest
    {
        ITestDataFactory DataFactory;

        [TestMethod]
        public async Task CREATE_AllPointsTestUsers()
        {
            DataFactory = ConfigurationHelper.SetPlatform(TenantsEnum.AllPoints);

            TestContactInformation genericContactInfo = new TestContactInformation
            {
                CompanyName = "allpoints company",
                Email = "contactEmail@dfs.com",
                FirstName = "johnny",
                LastName = "doe",
                PhoneNumber = "1234567890",
            };

            List<TestUserAccount> allpointsUserAcounts = new List<TestUserAccount>
            {
                new TestUserAccount
                {
                    Email = "eliTest@mail.com",
                    ContactInformation = genericContactInfo,
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "elitest123",
                        ContactExtId = "elitest123",
                        LoginExtId = "elitest123",
                        UserExtId = "elitest123"
                    }
                },
                new TestUserAccount
                {
                    Email = "AllPointsTest2@dfs.com",
                    ContactInformation = genericContactInfo,
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "test-123",
                        LoginExtId = "test-123",
                        ContactExtId = "test-123",
                        UserExtId = "test-123"
                    }
                },
                new TestUserAccount
                {
                    Email = "AllPointsTest3@dfs.com",
                    ContactInformation = genericContactInfo,
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "1257-B",
                        LoginExtId = "1257-B",
                        ContactExtId = "1257-B",
                        UserExtId = "1257-B"
                    }
                },
                new TestUserAccount
                {
                    Email = "AllPointsTest4@dfs.com",
                    ContactInformation = genericContactInfo,
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "1509",
                        ContactExtId = "contact-1509",
                        LoginExtId = "login-1509",
                        UserExtId = "user-1509"
                    }
                },
                new TestUserAccount
                {
                    Email = "AllPointsTest5@dfs.com",
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "A-1509-2",
                        ContactExtId = "contact-A-1509-2",
                        LoginExtId = "login-A-1509-2",
                        UserExtId = "user-A-1509-2"
                    },
                    ContactInformation = genericContactInfo
                }
            };

            foreach (var user in allpointsUserAcounts)
            {
                await DataFactory.UserAccounts.CreateUserAccount(user);
            }
        }

        [TestMethod]
        public async Task CREATE_FmpTestUsers()
        {
            DataFactory = ConfigurationHelper.SetPlatform(TenantsEnum.Fmp);

            //generic contact information
            TestContactInformation contactInfo = new TestContactInformation
            {
                CompanyName = "fmp company",
                Email = "contactEmail@dfs.com",
                FirstName = "johnny",
                LastName = "doe",
                PhoneNumber = "1234567890"
            };

            List<TestUserAccount> fmpUserAccounts = new List<TestUserAccount>
            {
                new TestUserAccount
                {
                    Email = "FMPTest2@dfs.com",
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "9509",
                        ContactExtId = "contact-A-1923",
                        LoginExtId = "login-A-1923",
                        UserExtId = "user-A-1923"
                    },
                    ContactInformation = contactInfo
                },
                new TestUserAccount
                {
                    Email = "FMPTest3@dfs.com",
                    AccountExternalIds = new TestExternalIdentifiers {
                        AccountMasterExtId = "A-1923",
                        ContactExtId = "contact-A-1923",
                        LoginExtId = "login-A-1923",
                        UserExtId = "user-A-1923"
                    },
                    ContactInformation = contactInfo
                },
                new TestUserAccount
                {
                    Email = "FMPTest4@dfs.com",
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "19411",
                        ContactExtId = "contact-19411",
                        LoginExtId = "login-19411",
                        UserExtId = "user-19411"
                    },
                    ContactInformation = contactInfo
                },
                new TestUserAccount
                {
                    Email = "FMPTest5@dfs.com",
                    AccountExternalIds = new TestExternalIdentifiers
                    {
                        AccountMasterExtId = "A-1941",
                        ContactExtId = "contact-A-1941",
                        LoginExtId = "login-A-1941",
                        UserExtId = "user-A-1941"
                    },
                    ContactInformation = contactInfo
                }
            };

            foreach (var user in fmpUserAccounts)
            {
                await DataFactory.UserAccounts.CreateUserAccount(user);
            }
        }

        [TestMethod]
        public async Task CREATE_EliAllPointsTestUser()
        {
            DataFactory = ConfigurationHelper.SetPlatform(TenantsEnum.AllPoints);

            string identifier = "elibr-1234";
            var testUser = new TestUserAccount
            {
                Email = "eliTest@mail.com",
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = identifier,
                    ContactExtId = identifier,
                    LoginExtId = identifier,
                    UserExtId = identifier
                },
                ContactInformation = new TestContactInformation
                {
                    Email = "allpoints@dfs.com",
                    CompanyName = "allpoints",
                    FirstName = "eli",
                    LastName = "barbarick",
                    PhoneNumber = "1234567890"
                }
            };
            await DataFactory.UserAccounts.CreateUserAccount(testUser);
        }
    }
}
