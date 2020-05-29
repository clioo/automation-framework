using AllPoints.Features;
using AllPoints.Features.Cart;
using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages;
using AllPointsPOM.PageObjects.QuickOrderPOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllPoints.Tests.QuickOrders
{
    [TestClass]
    public class QuickOrderTest : FeatureBase
    {
        private CartDataFactory dataFactory;

        public string AllPointsNumber = "U92586";
        public string Qty = "2";

        public QuickOrderTest()
        {
            dataFactory = new CartDataFactory();
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Validate_ItemAddedToCart()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            var user = dataFactory.CreateLoginAccount();

            QuickOrdersHomePage QuickOrderPage = indexPage.Header.ClickOnQuickOrder();

            QuickOrderPage.TypeAllPointsNumber(AllPointsNumber);

            QuickOrderPage.TypeQty(Qty);

            CartPage cartHomePage = QuickOrderPage.ClickAddToCart();

            Assert.IsTrue(cartHomePage.SKUSectionIsDisplayed(), "No Items added in Cart");
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void Validate_YourPriceLabel()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            var user = dataFactory.CreateLoginAccount();

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user.Email, user.Password);

            QuickOrdersHomePage QuickOrderPage = indexPage.Header.ClickOnQuickOrder();

            QuickOrderPage.TypeAllPointsNumber(AllPointsNumber);

            QuickOrderPage.TypeQty(Qty);

            Assert.IsTrue(QuickOrderPage.Validate_YourPriceLabelIsDisplayed(),"Your Price is not Dispalyed");
        }


    }
}
