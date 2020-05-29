using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.ProductList;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FMPOnlineTests.TestMethods.ProductList
{
    [TestClass]
    public class ProductListTests : FmpBaseTest
    {
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestMethod]
        public void CheckProductListOnlinePriceTag()
        {
            string expectedPriceTag = "Online price";
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            var manufacturers = indexHomePage.TopHeader.GetManufacturerMenuOptionsText();
            string manufacturer = manufacturers.ElementAt(1);
            indexHomePage.TopHeader.SetManufacturer(manufacturer);
            ProductListPage productListPage = indexHomePage.TopHeader.ClickOnSubmit();
            var productItems = productListPage.GetResultItems();

            var incorrectPriceTag = productItems.FirstOrDefault(p => p.GetItemDetail(ProductItemDetailsEnum.PriceTag) != expectedPriceTag);
            Assert.IsNull(incorrectPriceTag, $"Theres an invalid Price tag on {manufacturer}");
        }
    }
}
