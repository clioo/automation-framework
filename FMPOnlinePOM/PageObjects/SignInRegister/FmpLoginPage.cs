using CommonHelper.Pages.LoginPage;
using FMPOnlinePOM.Components;
using FMPOnlinePOM.PageObjects.IndexHome;
using OpenQA.Selenium;

namespace FMPOnlinePOM.PageObjects.SignInRegister
{
    public class FmpLoginPage : BaseLoginPage
    {
        public readonly FmpHeader Header;
        public FmpLoginPage(IWebDriver driver) : base(driver)
        {
            Header = new FmpHeader(driver);
        }

        public FmpIndexPage ClickOnLoginSubmit()
        {
            SubmitLoginForm();
            return new FmpIndexPage(Driver);
        }
    }
}
