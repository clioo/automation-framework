using AllPoints.Constants;
using AllPoints.Features.BaseTest.Constants;
using AllPoints.Pages;
using AllPoints.Tests.BaseTest;
using AllPoints.Tests.BaseTest.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllPoints.Tests.BrowserstackUatTests
{
    [TestClass]
    public class HomePageTests : BaseBrowserStackFeature
    {
        //Test case on test rail -> "Pending"
        //[DataTestMethod]
        [DataRow(BStackDevicesConstants.WindowsChrome)]
        [DataRow(BStackDevicesConstants.MacSafari)]
        [DataRow(BStackDevicesConstants.WindowsIExplorer)]
        [DataRow(BStackDevicesConstants.WindowsFirefox)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void CheckAllPointsPhoneNumber(string browser)
        {
            Driver = GetDriverInstance(browser);
            string expectedPhoneNumber = "1.800.332.2500";
            IndexPage indexHhomePage = new IndexPage(Driver, Url);
            string actualAllpointsPhoneNumber = indexHhomePage.Header.GetPhoneNumber();
            Assert.IsFalse(string.IsNullOrWhiteSpace(actualAllpointsPhoneNumber), "Phone number is empty");
            Assert.AreEqual(expectedPhoneNumber, actualAllpointsPhoneNumber);
        }
    }
}