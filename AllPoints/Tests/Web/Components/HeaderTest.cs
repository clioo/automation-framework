using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllPoints.Pages;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.AllPoints;
using System.Linq;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.MyAccountPOM;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.PageObjects.MyAccountPOM.DashboardPOM;
using System.Threading;
using AllPoints.PageObjects.CartPOM;
using AllPoints.Constants;

namespace AllPoints.Features
{
    [TestClass]
    public class HeaderTest : AllPointsBaseTest
    {
        #region account menu
        [TestMethod]
        public void ClickOnSignInPage()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");
        }

        [TestMethod]
        public void MyAccountMenuOptionsNavigate()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();

            AddressesHomePage addressesPage = paymentsPage.Header.ClickOnAddresses();

            OrdersHomePage ordersPage = addressesPage.Header.ClickOnOrders();

            ContactInfoHomePage contactInfoPage = ordersPage.Header.ClickOnContactInfo();

            //pavito here
            //TODO:
            //Fix the dashboard page pls
            DashboardHomePage dashboardPage = contactInfoPage.Header.ClickOnDashboard();

            indexPage = dashboardPage.Header.ClickOnSignOut();

            Thread.Sleep(1500);
        }

        [TestMethod]
        public void ClickOnCart()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APCartPage cartPage = indexPage.Header.ClickOnViewCart();

            Thread.Sleep(1500);
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.AllPoints)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AllPointsPhoneNumberOnUtilityMenu()
        {
            string expectedPhoneNumber = "1.800.332.2500";

            APIndexPage indexHomePage = new APIndexPage(Driver, Url);
            string actualAllpointsPhoneNumber = indexHomePage.Header.GetPhoneNumber();

            Assert.IsFalse(string.IsNullOrWhiteSpace(actualAllpointsPhoneNumber), "Phone number is empty");
            Assert.AreEqual(expectedPhoneNumber, actualAllpointsPhoneNumber);
        }
        #endregion

        #region search section
        [TestMethod]
        public void SelectAndSearchByManuFacturer()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            indexPage.Header.SelectManufacturer("Block");
            indexPage.Header.SetSearchFieldText("plastic");

            CatalogItemsPage catalogPage = indexPage.Header.ClickOnSearchButton();
        }

        [TestMethod]
        public void SelectManufacturerByName()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            var manufacturers = indexPage.Header.GetManufacturerDropdownOptions();
            indexPage.Header.SelectManufacturer(manufacturers.ElementAt(2));
        }

        [TestMethod]
        public void GetManufacturerOptions()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            var manufacturerOptions = indexPage.Header.GetManufacturerOptions();

            Assert.IsNotNull(manufacturerOptions);
        }
        #endregion

        #region categories menu
        //[TestMethod]
        public void ClickAnyParentCategory()
        {

        }
        #endregion
    }
}
