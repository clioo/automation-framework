using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.IndexHome.Enums;
using FMPOnlineTests.Constants;
using FMPOnlineTests.TestMethods.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace FMPOnlineTests.TestMethods.BrowserstackUatTests.Web.CoBrandHomePage
{
    [TestClass]
    public class HomePageTests : BaseBrowserstackFeature
    {
        //Test Case on test rail -> C1714
        //[DataTestMethod]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void TopCoBrandContentNotVisibleForAnonymousUsers(string browser)
        {
            Driver = GetDriverInstance(browser);
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            string CoBrandTopContent = indexHomePage.GetCoBrandSectionText(IndexHomePageSectionsEnum.Top);
            Assert.IsTrue(string.IsNullOrWhiteSpace(CoBrandTopContent), "Anonymous user should not be able to see CoBrand content -> top zone");
        }

        //Test Case on test rail -> C1730
        //[DataTestMethod]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void BottomCoBrandContentNotVisibleForAnonymousUsers(string browser)
        {
            Driver = GetDriverInstance(browser);
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            string CoBrandBottomContent = indexHomePage.GetCoBrandSectionText(IndexHomePageSectionsEnum.Bottom);
            Assert.IsTrue(string.IsNullOrWhiteSpace(CoBrandBottomContent), "Anonymous user should not be able to see CoBrand content -> bottom zone");
        }

        //[DataTestMethod]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void CheckFmpPhoneNumber(string browser)
        {
            string expectedPhoneNumber = "1.800.257.7737";
            Driver = GetDriverInstance(browser);
            IndexHomePage indexHomePage = new IndexHomePage(Driver, Url);
            string actualPhoneNumber = indexHomePage.UtilityMenu.GetPhoneNumber();
            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }
    }
}