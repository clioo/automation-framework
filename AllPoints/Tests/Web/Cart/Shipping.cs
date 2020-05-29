using AllPoints.AllPoints;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AllPoints.Features.Cart
{
    [TestClass]
    public class Shipping : AllPointsBaseTest
    {
        public string manufacturerOption = "Fay";
        public string searchField = "Soft";

        [TestMethod]
        [TestCategory("Regression")]
        public void Freeshipping_T1292()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //use a datafactory tool instead csv (shipping.csv)
            var testData = new
            {
                email = "Email",
                password = "Password",

                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",

                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber"
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(CartMainPage.totalsInformationDislayed(), "Totals are not displayed");

            Assert.IsTrue(CartMainPage.FindCongraMessage(), "Congratulation message is missing");
        }

        [TestMethod]
        [TestCategory("Regression")]        
        public void ShippingProximity_T1293()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //use a datafactory tool instead csv (shipping.csv)
            var testData = new
            {
                email = "Email",
                password = "Password",
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber"
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(CartMainPage.totalsInformationDislayed(), "Totals are not displayed");

            Assert.IsTrue(CartMainPage.proximtyMessageDisplayed(), "Proximity message is missing");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void HandlingFeeMsg()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //use a datafactory tool instead csv (shipping.csv)
            var testData = new
            {
                email = "Email",
                password = "Password",
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber"
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            //Handling Fee should be coded
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void TBD_ShippingServiceLevels()
        {
            var indexPage = new APIndexPage(Driver, Url);

            //TODO
            //test data should be improved to use a datafactory instead csv (shipping.csv)"
            var testData = new
            {
                email = "Email",
                password = "Password",
                country = "Country",
                countryShort = "CountryShort",
                address = "StreetAddress",
                state = "State",
                city = "City",
                zipCode = "ZipCode",
                apt = "Apt",
                firstname = "FirstName",
                lastname = "LastName",
                company = "Company",
                phonenumber = "PhoneNumber",
            };

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);


            indexPage.Header.SelectManufacturer(manufacturerOption);

            indexPage.Header.SetSearchFieldText(searchField);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            string TBDtext = CartMainPage.GetShippingServiceLevelsTDB();

            Assert.IsTrue("TBD".Equals(TBDtext), "TBD Message is not Present, please use another Test User");
        }
    }
}