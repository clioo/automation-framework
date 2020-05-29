using AllPoints.Constants;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllPoints.Features.MyAccount.Components
{
    [TestClass]
    public class AccountMenuLeftTests : FeatureBase
    {
        //Test case on test rail -> C1362
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void CheckAccountNumber()
        {
            string user = "test";
            string pass = "1234";
            string expectedAccountNumber = string.Empty;

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user, pass);

            ContactInfoHomePage addressesPage = indexPage.Header.ClickOnContactInfo();
            expectedAccountNumber = addressesPage.GetAccountNumber();

            Assert.IsNotNull(expectedAccountNumber, "Account number is null");
            Assert.IsFalse(string.IsNullOrEmpty(expectedAccountNumber));
            Assert.IsFalse(string.IsNullOrWhiteSpace(expectedAccountNumber));

            System.Console.WriteLine(expectedAccountNumber);
        }
    }
}