using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllPointsPOM.PageObjects.QuickOrderPOM
{
    public class QuickOrdersHomePage : AllPointsBaseWebPage
    {
        private GenericPage _helper { get { return Helper; } }

        public QuickOrdersHomePage(IWebDriver driver) : base(driver)
        {
            this.detailSummary.Init(driver, SeleniumConstants.defaultWaitTime);
        }

        private DomElement detailSummary = new DomElement(By.CssSelector)
        {
            locator = "main"
        };

        private DomElement detailQuickOrder = new DomElement
        {
            locator = ".quick-order"
        };

        private DomElement detailProductTextBox = new DomElement
        {
            locator = ".products"
        };

        private DomElement detailButtonContainer = new DomElement
        {
            locator = ".button-container"
        };

        private DomElement detailFormControl = new DomElement
        {
            locator = ".form-control"
        };

        private DomElement detailQuantityTxtBox = new DomElement
        {
            locator = "input[type='text']"
        };

        private DomElement detailQuantity = new DomElement
        {
            locator = ".col-sm-1"
        };

        private DomElement detailButtonAddtoCart = new DomElement
        {
            locator = ".btn"
        };

        private DomElement detailHeaderLabels = new DomElement
        {
            locator = ".header"
        };

        private DomElement detailYourPrice = new DomElement
        {
            locator = ".your-price"
        };

        public void TypeAllPointsNumber(string skunumber)
        {
            DomElement mainQuickOrderPage = detailSummary.GetElementWaitByCSS(detailQuickOrder.locator);
            DomElement detailProductSection = mainQuickOrderPage.GetElementWaitByCSS(detailProductTextBox.locator);
            DomElement formControlTxtBox = detailProductSection.GetElementWaitByCSS(detailFormControl.locator);
            formControlTxtBox.webElement.SendKeys(skunumber);
        }

        public void TypeQty(string qty)
        {
            DomElement mainQuickOrderPage = detailSummary.GetElementWaitByCSS(detailQuickOrder.locator);
            DomElement detailProductSection = mainQuickOrderPage.GetElementWaitByCSS(detailProductTextBox.locator);
            DomElement detailChildQty = detailProductSection.GetElementWaitByCSS(detailQuantity.locator);
            DomElement formQtyTxtBox = detailChildQty.GetElementWaitByCSS(detailFormControl.locator);
            formQtyTxtBox.webElement.Click();
            formQtyTxtBox.webElement.Clear();
            formQtyTxtBox.webElement.SendKeys(qty);
            Thread.Sleep(2000);
        }

        public APCartPage ClickAddToCart()
        {
            DomElement mainQuickOrderPage = detailSummary.GetElementWaitByCSS(detailQuickOrder.locator);
            DomElement deailbuttoncontain = mainQuickOrderPage.GetElementWaitByCSS(detailButtonContainer.locator);
            DomElement clickBtn = deailbuttoncontain.GetElementWaitByCSS(detailButtonAddtoCart.locator);
            clickBtn.webElement.Click();
            return new APCartPage(Driver);
        }

        public bool Validate_YourPriceLabelIsDisplayed()
        {
            DomElement mainQuickOrderPage = detailSummary.GetElementWaitByCSS(detailQuickOrder.locator);
            DomElement headerdetail = mainQuickOrderPage.GetElementWaitByCSS(detailHeaderLabels.locator);
            DomElement yourpriceDetail = headerdetail.GetElementWaitByCSS(detailYourPrice.locator);
            return yourpriceDetail.webElement.Enabled;
        }
    }
}
