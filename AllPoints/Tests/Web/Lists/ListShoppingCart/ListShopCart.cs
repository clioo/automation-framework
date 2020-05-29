using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using AllPoints.Constants;
using AllPoints.AllPoints;

namespace AllPoints.Features.Lists.ListShoppingCart
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class ListShopCart : AllPointsBaseTest
    {
        public string manufacturerOption = "";
        public string searchField = "";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void AddItemtoDefaultList()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test", "1234");

            manufacturerOption = indexPage.Header.GetManufacturerDropdownOptions().ElementAtOrDefault(2);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

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
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            manufacturerOption = indexPage.Header.GetManufacturerDropdownOptions().ElementAt(2);

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            CartMainPage.ClickAddtoListLink();

            CartMainPage.ClickAddtoListButton();

            PageObjects.ListPOM.HomePagePOM.APListHomePage listPage = CartMainPage.VisitListButton();

            Assert.IsTrue(listPage.DefaultTitlePage(), "User was not redirected to List Page");

        }
    }
}
