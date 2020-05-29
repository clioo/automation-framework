using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllPoints.Constants;
using System.Linq;
using AllPoints.AllPoints;

namespace AllPoints.Features.Offering
{
    [TestClass]
    public class OfferingProducts : AllPointsBaseTest
    {
        public string manufacturerOption = "";
        public string searchField = "";

        //TODO:
        //[TestMethod]
        //public void ProductWithProp65Message

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        //[TestCategory(TestCategoriesConstants.NoTestData)]
        public void ValidateListPriceLabelforANON()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //replace on the new GetManufacturerDropdownOptions
            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.ListPriceLabelforANON();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\offering.csv", "offering#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\offering.csv")]
        public void ValidateYourPriceLabelforAUTH()
        {
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                email = (string)TestContext.DataRow["Email"],
                password = TestContext.DataRow["Password"].ToString(),
                country = (string)TestContext.DataRow["Country"],
                countryShort = (string)TestContext.DataRow["CountryShort"],
                address = (string)TestContext.DataRow["StreetAddress"],
                state = (string)TestContext.DataRow["State"],
                city = (string)TestContext.DataRow["City"],
                zipCode = TestContext.DataRow["ZipCode"].ToString(),
                apt = TestContext.DataRow["Apt"].ToString(),
                firstname = (string)TestContext.DataRow["FirstName"],
                lastname = (string)TestContext.DataRow["LastName"],
                company = (string)TestContext.DataRow["Company"],
                phonenumber = TestContext.DataRow["PhoneNumber"].ToString(),
                attn = TestContext.DataRow["ATTN"].ToString(),
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

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
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                email = (string)TestContext.DataRow["Email"],
                password = TestContext.DataRow["Password"].ToString(),
                country = (string)TestContext.DataRow["Country"],
                countryShort = (string)TestContext.DataRow["CountryShort"],
                address = (string)TestContext.DataRow["StreetAddress"],
                state = (string)TestContext.DataRow["State"],
                city = (string)TestContext.DataRow["City"],
                zipCode = TestContext.DataRow["ZipCode"].ToString(),
                apt = TestContext.DataRow["Apt"].ToString(),
                firstname = (string)TestContext.DataRow["FirstName"],
                lastname = (string)TestContext.DataRow["LastName"],
                company = (string)TestContext.DataRow["Company"],
                phonenumber = TestContext.DataRow["PhoneNumber"].ToString(),
                attn = TestContext.DataRow["ATTN"].ToString(),
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

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
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                email = (string)TestContext.DataRow["Email"],
                password = TestContext.DataRow["Password"].ToString(),
                country = (string)TestContext.DataRow["Country"],
                countryShort = (string)TestContext.DataRow["CountryShort"],
                address = (string)TestContext.DataRow["StreetAddress"],
                state = (string)TestContext.DataRow["State"],
                city = (string)TestContext.DataRow["City"],
                zipCode = TestContext.DataRow["ZipCode"].ToString(),
                apt = TestContext.DataRow["Apt"].ToString(),
                firstname = (string)TestContext.DataRow["FirstName"],
                lastname = (string)TestContext.DataRow["LastName"],
                company = (string)TestContext.DataRow["Company"],
                phonenumber = TestContext.DataRow["PhoneNumber"].ToString(),
                attn = TestContext.DataRow["ATTN"].ToString(),
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

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
            var indexPage = new APIndexPage(Driver, Url);

            var testData = new
            {
                email = (string)TestContext.DataRow["Email"],
                password = TestContext.DataRow["Password"].ToString(),
                country = (string)TestContext.DataRow["Country"],
                countryShort = (string)TestContext.DataRow["CountryShort"],
                address = (string)TestContext.DataRow["StreetAddress"],
                state = (string)TestContext.DataRow["State"],
                city = (string)TestContext.DataRow["City"],
                zipCode = TestContext.DataRow["ZipCode"].ToString(),
                apt = TestContext.DataRow["Apt"].ToString(),
                firstname = (string)TestContext.DataRow["FirstName"],
                lastname = (string)TestContext.DataRow["LastName"],
                company = (string)TestContext.DataRow["Company"],
                phonenumber = TestContext.DataRow["PhoneNumber"].ToString(),
                attn = TestContext.DataRow["ATTN"].ToString(),
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

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