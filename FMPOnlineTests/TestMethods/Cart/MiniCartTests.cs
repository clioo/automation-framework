using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FMPOnlineTests.TestMethods.Cart
{
    [TestClass]
    public class MiniCartTests : FmpBaseTest
    {
        //Test case in test rail -> C1765
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestMethod]
        public void MiniCartOnlinePriceTagIsCorrect()
        {
            string actualPricingTag = string.Empty;
            string expectedPricingTag = "Online price";
            IndexHomePage indexPage = new IndexHomePage(Driver, Url);
            indexPage.UtilityMenu.ShowMiniCart();
            actualPricingTag = indexPage.UtilityMenu.MiniCart.GetSection(MiniCartSections.PricingTag);

            Assert.AreEqual(expectedPricingTag, actualPricingTag, "Actual tag '{0}' is not as expected", actualPricingTag);
        }
    }
}