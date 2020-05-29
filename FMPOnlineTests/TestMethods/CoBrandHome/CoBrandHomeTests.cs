using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.IndexHome.Enums;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FMPOnlineTests.TestMethods.CoBrand
{
    [TestClass]
    public class CoBrandHomePageTests : FmpBaseTest
    {
        //Test Case on test rail -> C1714
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void TopCoBrandContentNotVisibleForAnonymousUsers()
        {
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            string CoBrandTopContent = indexHomePage.GetCoBrandSectionText(IndexHomePageSectionsEnum.Top);

            Assert.IsTrue(string.IsNullOrEmpty(CoBrandTopContent), "Anonymous user should not be able to see CoBrand content -> top zone");
        }

        //Test Case on test rail -> C1730
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void BottomCoBrandContentNotVisibleForAnonymousUsers()
        {
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            string CoBrandBottomContent = indexHomePage.GetCoBrandSectionText(IndexHomePageSectionsEnum.Bottom);

            Assert.IsTrue(string.IsNullOrEmpty(CoBrandBottomContent), "Anonymous user should not be able to see CoBrand content -> bottom zone");
        }
    }
}
