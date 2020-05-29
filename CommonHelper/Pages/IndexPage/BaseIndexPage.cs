using CommonHelper.BaseComponents;
using OpenQA.Selenium;

namespace CommonHelper.Pages.IndexPage
{
    public class BaseIndexPage : BasePOM
    {
        public BaseIndexPage(IWebDriver driver) : base(driver) { }

        public BaseIndexPage(IWebDriver driver, string url) : base(driver)
        {
            Driver = driver;
            Driver.Navigate().GoToUrl(url);
        }

        public BaseIndexPage(IWebDriver driver, bool init) : base(driver){ }
    }
}
