using AllPoints.PageObjects.CartPOM;
using AllPoints.Pages.Components;
using CommonHelper;
using CommonHelper.Pages.LoginPage;
using OpenQA.Selenium;
using System.Threading;

namespace AllPoints.Pages
{
    public class APLoginPage : BaseLoginPage
    {
        public Header Header;

        public AccountMenuLeft AccountMenuLeft;

        protected GenericPage Helper;

        public APLoginPage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            Helper = new GenericPage(driver);
            this.detailSingInPage.Init(driver, SeleniumConstants.defaultWaitTime);
            this.InitializeWebElements();
        }

        #region login form
        private DomElement emailField = new DomElement()
        {
            locator = "LoginUsername"
        };

        private DomElement passwordField = new DomElement()
        {
            locator = "LoginPassword"
        };

        private DomElement loginButton = new DomElement()
        {
            locator = "//*[@class='btn btn-primary'][text()='Sign in']"
        };
        #endregion

        #region new register
        //Register section elements

        //firstNameField
        //lastNameField
        //emailField
        #endregion

        //Checkout alternative Path
        private DomElement detailSingInPage = new DomElement(By.CssSelector)
        {
            locator = ".row"
        };

        private DomElement lastchanceLogin = new DomElement
        {
            locator = ".hide-on-signin"
        };

        private DomElement clickasguestbutton = new DomElement()
        {
            locator = ".btn"
        };

        #region constructor (not valid)
        //public LoginPage(IWebDriver driver) : base (driver)
        //{             
        //    this.InitializeWebElements();
        //}
        #endregion


        private void InitializeWebElements()
        {
            //Selectors and locators declarations here

            emailField.findsBy = By.Id(emailField.locator);

            passwordField.findsBy = By.Id(passwordField.locator);

            loginButton.findsBy = By.XPath(loginButton.locator);

            //Get the web elements by the generic helper


            emailField.webElement = Helper.GetElementWait(emailField.findsBy);

            passwordField.webElement = Helper.GetElementWait(passwordField.findsBy);

            loginButton.webElement = Helper.GetElementWait(loginButton.findsBy);
        }


        public APIndexPage Login(string email, string password)
        {
            emailField.webElement.SendKeys(email);

            passwordField.webElement.SendKeys(password);

            //Bug in login!
            Thread.Sleep(1500);

            loginButton.webElement.Click();

            if (!LoginIsSuccess()) throw new System.Exception("Login failed");

            return new APIndexPage(Driver, true);
        }

        public APCheckoutPage clickOnGuestbutton()
        {
            DomElement detailCheckoutGuess = detailSingInPage.GetElementWaitByCSS(lastchanceLogin.locator);
            DomElement checkOutasGuestbtn = detailCheckoutGuess.GetElementWaitByCSS(clickasguestbutton.locator);
            checkOutasGuestbtn.webElement.Click();
            return new APCheckoutPage(Driver);
        }
    }
}