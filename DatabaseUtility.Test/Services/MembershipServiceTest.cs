using DatabaseUtility.Constants;
using DatabaseUtility.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DatabaseUtility.Test
{
    [TestClass]
    public class MembershipServiceTest : ServiceTestBase<MembershipService>
    {
        public MembershipServiceTest() : base(ConfigurationConstants.MembershipDatabase)
        {
        }

        [TestMethod]
        public void CreateFullPath()
        {
            Guid loginId = new Guid("9bd4ad47-656a-4dfa-b00f-322bb9c68368");
            var newAM = service.CreateAccountMaster("Softtek QA Test", true).Result;
            var newA = service.CreateAccount(newAM.Contents.Identifier).Result;
            var newC = service.CreateContact(newA.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var newU = service.CreateUser(newA.Contents.Identifier, loginId, newC.Contents.Identifier).Result;
            Assert.IsNotNull(newU);
        }
    }
}