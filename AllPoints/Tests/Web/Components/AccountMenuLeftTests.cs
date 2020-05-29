using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllPoints.Features.MyAccount.Components
{
    [TestClass]
    public class AccountMenuLeftTests : AllPointsBaseTest
    {
        //Test case on test rail -> C1362
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void CheckAccountNumber()
        {
            string user = "test";
            string pass = "1234";
            string expectedAccountNumber = string.Empty;

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user, pass);

            ContactInfoHomePage addressesPage = indexPage.Header.ClickOnContactInfo();
            expectedAccountNumber = addressesPage.AccountMenuLeft.GetAccountNumber();

            Assert.IsNotNull(expectedAccountNumber, "Account number is null");
            Assert.IsFalse(string.IsNullOrEmpty(expectedAccountNumber));
            Assert.IsFalse(string.IsNullOrWhiteSpace(expectedAccountNumber));

            System.Console.WriteLine(expectedAccountNumber);
        }
    }
}