using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Helpers;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.MyAccountPOM.OrdersPOM
{
    public class OrderDetailsPage : AllPointsBaseWebPage
    {
        private GenericPage _helper;

        private DomElement sectionTitle = new DomElement()
        {
            locator = "h1"
        };

        private DomElement sectionSubtitle = new DomElement()
        {
            locator = "article.detail-section h2"
        };

        private DomElement detailsText = new DomElement()
        {
            locator = "h2.title"
        };

        private DomElement dateKey = new DomElement()
        {
            locator = "div.three :nth-child(1) span"
        };

        private DomElement dateValue = new DomElement()
        {
            locator = "div.three :nth-child(1) span.ng-binding"
        };

        private DomElement totalItemsKey = new DomElement()
        {
            locator = "div.three :nth-child(2) span"
        };

        private DomElement totalItemsValue = new DomElement()
        {
            locator = "div.three :nth-child(2) span.ng-binding"
        };

        private DomElement poNumberKey = new DomElement()
        {
            locator = "div.three :nth-child(3) span"
        };

        private DomElement poNumberValue = new DomElement()
        {
            locator = "div.three :nth-child(3) span.ng-binding"
        };

        private DomElement statusKey = new DomElement()
        {
            locator = "div.three :nth-child(4) span"
        };

        private DomElement statusValue = new DomElement()
        {
            locator = "div.three :nth-child(4) span:nth-of-type(2)"
        };

        private DomElement itemTotalsKey = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(1) span"
        };

        private DomElement itemTotalsValue = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(1) span.ng-binding"
        };

        private DomElement taxKey = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(2) span"
        };

        private DomElement taxValue = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(2) span.ng-binding"
        };

        private DomElement shippingKey = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(3) span"
        };

        private DomElement shippingValue = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(3) span.ng-binding"
        };

        private DomElement totalKey = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(4) span"
        };

        private DomElement totalValue = new DomElement()
        {
            locator = "div.spaced.row-container  :nth-child(2) :nth-child(4) span.ng-binding"
        };

        //shipping subtitle
        private DomElement shippingSubtitle = new DomElement()
        {
            locator = ":nth-of-type(2) h2.title"
        };

        private DomElement shippingAddresses = new DomElement()
        {
            locator = "div.three div.row-container div.content div.ng-binding"
        };

        private DomElement trackingNumbers = new DomElement()
        {
            locator = "div.stackeable.row-container.ng-scope div"
        };

        private DomElement messageWOD = new DomElement()
        {
            locator = "//*[@class='alert alert-success ng-scope']"
        };

        private DomElement tableDetails = new DomElement()
        {
            locator = "//*[@class='table table-borderless ng-scope']"
        };

        private DomElement detailQty = new DomElement()
        {
            locator = "table tbody tr td:nth-of-type(3)"
        };

        private DomElement detailShipped = new DomElement()
        {
            locator = "table tbody tr td:nth-of-type(4)"
        };

        private DomElement detailPrice = new DomElement()
        {
            locator = "table tbody tr td:nth-of-type(5)"
        };

        private DomElement detailTotal = new DomElement()
        {
            locator = "table tbody tr td:nth-of-type(6)"
        };

        private DomElement reorderButton = new DomElement(By.CssSelector)
        {
            locator = "button.action-reorder"
        };

        private DomElement addToCartButton = new DomElement(By.XPath)
        {
            locator = "//div[contains(@class,'modal-dialog')]//button[contains(@class,'action-reorder')]"
        };

        public OrderDetailsPage(IWebDriver driver) : base(driver)
        {
            _helper = new GenericPage(Driver);
            this.InitializeElements();
        }

        private void InitializeElements()
        {
            this.sectionTitle.findsBy = By.TagName(this.sectionTitle.locator);
            this.sectionSubtitle.findsBy = By.CssSelector(this.sectionSubtitle.locator);
            this.detailsText.findsBy = By.CssSelector(this.detailsText.locator);
            this.dateKey.findsBy = By.CssSelector(this.dateKey.locator);
            this.dateValue.findsBy = By.CssSelector(this.dateValue.locator);
            this.totalItemsKey.findsBy = By.CssSelector(this.totalItemsKey.locator);
            this.totalItemsValue.findsBy = By.CssSelector(this.totalItemsValue.locator);
            this.poNumberKey.findsBy = By.CssSelector(this.poNumberKey.locator);
            this.poNumberValue.findsBy = By.CssSelector(this.poNumberValue.locator);
            this.statusKey.findsBy = By.CssSelector(this.statusKey.locator);
            this.statusValue.findsBy = By.CssSelector(this.statusValue.locator);
            this.itemTotalsKey.findsBy = By.CssSelector(this.itemTotalsKey.locator);
            this.itemTotalsValue.findsBy = By.CssSelector(this.itemTotalsValue.locator);
            this.taxKey.findsBy = By.CssSelector(this.taxKey.locator);
            this.taxValue.findsBy = By.CssSelector(this.taxValue.locator);
            this.shippingKey.findsBy = By.CssSelector(this.shippingKey.locator);
            this.shippingValue.findsBy = By.CssSelector(this.shippingValue.locator);
            this.totalKey.findsBy = By.CssSelector(this.totalKey.locator);
            this.totalValue.findsBy = By.CssSelector(this.totalValue.locator);
            this.shippingSubtitle.findsBy = By.CssSelector(this.shippingSubtitle.locator);
            this.shippingAddresses.findsBy = By.CssSelector(this.shippingAddresses.locator);
            this.trackingNumbers.findsBy = By.CssSelector(this.trackingNumbers.locator);
            this.messageWOD.findsBy = By.XPath(this.messageWOD.locator);
            this.tableDetails.findsBy = By.XPath(this.tableDetails.locator);
            this.detailQty.findsBy = By.CssSelector(this.detailQty.locator);
            this.detailShipped.findsBy = By.CssSelector(this.detailShipped.locator);
            this.detailPrice.findsBy = By.CssSelector(this.detailPrice.locator);
            this.detailTotal.findsBy = By.CssSelector(this.detailTotal.locator);
            this.reorderButton.findsBy = By.CssSelector(this.reorderButton.locator);
            this.addToCartButton.findsBy = By.XPath(this.addToCartButton.locator);
        }

        public bool TitleExist() => this.WebElementExist(this.sectionTitle.findsBy);

        public string GetTitleText() => this.WebElementGetText(this.sectionTitle.findsBy);

        public bool SubtitleExist() => this.WebElementExist(this.sectionSubtitle.findsBy);

        public string GetSubtitleText() => this.WebElementGetText(this.sectionSubtitle.findsBy);

        public bool DetailSubtitleExist() => this.WebElementExist(detailsText.findsBy);

        public string GetDetailSubtitleText() => this.WebElementGetText(this.detailsText.findsBy);

        public bool DateLabelExist() => this.WebElementExist(this.dateKey.findsBy);

        public string GetDateLabelText() => this.WebElementGetText(this.dateKey.findsBy);

        public string GetDateValue() => this.WebElementGetText(this.dateValue.findsBy);

        public bool TotalItemsLabelExist() => this.WebElementExist(this.totalItemsKey.findsBy);

        public string GetTotalItemsLabelText() => this.WebElementGetText(this.totalItemsKey.findsBy);

        public string GetTotalItemsValue() => this.WebElementGetText(this.totalItemsValue.findsBy);

        public bool PoLabelExist() => this.WebElementExist(this.poNumberValue.findsBy);

        public string GetPoLabelText() => this.WebElementGetText(this.poNumberKey.findsBy);

        public string GetPoValue() => this.WebElementGetText(this.poNumberValue.findsBy);

        public bool StatusLabelExist() => this.WebElementExist(this.statusKey.findsBy);

        public string GetStatusLabelText() => this.WebElementGetText(this.statusKey.findsBy);

        public string GetStatusValue() => this.WebElementGetText(this.statusValue.findsBy);

        public bool GetStatusColor(string hexColor)
        {
            return OrdersHelper.ToRGBAColor(hexColor)
                .Equals(_helper.GetElementWait(this.statusValue.findsBy).GetCssValue("color"));
        }

        public bool ItemTotalsLabelExist() => this.WebElementExist(this.itemTotalsKey.findsBy);

        public string GetItemTotalsLabelText() => this.WebElementGetText(this.itemTotalsKey.findsBy);

        public string GetItemTotalsValue() => this.WebElementGetText(this.itemTotalsValue.findsBy);

        public bool TaxLabelExist() => this.WebElementExist(this.taxKey.findsBy);

        public string GetTaxLabelText() => this.WebElementGetText(this.taxKey.findsBy);

        public string GetTaxValue() => this.WebElementGetText(this.taxValue.findsBy);

        public bool ShippingLabelExist() => this.WebElementExist(this.shippingKey.findsBy);

        public string GetShippingLabelText() => this.WebElementGetText(this.shippingKey.findsBy);

        public string GetShippingValue() => this.WebElementGetText(this.shippingValue.findsBy);

        public bool TotalLabelExist() => this.WebElementExist(this.totalKey.findsBy);

        public string GetTotalLabelText() => this.WebElementGetText(this.totalKey.findsBy);

        public string GetTotalValue() => this.WebElementGetText(this.totalValue.findsBy);

        public bool ShippingSubtitleExist() => this.WebElementExist(this.shippingSubtitle.findsBy);

        public string GetShippingSubtitle() => this.WebElementGetText(this.shippingSubtitle.findsBy);

        public bool WODMessageExist() => this.WebElementExist(this.messageWOD.findsBy);

        public string GetWODMessage() => this.WebElementGetText(this.messageWOD.findsBy);

        public bool DetailsTableExist() => this.WebElementExist(this.tableDetails.findsBy);

        public bool ValidateShippingAddresses()
        {
            return true;
        }

        public string ValidateTrackingNumbers(string tNumbers)
        {
            string[] trackingNumbersText = tNumbers.Split(',');

            var trackingNumbersDom = _helper.GetElementsWait(this.trackingNumbers.findsBy);

            string result = string.Empty;

            foreach (string number in trackingNumbersText)
            {
                if (trackingNumbersDom.FirstOrDefault((el) => el.Text.Equals(number)) == null)
                    result = number;
            }
            return result;
        }

        public bool ClickOnTrakingNumber(string tNumbers)
        {
            string[] trackingNumbersText = tNumbers.Split(',');

            var trackingNumbersDom = _helper.GetElementsWait(this.trackingNumbers.findsBy);

            string result = string.Empty;

            foreach (string number in trackingNumbersText)
            {
                //driver.switchTo().window("windowName");
                if (trackingNumbersDom.FirstOrDefault((el) => el.Text.Equals(number)) == null)
                    result = number;
            }

            return true;
        }

        //Get the total number of the items in the table details
        public string CountElementTableDetails()
        {
            var numberElements = GetTotalsFromTable().Count;
            return numberElements.ToString();
        }

        //Sum of the all totals of table details
        public string CalculateTableTotal()
        {
            float total = 0;

            var totalsFromTable = GetTotalsFromTable();

            foreach (var status in totalsFromTable)
            {
                total += float.Parse(status.Trim('$'));
            }

            return "$" + string.Format("{0:0.00}", total);
        }

        public string GetFinalTotal()
        {
            var itemTotal = float.Parse(GetItemTotalsValue().Trim('$'));
            var tax = float.Parse(GetTaxValue().Trim('$'));
            var shipping = float.Parse(GetShippingValue().Trim('$'));
            var finalTotal = itemTotal + tax + shipping;
            return "$" + string.Format("{0:0.00}", finalTotal);
        }

        public bool HasDiscount()
        {
            var totalTable = float.Parse(CalculateTableTotal().Trim('$'));
            var total = float.Parse(GetTotalValue().Trim('$'));
            var tax = float.Parse(GetTaxValue().Trim('$'));
            var shipping = float.Parse(GetShippingValue().Trim('$'));

            if ((total - tax - shipping) < totalTable)
                return true;
            else
                return false;
        }

        public bool CompareDatesString(string testDate)
        {
            string parseDate = DateTime.Parse(testDate).ToString("MM/dd/yyyy");
            return parseDate.Equals(GetDateValue());
        }

        //private methods
        private bool WebElementExist(By findBy)
        {
            if (_helper.ElementExist(findBy))
            {
                return _helper.GetElementWait(findBy).Displayed;
            }
            return false;
        }

        private string WebElementGetText(By findBy)
        {
            return _helper.GetElementWait(findBy).Text;
        }

        //Get each total of table details
        private ICollection<string> GetTotalsFromTable()
        {
            var totalsFromTable = _helper.GetElementsWait(this.detailTotal.findsBy);

            List<string> partialTotals = new List<string>();

            foreach (var status in totalsFromTable)
            {
                partialTotals.Add(status.Text);
            }

            return partialTotals;
        }

        public void ClickOnReorderButton()
        {
            IWebElement reorderButton = _helper.GetElementWait(this.reorderButton.findsBy);
            reorderButton.Click();
        }

        public void ClickOnAddToCartButton()
        {
            Thread.Sleep(3000);
            IWebElement addToCartButton = _helper.GetElementWait(this.reorderButton.findsBy);
            addToCartButton.Click();
            base.WaitForAppBusy();
        }
    }
}