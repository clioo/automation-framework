using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.Features.Cart;
using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages;
using AllPointsPOM.PageObjects.QuickOrderPOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AllPoints.Tests.QuickOrders
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class QuickOrderTest : AllPointsBaseTest
    {
        public string AllPointsNumber = "U92586";
        public string Qty = "2";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Validate_ItemAddedToCart()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            var testUser = DataFactory.Users.CreateTestUser();

            QuickOrdersHomePage QuickOrderPage = indexPage.Header.ClickOnQuickOrder();

            QuickOrderPage.TypeAllPointsNumber(AllPointsNumber);

            QuickOrderPage.TypeQty(Qty);

            APCartPage cartHomePage = QuickOrderPage.ClickAddToCart();

            Assert.IsTrue(cartHomePage.SKUSectionIsDisplayed(), "No Items added in Cart");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Validate_YourPriceLabel()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            var user = DataFactory.Users.CreateTestUser();

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user.Username, user.Password);

            QuickOrdersHomePage QuickOrderPage = indexPage.Header.ClickOnQuickOrder();

            QuickOrderPage.TypeAllPointsNumber(AllPointsNumber);

            QuickOrderPage.TypeQty(Qty);

            Assert.IsTrue(QuickOrderPage.Validate_YourPriceLabelIsDisplayed(), "Your Price is not Displayed");
        }
    }
}
