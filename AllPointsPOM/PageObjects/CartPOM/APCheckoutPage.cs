using AllPoints.PageObjects.NewFolder1;
using AllPoints.PageObjects.OderConfirmPOM;
using CommonHelper;
using CommonHelper.Pages.CartPage;
using CommonHelper.Pages.CartPage.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.CartPOM
{
    public class APCheckoutPage : BaseCheckoutPage
    {
        #region Contructor

        public APCheckoutPage(IWebDriver driver) : base(driver)
        {
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        #endregion Contructor

        public new OrderConfirmationPage PlaceOrderSubmitClick()
        {
            base.PlaceOrderSubmitClick();
            return new OrderConfirmationPage(Driver);
        }
    }
}