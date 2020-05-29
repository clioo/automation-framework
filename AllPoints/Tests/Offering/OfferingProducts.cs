using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using AllPoints.Constants;
using System.Linq;

namespace AllPoints.Features.Offering
{
    [TestClass]
    public class OfferingProducts : FeatureBase
    {
        public string manufacturerOption = "";
        public string searchField = "";

        [ClassInitialize]
        public static void InitClassSuite(TestContext tstContext)
        {
            testContext = tstContext;
        }


        #region Prop65

        //TODO:
        //[TestMethod]
        public void ProductContainsDangerousChemicals()
        {
        }

        //TODO:
        //[TestMethod]
        //[TestCategory(CategoriesConstants.Regression)]
        public void ProductWithNoDangerousChemicals()
        {
            var testData = new
            {
                email = testContext.DataRow["email"].ToString(),
                password = testContext.DataRow["password"].ToString(),
                sku = testContext.DataRow["sku"].ToString()
            };

            IndexPage indexPage = new IndexPage(driver);

            //indexPage
        }

        #endregion Prop65

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        //[TestCategory(TestCategoriesConstants.NoTestData)]
        public void ValidateListPriceLabelforANON()
        {
            var indexPage = new IndexPage(driver, url);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.ListPriceLabelforANON();
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\offering.csv", "offering#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\offering.csv")]
        public void ValidateYourPriceLabelforAUTH()
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
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.YourPriceLabelforAUTH();
        }

        [TestMethod]
        [TestCategory("Regression")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\offering.csv", "offering#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\offering.csv")]
        public void ValidateSavingsMsg()
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
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.SavingsMsgDisplayed();

        }

        [TestMethod]
        [TestCategory("Regression")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\offering.csv", "offering#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\offering.csv")]
        public void ValidateAddToCartinOffering()
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
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.AddtoCartInOffering();
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\offering.csv", "offering#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\offering.csv")]
        public void ValidateProductSectionsQUTH()
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
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.ShippingInfoSectionAvailable();

            //offeringProductpage.AvailabilityInfo();

            offeringProductpage.DescriptionAvailableSection();

            offeringProductpage.SpecificationsSection();

        }
    }
}