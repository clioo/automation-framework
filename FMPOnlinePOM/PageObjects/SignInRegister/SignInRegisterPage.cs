using CommonHelper;
using FMPOnlinePOM.Base;
using FMPOnlinePOM.PageObjects.Checkout;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPOnlinePOM.PageObjects.SignInRegister
{
    public class SignInRegisterPage : FmpBasePage
    {
        DomElement SignSectionContainer = new DomElement
        {
            locator = ".signin-section.container"
        };
        DomElement GuestSectionContainer = new DomElement
        {
            locator = ".row > div:nth-of-type(2)"
        };
        DomElement GuestSectionContainerSubmitButton = new DomElement
        {
            locator = ".btn"
        };

        #region constructor
        public SignInRegisterPage(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public CheckoutPage ClickOnCheckoutAsGuest()
        {
            DomElement signInSectionContainer = BodyContainer.GetElementWaitByCSS(SignSectionContainer.locator);
            DomElement guestSectionContainer = signInSectionContainer.GetElementWaitByCSS(GuestSectionContainer.locator);
            DomElement submitButton = guestSectionContainer.GetElementWaitByCSS(GuestSectionContainerSubmitButton.locator);
            submitButton.webElement.Click();
            return new CheckoutPage(Driver);
        }
    }
}
