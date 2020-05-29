using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AllPoints.Features.MyAccount.Dashbord
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    [TestCategory(TestCategoriesConstants.ReadyToGo)]
    public class Dashboard : AllPointsBaseTest
    {
        #region View
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateDashboardIsDislayed_C1154()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            var indexPage = new APIndexPage(Driver, Url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate that it is de correct page
            Assert.IsTrue(dashboardHomePage.DashboardTitleExist(), "Dashboard title does not exist");
            Assert.IsTrue(dashboardHomePage.DashboardTitleTextIsCorrect("Account dashboard"), "Dashboard title is incorrect");
        }

        #endregion View

        #region Contact information

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateIsContactInformationInDashboard_C1155()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            var indexPage = new APIndexPage(Driver, Url);
            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate that exist the section
            Assert.IsTrue(dashboardHomePage.ContactInfoExist(), "Contact Information does not exist on Dashboard");
        }

        //[TestMethod]
        //public void ContactInfo_Click_URL_Redirect()
        //{
        //    var indexPage = new IndexPage(Driver, Url);

        //    var loginPage = indexPage.GoToLogin();

        //    indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

        //    var dashboardHomePage = indexPage.Header.ClickOnDashboard();

        //    var clickedPage = dashboardHomePage.ClickContactInfoLink();
        //    bool clickedAndRedirected = clickedPage && Driver.Url.Contains("OrderDetail");
        //    bool sectionNotExist = !clickedPage && !Driver.Url.Contains("OrderDetail");

        //    Assert.IsTrue(clickedAndRedirected || sectionNotExist, "Element Exist but there is no redirection");
        //}

        #endregion Contact information

        #region Addresses

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateIsAddressesInDashboard_C1157()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            var indexPage = new APIndexPage(Driver, Url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate that exist the section
            Assert.IsTrue(dashboardHomePage.AddressesExist(), "Contact Information does not exist on Dashboard");
        }

        #endregion Addresses

        #region Recent Orders

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateIsRecentOrdersInDashboard_C1348()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            var indexPage = new APIndexPage(Driver, Url);
            var loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate that exist the section
            Assert.IsTrue(dashboardHomePage.RecentOrdersExist(), "Recent Orders does not exist on Dashboard");
        }

        //[TestMethod]
        public void ValidateAreRecentOrders_C1349()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            var indexPage = new APIndexPage(Driver, Url);
            var loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate recent orders
            Assert.IsTrue(dashboardHomePage.AreRecentOrders(), "There are not Recent orders");
            //To Do
            //Assert.AreEqual(dashboardHomePage.FiveRecentOrders(), orderHomePage.LastRecentOrders(), "This are not the recent orders");
            // Assert.IsTrue(dashboardHomePage.AreRecentOrders(), "There are not rRecent orders");
        }

        //[TestMethod]
        //public void OrderDetail_Click_URL_Redirect()
        //{
        //    var indexPage = new IndexPage(Driver, Url);

        //    var loginPage = indexPage.GoToLogin();

        //    indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

        //    var dashboardHomePage = indexPage.Header.ClickOnDashboard();

        //    var clickedPage = dashboardHomePage.ClickOrderDetail();
        //    bool clickedAndRedirected = clickedPage && Driver.Url.Contains("OrderDetail");
        //    bool sectionNotExist = !clickedPage && !Driver.Url.Contains("OrderDetail");

        //    Assert.IsTrue(clickedAndRedirected || sectionNotExist, "Element Exist but there is no redirection");
        //}

        #endregion Recent Orders

        #region Payment options

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateIsPaymentOptionsInDashboard_C1159()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            var indexPage = new APIndexPage(Driver, Url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            var dashboardHomePage = indexPage.Header.ClickOnDashboard();

            //Validate that exist the section
            Assert.IsTrue(dashboardHomePage.PaymentOptionsExist(), "Contact Information does not exist on Dashboard");
        }

        #endregion Payment options

    }
}