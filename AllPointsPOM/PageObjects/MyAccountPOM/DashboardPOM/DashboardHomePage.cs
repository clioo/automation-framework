using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.NewFolder1;
using AllPoints.Pages;
using CommonHelper;
using OpenQA.Selenium;
using System.Linq;

namespace AllPoints.PageObjects.MyAccountPOM.DashboardPOM
{
    public class DashboardHomePage : AllPointsBaseWebPage
    {
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = ".container.myaccount.page-section.ng-scope"
        };

        private DomElement Subcontainer = new DomElement
        {
            locator = ".dashboard.detail-section"
        };

        private DomElement PageTitle = new DomElement()
        {
            locator = "h1"
        };

        #region Contact Information
        private DomElement ContactInfoSection = new DomElement(By.CssSelector)
        {
            locator = ".contact-info.licard-container"
        };

        private DomElement LinkContactInfo = new DomElement(By.CssSelector)
        {
            locator = ""
        };
        private DomElement EditContactInfoLink = new DomElement(By.CssSelector)
        {
            locator = "span:nth-child(1) button.btn-link"
        };
        #endregion Contact Information

        #region Addresses
        private DomElement AddressesSection = new DomElement(By.CssSelector)
        {
            locator = ".addresses.licard-container"
        };
        private DomElement AddressesLink = new DomElement(By.CssSelector)
        {
            locator = "span:nth-child(3) button.btn-link"
        };
        #endregion Adresses

        #region Payment options
        private DomElement PaymentOptionsSection = new DomElement(By.CssSelector)
        {
            locator = ".cardtokens.licard-container"
        };
        private DomElement PaymentOptionsEditLink = new DomElement(By.CssSelector)
        {
            locator = "span:nth-child(2) button.btn-link"
        };
        private DomElement PaymentsOptionsLink = new DomElement(By.CssSelector)
        {
            locator = "span:nth-child(3) button.btn-link"
        };
        #endregion Payment options

        #region Recent orders
        private DomElement RecentOrdersSection = new DomElement(By.CssSelector)
        {
            locator = ".orders.licard-container"
        };

        private DomElement TableRecentOrders = new DomElement(By.CssSelector)
        {            
            locator = ".licard-info.grid"
        };

        private DomElement MessageNotRecentOrders = new DomElement()
        {
            locator = ".section.orders div.message-summary"
        };
        #endregion Recent orders


        public DashboardHomePage(IWebDriver driver) : base(driver)
        {
            Container.Init(driver, SeleniumConstants.defaultWaitTime);
        }
                    
        //public string GetNoRecentOrdersMessage() => this.WebElementGetText(this.MessageNotRecentOrders.findsBy);

        public bool DashboardTitleExist()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement title = dashboardContainer.GetElementWaitByCSS(PageTitle.locator);

