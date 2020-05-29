using DatabaseUtility.Constants;
using DatabaseUtility.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseUtility.Test
{
    [TestClass]
    public class ConfigurationServiceTest : ServiceTestBase<ConfigurationService>
    {
        public ConfigurationServiceTest() : base(ConfigurationConstants.ConfigurationDatabase)
        {
        }

        [TestMethod]
        public void Create()
        {
            string email = "Max@stk.com";
            var newLogin = service.CreateLogin(email).Result;
            Assert.IsTrue(newLogin.Contents.Email == email);
        }
    }
}