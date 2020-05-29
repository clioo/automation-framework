using FMPOnlinePOM;
using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using FMPOnlinePOM.PageObjects.Cart;
using FMPOnlinePOM.PageObjects.Checkout;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.ProductList;
using FMPOnlinePOM.PageObjects.SignInRegister;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FMPOnlineTests.TestMethods.Checkout
{
    [TestClass]
    public class CheckoutTests : FmpBaseTest
    {
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestMethod]
        public void CheckPriceTagOnCheckoutPage()
        {
            string expectedPriceTag = "Online price";
            IndexHomePage indexPage = new IndexHomePage(Driver, Url);
            string firstManufacturer = indexPage.TopHeader.GetManufacturerMenuOptionsText().ElementAtOrDefault(1);
            indexPage.TopHeader.SetManufacturerDropdown(firstManufacturer);
            ProductListPage productsListPage = indexPage.TopHeader.ClickOnSubmit();
            var firstProductItem = productsListPage.GetResultItems().FirstOrDefault();
            firstProductItem.ClickOnAddToCart();
            productsListPage.WaitForAppBusy();
            indexPage = productsListPage.TopHeader.ClickOnLogo();
            CartPage cartPage = indexPage.UtilityMenu.ClickOnCart();
            cartPage.WaitUntilItemsAreDisplayed();
            SignInRegisterPage signInPage = cartPage.ClickOnProceedToCheckoutButtonAnonymous();
            CheckoutPage checkoutPage = signInPage.ClickOnCheckoutAsGuest();
            checkoutPage.WaitUntilItemsAreDisplayed();
            var productItems = checkoutPage.GetProductItems();

            foreach(var product in productItems)
            {
                System.Console.WriteLine(product.GetItemDetail(ProductItemDetailsEnum.PriceTag));
                Assert.AreEqual(expectedPriceTag, product.GetItemDetail(ProductItemDetailsEnum.PriceTag));
            }
        }
    }
}