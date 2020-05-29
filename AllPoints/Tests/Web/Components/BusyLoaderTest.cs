using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace AllPoints.AllPoints.Web.Components
{
    [TestClass]
    public class BusyLoaderTest : AllPointsBaseTest
    {
        [TestMethod]
        public void HandleBusyApp()
        {
            APIndexPage homePage = new APIndexPage(Driver, Url);
            var cartPage = homePage.Header.ClickOnViewCart();

            //make busy app appear for 5 seconds then quit
            string script = "setTimeout(() => { document.querySelector('app-busy-indicator div').classList.add('ng-hide'); }, 5000); document.querySelector('app-busy-indicator div').classList.remove('ng-hide')";

            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)Driver;

            scriptExecutor.ExecuteScript(script);
            cartPage.WaitForAppBusy();
        }
    }
}