            return title.webElement.Displayed && title.webElement.Enabled;
        }

        public bool DashboardTitleTextIsCorrect(string expectedText)
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement textTitle = dashboardContainer.GetElementWaitByCSS(PageTitle.locator);

            return textTitle.webElement.Text.Equals(expectedText);
        }

        public bool ContactInfoExist()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement contactInfoSection = dashboardContainer.GetElementWaitByCSS(ContactInfoSection.locator);

            return contactInfoSection.webElement.Displayed && contactInfoSection.webElement.Enabled;
        }

        //public bool ClickContactInfoLink()
        //{
        //    this.linkContactInfo.webElement = _helper.GetElementWait(this.linkContactInfo.findsBy);
        //    linkContactInfo.webElement.Click();
        //    return true;
        //}

        public bool AddressesExist()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement addressesSection = dashboardContainer.GetElementWaitByCSS(AddressesSection.locator);

            return addressesSection.webElement.Displayed && addressesSection.webElement.Enabled;
        }

        public AddressesHomePage ClickAddressesLink()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement addressSection = dashboardContainer.GetElementWaitByCSS(AddressesSection.locator);
            DomElement goToEditLink = addressSection.GetElementWaitByCSS(AddressesLink.locator);

            goToEditLink.webElement.Click();

            return new AddressesHomePage(Driver);
        }

        public bool RecentOrdersExist()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement recentOrdersSection = dashboardContainer.GetElementWaitByCSS(RecentOrdersSection.locator);

            return recentOrdersSection.webElement.Displayed && recentOrdersSection.webElement.Enabled;
        }

        public bool AreRecentOrders()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement recentOrdersSection = dashboardContainer.GetElementWaitByCSS(RecentOrdersSection.locator);
            DomElement tableRecentOrders = recentOrdersSection.GetElementWaitByCSS(TableRecentOrders.locator);

            return tableRecentOrders.webElement.Displayed && tableRecentOrders.webElement.Enabled;
        }

        public ContactInfoEditPage ContactInfoEdit()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement contactInfoSection = dashboardContainer.GetElementWaitByCSS(ContactInfoSection.locator);
            DomElement goToEditLink = contactInfoSection.GetElementWaitByCSS(EditContactInfoLink.locator);

            goToEditLink.webElement.Click();

            return new ContactInfoEditPage(Driver);
        }

        //public bool AreNotRecentOrders()
        //{
        //    this.messageNotRecentOrders.webElement = _helper.GetElementWait(this.messageNotRecentOrders.findsBy);

        //    return this.messageNotRecentOrders.webElement.Displayed && this.messageNotRecentOrders.webElement.Enabled;
        //}

        //internal bool ClickOrderDetail()
        //{
        //    var orderTable = recentOrdersSection.GetElementsWaitByCSS("table tbody").FirstOrDefault();
        //    if (orderTable == null)
        //    {
        //        return false;
        //    }
        //    var tableRows = orderTable.GetElementsWaitByCSS("tr");
        //    if (tableRows.Count > 0)
        //    {
        //        var ancor = orderTable.GetElementsWaitByCSS("a").FirstOrDefault();
        //        if (ancor != null)
        //        {
        //            ancor.webElement.Click();
        //            return true;
        //        }
        //    }
        //    return true;
        //}


        public bool PaymentOptionsExist()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement paymentOptionsSection = dashboardContainer.GetElementWaitByCSS(PaymentOptionsSection.locator);

            return paymentOptionsSection.webElement.Displayed && paymentOptionsSection.webElement.Enabled;
        }


        public PaymentOptionsEditPage ClickEditPaymentLink()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement paymentSection = dashboardContainer.GetElementWaitByCSS(PaymentOptionsSection.locator);
            DomElement goToEditLink = paymentSection.GetElementWaitByCSS(PaymentOptionsEditLink.locator);

            // TEMPORARY Solution to have link in viewport
            // avoids click interception error
            IJavaScriptExecutor jse2 = (IJavaScriptExecutor)Driver;
            jse2.ExecuteScript("arguments[0].scrollIntoView()", goToEditLink.webElement);
            goToEditLink.webElement.Click();

            return new PaymentOptionsEditPage(Driver);
        }

        public PaymentOptionsHomePage ClickPaymentsLink()
        {
            DomElement dashboardContainer = Container.GetElementWaitByCSS(Subcontainer.locator);
            DomElement paymentSection = dashboardContainer.GetElementWaitByCSS(PaymentOptionsSection.locator);
            DomElement goToPaymentsLink = paymentSection.GetElementWaitByCSS(PaymentsOptionsLink.locator);

            // TEMPORARY Solution to have link in viewport
            // avoids click interception error
            IJavaScriptExecutor jse2 = (IJavaScriptExecutor)Driver;
            jse2.ExecuteScript("arguments[0].scrollIntoView()", goToPaymentsLink.webElement);
            goToPaymentsLink.webElement.Click();

            return new PaymentOptionsHomePage(Driver);
        }




        ////*

        //public string GetNoRecentOrdersMessage() => this.WebElementGetText(this.messageNotRecentOrders.findsBy);

        //public bool DashboardTitleExist()
        //{
        //    this.pageTitle.webElement = _helper.GetElementWait(this.pageTitle.findsBy);

        //    return this.pageTitle.webElement.Displayed && this.pageTitle.webElement.Enabled;
        //}

        //public bool DashboardTitleTextIsCorrect(string expectedText)
        //{
        //    this.pageTitle.webElement = _helper.GetElementWait(this.pageTitle.findsBy);

        //    return this.pageTitle.webElement.Text.Equals(expectedText);
        //}

        //public bool RecentOrdersExist()
        //{
        //    this.recentOrdersSection.webElement = _helper.GetElementWait(this.recentOrdersSection.findsBy);

        //    return this.recentOrdersSection.webElement.Displayed && this.recentOrdersSection.webElement.Enabled;
        //}

        //public bool AreRecentOrders()
        //{
        //    this.tableRecentOrders.webElement = _helper.GetElementWait(this.tableRecentOrders.findsBy);

        //    return this.tableRecentOrders.webElement.Displayed && this.tableRecentOrders.webElement.Enabled;
        //}

        //public bool AreNotRecentOrders()
        //{
        //    this.messageNotRecentOrders.webElement = _helper.GetElementWait(this.messageNotRecentOrders.findsBy);

        //    return this.messageNotRecentOrders.webElement.Displayed && this.messageNotRecentOrders.webElement.Enabled;
        //}

        //public bool ClickOrderDetail()
        //{
        //    var orderTable = recentOrdersSection.GetElementsWaitByCSS("table tbody").FirstOrDefault();
        //    if (orderTable == null)
        //    {
        //        return false;
        //    }
        //    var tableRows = orderTable.GetElementsWaitByCSS("tr");
        //    if (tableRows.Count > 0)
        //    {
        //        var ancor = orderTable.GetElementsWaitByCSS("a").FirstOrDefault();
        //        if (ancor != null)
        //        {
        //            ancor.webElement.Click();
        //            return true;
        //        }
        //    }
        //    return true;
        //}

        ////private methods
        //private string WebElementGetText(By findBy)
        //{
        //    return _helper.GetElementWait(findBy).Text;
        //}

    }
}