using AllPoints.Constants;
using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages;
using AllPointsPOM.PageObjects.QuickOrderPOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AllPoints.AllPoints.Components
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    [TestCategory(TestCategoriesConstants.ReadyToGo)]
    public class UtilityMenuTest : AllPointsBaseTest
    {
        [TestMethod]
        public void CheckPhoneNumber()
        {
            string expectedPhoneNumber = "1.800.332.2500";

            APIndexPage homePage = new APIndexPage(Driver, Url);
            string actualPhoneNumber = homePage.Header.GetPhoneNumber();

            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [TestMethod]
        public void ClickAllMainOptionsAnonymous()
        {
            string expectedLoginPageTitle = "Login";
            //string expectedTrackOrderPageTitle = "Track Order";
            string expectedQuickOrderPageTitle = "QuickOrder";
            string expectedCartPageTitle = "Cart";

            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            Assert.AreEqual(expectedLoginPageTitle, Driver.Title);

            //TODO
            //TrackOrderPage
            //Assert

            QuickOrdersHomePage quickOrderPage = loginPage.Header.ClickOnQuickOrder();
            Assert.AreEqual(expectedQuickOrderPageTitle, Driver.Title);

            APCartPage cartPage = quickOrderPage.Header.ClickOnViewCart();
            Assert.AreEqual(expectedCartPageTitle, Driver.Title);
        }

        //TODO
        //pending to done
        //[TestMethod]
        public void NavigateThroughMyAccountMenuOptions()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            //TODO
            //Account dashboard

            //Contact information

            //AddressesHomePage addressesPage = paymentsPage.Header.ClickOnAddresses();
            //PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //Password
            //OrdersHomePage ordersPage = addressesPage.Header.ClickOnOrders();
            //Lists

            //TODO
            //validations
            //check page titles
        }
    }
}
