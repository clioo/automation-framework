using CommonHelper.BaseComponents;
using CommonHelper.Pages.LoginPage.Models;
using OpenQA.Selenium;

namespace CommonHelper.Pages.LoginPage
{
    public abstract class BaseLoginPage : BasePOM
    {
        protected DomElement SignInSectionContainer = new DomElement
        {
            locator = ".signin-section.container"
        };
        //login form locators
        protected DomElement LoginContainer = new DomElement
        {
            locator = "div .login-tab-1"
        };
        protected DomElement ErrorMessage = new DomElement
        {
            locator = "div.alert.alert-danger"
        };
        protected DomElement EmailInput = new DomElement
        {
            locator = "#LoginUsername"
        };
        protected DomElement PasswordInput = new DomElement
        {
            locator = "#LoginPassword"
        };
        protected DomElement LoginSubmitButton = new DomElement
        {
            locator = "button[type='submit']"
        };
        //guest locators
        protected DomElement GuestSectionContainer = new DomElement
        {
            locator = ".row > div:nth-of-type(2)"
        };
        protected DomElement GuestSubmitButton = new DomElement
        {
            locator = ".btn"
        };

        public BaseLoginPage(IWebDriver driver) : base(driver)
        {
            
        }

        public virtual string GetLoginErrorMessage()
        {
            DomElement signInPageContainer = BodyContainer.GetElementWaitByCSS(SignInSectionContainer.locator);
            DomElement loginContainer = signInPageContainer.GetElementWaitByCSS(LoginContainer.locator);
            bool loginErrorExist = loginContainer.IsElementPresent(ErrorMessage.locator);

            //locate the error message element only if is displayed in order to avoid selenium exceptions
            if (loginErrorExist)
            {
                return loginContainer.GetElementWaitByCSS(ErrorMessage.locator).webElement.Text;
            }
            return null;
        }

        public virtual bool LoginIsSuccess()
        {
            bool isSuccess;
            try
            {
                IWebElement errorMessage = Driver.FindElement(By.CssSelector(ErrorMessage.locator));
                isSuccess = false;
            }
            catch (NoSuchElementException)
            {
                isSuccess = true;
            }
            return isSuccess;
        }

        public virtual void SetLoginCredentials(LoginCredential loginCredentials)
        {
            DomElement signInPageContainer = BodyContainer.GetElementWaitByCSS(SignInSectionContainer.locator);
            DomElement loginContainer = signInPageContainer.GetElementWaitByCSS(LoginContainer.locator);
            DomElement emailField = loginContainer.GetElementWaitByCSS(EmailInput.locator);
            DomElement passwordField = loginContainer.GetElementWaitByCSS(PasswordInput.locator);
            emailField.webElement.SendKeys(loginCredentials.User);
            passwordField.webElement.SendKeys(loginCredentials.Password);
            //TODO:
            //use a selenium wait to replace this Sleep
            System.Threading.Thread.Sleep(1900);
        }

        protected virtual void GuestButtonSubmit()
        {
            DomElement signInPageContainer = BodyContainer.GetElementWaitByCSS(SignInSectionContainer.locator);
            DomElement guestSectionContainer = signInPageContainer.GetElementWaitByCSS(GuestSectionContainer.locator);
            DomElement guestButton = guestSectionContainer.GetElementWaitByCSS(GuestSubmitButton.locator);
            guestButton.webElement.Click();
        }

        protected virtual void SubmitLoginForm()
        {
            DomElement signInPageContainer = BodyContainer.GetElementWaitByCSS(SignInSectionContainer.locator);
            DomElement loginContainer = signInPageContainer.GetElementWaitByCSS(LoginContainer.locator);
            DomElement loginSubmit = loginContainer.GetElementWaitByCSS(LoginSubmitButton.locator);
            loginSubmit.webElement.Click();

            string errMessage = null;

            //if page url is currently the login page
            if (Driver.Url.Contains("SignIn"))
            {
                errMessage = GetLoginErrorMessage();
            }
            
            if (!string.IsNullOrEmpty(errMessage) || !string.IsNullOrWhiteSpace(errMessage))
            {
                throw new System.Exception("Login failed");
            }
        }
    }
}
