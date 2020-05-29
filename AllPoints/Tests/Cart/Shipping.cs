using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading;

namespace AllPoints.Features.Cart
{
    [TestClass]
    public class Shipping : FeatureBase
    {
        public string manufacturerOption = "Fay";
        public string searchField = "Soft";

        [ClassInitialize]
        public static void InitClassSuite(TestContext tstContext)
        {
            testContext = tstContext;
        }

        [TestMethod]
        [TestCategory("Regression")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\shipping.csv", "shipping#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\shipping.csv")]
        public void Freeshipping_T1292()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                password = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(CartMainPage.totalsInformationDislayed(), "Totals are not displayed");

            Assert.IsTrue(CartMainPage.FindCongraMessage(), "Congratulation message is missing");
        }

        [TestMethod]
        [TestCategory("Regression")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\shipping.csv", "shipping#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\shipping.csv")]
        public void ShippingProximity_T1293()
        {
            var indexPage = new IndexPage(driver, url);
           
            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                password = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(CartMainPage.totalsInformationDislayed(), "Totals are not displayed");

            Assert.IsTrue(CartMainPage.proximtyMessageDisplayed(), "Proximity message is missing");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\shipping.csv", "shipping#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\shipping.csv")]
        public void HandlingFeeMsg()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                password = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            //Handling Fee should be coded

        }

        [TestMethod]
        [TestCategory("Regression")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\shipping.csv", "shipping#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\shipping.csv")]
        public void TBD_ShippingServiceLevels()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                password = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);


            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            string TBDtext = CartMainPage.GetShippingServiceLevelsTDB();

            Assert.IsTrue("TBD".Equals(TBDtext), "TBD Message is not Present, please use another Test User");
        }
    }
}