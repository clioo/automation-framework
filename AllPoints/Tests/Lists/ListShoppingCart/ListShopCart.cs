using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllPoints.PageObjects.ListPOM.HomePagePOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AllPoints.Constants;

namespace AllPoints.Features.Lists.ListShoppingCart
{
    [TestClass]
    public class ListShopCart : FeatureBase
    {
        public string manufacturerOption = "";
        public string searchField = "";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void AddItemtoDefaultList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            CartMainPage.ClickAddtoListLink();

            CartMainPage.ClickAddtoListButton();

            Assert.IsTrue(CartMainPage.SuccessMessageAdded(), "Items was not Added to any List");

            CartMainPage.ClickCloseModalLink();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        //[TestCategory(TestCategoriesConstants.NoTestData)]
        public void ValidateItemtoInDefaultList()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            CartMainPage.ClickAddtoListLink();

            CartMainPage.ClickAddtoListButton();

            PageObjects.ListPOM.HomePagePOM.ListHomePage listPage = CartMainPage.VisitListButton();

            Assert.IsTrue(listPage.DefaultTitlePage(), "User was not redirected to List Page");

        }

        


    }
}
