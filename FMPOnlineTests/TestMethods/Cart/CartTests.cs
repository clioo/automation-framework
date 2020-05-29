using FMPOnlinePOM;
using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using FMPOnlinePOM.PageObjects.Cart;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.ProductList;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FMPOnlineTests.TestMethods.Cart
{
    [TestClass]
    public class CartTests : FmpBaseTest
    {
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestMethod]
        public void CheckCartOnlinePriceTag()
        {
            string expectedTagName = "Online price";
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            var manufacturers = indexHomePage.TopHeader.GetManufacturerMenuOptionsText();
            string secondManufacturer = manufacturers.ElementAt(1);
            indexHomePage.TopHeader.SetManufacturerDropdown(secondManufacturer);
            ProductListPage productListPage = indexHomePage.TopHeader.ClickOnSubmit();
            var firstItem = productListPage.GetResultItems().FirstOrDefault();
            firstItem.ClickOnAddToCart();
            //productListPage.IsAppBusy();
            //Go back to index page
            indexHomePage = productListPage.TopHeader.ClickOnLogo();
            CartPage cartPage = indexHomePage.UtilityMenu.ClickOnCart();
            cartPage.WaitUntilItemsAreDisplayed();
            var cartItems = cartPage.GetProductItems();
            foreach (var item in cartItems)
            {
                //check each item for tag value
                Assert.AreEqual(expectedTagName, item.GetItemDetail(ProductItemDetailsEnum.PriceTag));
            }
        }
    }
}