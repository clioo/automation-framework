using FMPOnlinePOM;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.ProductDetail;
using FMPOnlinePOM.PageObjects.ProductList;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FMPOnlineTests.TestMethods.ProductDetail
{
    [TestClass]
    public class ProductDetailTest : FmpBaseTest
    {
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestMethod]
        public void CheckProductDetailOnlinePriceTag()
        {
            string expectedPriceTag = "Online price";
            IndexHomePage indexPage = new IndexHomePage(Driver, Url);
            var manufacturers = indexPage.TopHeader.GetManufacturerMenuOptionsText();
            //Get the second manufacturer
            string manufacturer = manufacturers.ElementAtOrDefault(1);
            indexPage.TopHeader.SetManufacturer(manufacturer);
            ProductListPage productListPage = indexPage.TopHeader.ClickOnSubmit();
            var productItem = productListPage.GetResultItems().FirstOrDefault();
            ProductDetailPage productDetailPage = productItem.Click();
            string actualPriceTag = productDetailPage.GetProductSection(ProductDetailSections.PriceTag);
            Assert.AreEqual(expectedPriceTag, actualPriceTag);
        }
    }
}
