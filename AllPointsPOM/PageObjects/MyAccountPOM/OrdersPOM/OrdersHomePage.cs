using AllPoints.PageObjects.MyAccountPOM.OrdersPOM;
using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Components;
using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Contracts;
using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Enums;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.PageObjects.MyAccountPOM
{
    public class OrdersHomePage : AllPointsBaseWebPage
    {
        private GenericPage _helper { get { return Helper; } }

        private DomElement pageTitle = new DomElement()
        {
            locator = ".detail-section h1"
        };

        private DomElement defaultEducateText = new DomElement()
        {
            locator = "h1+span.detail"
        };

        #region SearchForm

        private DomElement filterForm = new DomElement(By.CssSelector)
        {
            locator = "form[name=\"searchOrder\"]"
        };

        private DomElement filterSearchComponent = new DomElement()
        {
            locator = "order-number-search"
        };

        private DomElement filterSearchDropDown = new DomElement()
        {
            locator = "select"
        };

        private DomElement filterSearchInput = new DomElement()
        {
            locator = "input"
        };

        private DomElement filterDateComponent = new DomElement()
        {
            locator = "order-date-search"
        };

        private DomElement filterDateFromInput = new DomElement()
        {
            locator = "#startdate"
        };

        private DomElement filterDateToInput = new DomElement()
        {
            locator = "#enddate"
        };

        private DomElement filterSubmitButton = new DomElement()
        {
            locator = "input#submit"
        };

        #endregion SearchForm

        #region SearchResult

        private DomElement searchResultMessage = new DomElement(By.CssSelector)
        {
            locator = "section.message"
        };

        private DomElement searchResultTable = new DomElement(By.CssSelector)
        {
            locator = "order-table"
        };

        private DomElement searchResultTableOrders = new DomElement()
        {
            locator = "tbody"
        };

        #endregion SearchResult

        private DomElement searchBoldText = new DomElement
        {
            locator = "section h4"
        };

        private DomElement searchBySelect = new DomElement()
        {
            locator = "order-number-search .form-item select"
        };

        private DomElement searchByInput = new DomElement()
        {
            locator = ".search-input.form-control.five"
        };

        private DomElement searchBySelectOptions = new DomElement()
        {
            locator = "order-number-search .form-item select option"
        };

        private DomElement fromDateField = new DomElement()
        {
            locator = "startdate"
        };

        private DomElement toDateField = new DomElement()
        {
            locator = "enddate"
        };

        private DomElement statusSelect = new DomElement()
        {
            locator = ".form-item.five select.form-control"
        };

        private DomElement statusSelectOptions = new DomElement()
        {
            locator = ".form-item.five select.form-control option"
        };

        private DomElement searchButton = new DomElement()
        {
            locator = "submit"
        };

        private DomElement textMessageWhenNoOrders = new DomElement()
        {
            locator = "section.message.ng-scope span"
        };

        private DomElement datePicker = new DomElement()
        {
            locator = "div.uib-daypicker table"
        };

        private DomElement busyIndicator = new DomElement()
        {
            locator = "app-busy-indicator div"
        };

        private OrdersTable ordersTable;

        private DomElement orderNumberLink = new DomElement(By.XPath)
        {
            locator = "//td/a"
        };
        //page web elements declarations ends here...

        public OrdersHomePage(IWebDriver driver) : base(driver)
        {
            this.InitializeElements();
        }

        private void InitializeElements()
        {
            this.pageTitle.findsBy = By.CssSelector(this.pageTitle.locator);
            this.defaultEducateText.findsBy = By.CssSelector(this.defaultEducateText.locator);

            this.searchBoldText.findsBy = By.CssSelector(this.searchBoldText.locator);

            this.statusSelect.findsBy = By.CssSelector(this.statusSelect.locator);
            this.statusSelectOptions.findsBy = By.CssSelector(this.statusSelectOptions.locator);

            this.searchBySelect.findsBy = By.CssSelector(this.searchBySelect.locator);
            this.searchBySelectOptions.findsBy = By.CssSelector(this.searchBySelectOptions.locator);
            this.searchByInput.findsBy = By.CssSelector(this.searchByInput.locator);

            this.fromDateField.findsBy = By.Id(this.fromDateField.locator);

            this.toDateField.findsBy = By.Id(this.toDateField.locator);

            this.searchButton.findsBy = By.Id(this.searchButton.locator);

            this.textMessageWhenNoOrders.findsBy = By.CssSelector(this.textMessageWhenNoOrders.locator);

            this.datePicker.findsBy = By.CssSelector(this.datePicker.locator);

            this.ordersTable = new OrdersTable(Driver);

            this.busyIndicator.findsBy = By.CssSelector(this.busyIndicator.locator);

            this.filterForm.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        #region Filters

        public void FilterSetSearchOption(string searchOption, string searchValue)
        {
            filterSearchComponent = filterForm.GetElementWaitByCSS(filterSearchComponent.locator);
            filterSearchDropDown = filterSearchComponent.GetElementWaitByCSS(filterSearchDropDown.locator);
            filterSearchInput = filterSearchComponent.GetElementWaitByCSS(filterSearchInput.locator);
            SelectDropDownOption(filterSearchDropDown, searchOption);
            SetInputField(filterSearchInput, searchValue);
        }

        public void FilterSetDates(string dateFrom, string dateTo)
        {
            filterDateComponent = filterForm.GetElementWaitByCSS(filterDateComponent.locator);
            filterDateFromInput = filterDateComponent.GetElementWaitByCSS(filterDateFromInput.locator);
            filterDateToInput = filterDateComponent.GetElementWaitByCSS(filterDateToInput.locator);
            SetInputField(filterDateFromInput, dateFrom);
            SetInputField(filterDateToInput, dateTo);
        }

        public void FilterSubmit()
        {
            filterSubmitButton = filterForm.GetElementWaitByCSS(filterSubmitButton.locator);
            filterSubmitButton.webElement.Click();
        }

        public FilterSearchResultEnum OrderResultTableOrMessage()
        {
            searchResultMessage.webElement = Helper.GetElementsWait(By.CssSelector(searchResultMessage.locator)).FirstOrDefault();
            searchResultTable.webElement = Helper.GetElementsWait(By.CssSelector(searchResultTable.locator)).FirstOrDefault();
            if (searchResultMessage.webElement != null && searchResultTable.webElement == null)
            {
                return FilterSearchResultEnum.Message;
            }
            else if (searchResultMessage.webElement == null && searchResultTable.webElement != null)
            {
                return FilterSearchResultEnum.Table;
            }
            return FilterSearchResultEnum.None;
        }

        #endregion Filters

        public string OrderResultMessageGet()
        {
            searchResultMessage.Init(Driver, SeleniumConstants.defaultWaitTime);
            var spanMessage = searchResultMessage.GetElementWaitByCSS("span");
            return spanMessage.webElement.Text;
        }

        public bool OrderResultTableHaveValues()
        {
            searchResultTable.Init(Driver, SeleniumConstants.defaultWaitTime);
            searchResultTableOrders = searchResultTable.GetElementWaitByCSS(searchResultTableOrders.locator);
            List<DomElement> orders = searchResultTableOrders.GetElementsWaitByCSS("tr");
            if (orders.Count > 0)
            {
                return true;
            }
            return false;
        }

        //validate that the page has a Orders title
        public bool OrdersTitleExist()
        {
            this.pageTitle.webElement = _helper.GetElementWait(this.pageTitle.findsBy);

            return this.pageTitle.webElement.Displayed && this.pageTitle.webElement.Enabled;
        }

        public bool OrdersTitleTextIsCorrect(string expectedText)
        {
            this.pageTitle.webElement = _helper.GetElementWait(this.pageTitle.findsBy);

            return this.pageTitle.webElement.Text.Equals(expectedText);
        }

        public bool DefaultEducateTextExist()
        {
            if (_helper.ElementExist(this.defaultEducateText.findsBy))
            {
                var text = _helper.GetElementWait(this.defaultEducateText.findsBy);
                return text.Displayed && text.Enabled;
            }

            return false;
        }

        //validate that the page has a text indicating that theres a 30 days of orders activity
        public bool DefaultViewTextIsCorrect(string expectedText)
        {
            this.defaultEducateText.webElement = _helper.GetElementWait(this.defaultEducateText.findsBy);

            return this.defaultEducateText.webElement.Text.Equals(expectedText);
        }

        public string GetSearchSubtitleText()
        {
            return _helper.GetElementWait(this.searchBoldText.findsBy).Text;
        }

        public bool SearchTextIsBold()
        {
            int bold;

            bool result = int.TryParse(
                _helper.GetElementWait(this.searchBoldText.findsBy).GetCssValue("fontWeight"), out bold
            );

            if (!result) return false;

            return bold >= 500;
        }

        public bool TextWhenNoOrdersExist()
        {
            return _helper.GetElementWait(this.textMessageWhenNoOrders.findsBy).Displayed;
        }

        public bool TextWhenNoOrdersIsCorrect(string expectedText)
        {
            return _helper.GetElementWait(this.textMessageWhenNoOrders.findsBy, 120).Text.Equals(expectedText);
        }

        //validate the table order header; order date, number, web order number, status, total
        public bool TableHasHeaders(string tableHeaders)
        {
            string[] heads = tableHeaders.Split(',');

            List<string> headers = new List<string>();

            var actualHeaders = this.ordersTable.GetHeaders();

            foreach (var head in heads)
            {
                headers.Add(head);
            }

            try
            {
                CollectionAssert.AreEqual(headers, actualHeaders.ToList());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public int GetTableRowsCount()
        {
            return this.ordersTable.GetRowsCount();
        }

        //validate that the table is ordered by the column date (newest to oldest)
        public bool TableContentIsOrderedByDate(bool ascendent = true)
        {
            return this.ordersTable.ColumnIsOrdered("Date", ascendent);
        }

        //validate that theres exist a pagination between each 15 records
        public bool PaginationBetweenElements(int count)
        {
            //TODO: validate if the table has 15 elements
            //var current = this.tableContent.webElement.FindElements(By.CssSelector("")).Count;

            //return current == count;

            return true;
        }

        public bool StatusColorIsCorrect(string status, string color)
        {
            return ordersTable.StatusColorIsOk(status, color);
        }

        public bool FromDateSearchFieldExist()
        {
            if (_helper.FindByCondition(this.fromDateField.findsBy, (el) => el.Displayed && el.Enabled) != null)
                return true;

            return false;
        }

        public bool ToDateSearchFieldExist()
        {
            if (_helper.FindByCondition(this.toDateField.findsBy, (el) => el.Displayed && el.Enabled) != null)
                return true;

            return false;
        }

        public bool ToDatePickerExist()
        {
            return this.GetDatePicker(this.toDateField.findsBy);
        }

        public bool FromDatePickerExist()
        {
            return this.GetDatePicker(this.fromDateField.findsBy);
        }

        public bool SearchButtonIsDisabledCorrectly()
        {
            this.fromDateField.webElement = _helper.GetElement(this.fromDateField.findsBy);
            this.toDateField.webElement = _helper.GetElement(this.toDateField.findsBy);

            this.fromDateField.webElement.Clear();

            if (this.SearchButtonIsEnabled() == false)
            {
                return true;
            }

            return false;
        }

        public bool SearchByDropdownExist()
        {
            return _helper.ElementExist(this.searchBySelect.findsBy);
        }

        public bool SearchByHasOption(string option)
        {
            var searchByOptions = this.GetOptionsFromDropdown(this.searchBySelectOptions.findsBy);

            return !string.IsNullOrEmpty(searchByOptions.FirstOrDefault(opt => opt.Equals(option)));
        }

        public void SetFromDate(string fromDate)
        {
            this.SetDate(DateTime.Parse(fromDate).ToString("MM/dd/yyyy"), this.fromDateField);
        }

        public void SetToDate(string fromDate)
        {
            this.SetDate(DateTime.Parse(fromDate).ToString("MM/dd/yyyy"), this.toDateField);
        }

        public void SetFromDateOneYearLessThanNow()
        {
            this.SetDate(DateTime.Now.AddYears(-1).ToShortDateString(), this.fromDateField);
        }

        public void ClickOnSearchButton()
        {
            base.WaitForAppBusy(60);
            this.searchButton.webElement = _helper.GetElementWait(this.searchButton.findsBy);
            this.searchButton.webElement.Click();
            _helper.FindByCondition(this.busyIndicator.findsBy, (el) => !el.Displayed, 30);
        }

        public OrderDetailsPage GoToOrderDetail(string orderNumber)
        {
            if (this.ordersTable.ClickOnOrderNumber(orderNumber))
            {
                return new OrderDetailsPage(Driver);
            }

            return null;
        }

        public void ClickOnOrderDetailWithNoNumber()
        {
            //do nothing
        }

        public bool ValidateDatesAreInSearchRange(string from, string to)
        {
            return this.ordersTable.DatesAreInSearchRange(DateTime.Parse(from), DateTime.Parse(to));
        }

        public bool ValidateDefaultViewIsInRange(int days)
        {
            return this.ordersTable.DatesAreInDefaultRange(days);
        }

        public bool ColumnExist(string columnName)
        {
            return !string.IsNullOrEmpty(this.ordersTable.GetHeaders().FirstOrDefault(c =>
                c.Equals(columnName)));
        }

        public bool AnyColumnHasEmptyValues(string columnName)
        {
            return this.ordersTable.ColumnHasNotEmptyValues(columnName);
        }

        public bool StatusesAfterSearchAreOk(string status)
        {
            return ordersTable.StatusesAreCorrect(status);
        }

        public bool StatusDropDownExist()
        {
            this.statusSelect.webElement = _helper.GetElementWait(this.statusSelect.findsBy);

            return this.statusSelect.webElement.Displayed && this.statusSelect.webElement.Enabled;
        }

        public string GetStatusDropDownCurrentValue()
        {
            //actually returns all values <-
            var statusSelectElement = _helper.GetElementWait(this.statusSelect.findsBy);

            var statuses = this.GetOptionsFromDropdown(this.statusSelectOptions.findsBy).ToArray();

            string selected = statuses[int.Parse(statusSelectElement.GetAttribute("selectedIndex"))];

            return selected;
        }

        public bool StatusDropDownHasStatuses(string statuses)
        {
            string[] expectedStatuses = statuses.Split(';');

            var actualStatuses = this.GetOptionsFromDropdown(this.statusSelectOptions.findsBy);

            try
            {
                CollectionAssert.AreEqual(expectedStatuses.ToList(), actualStatuses.ToList());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void SetStatus(string status)
        {
            this.statusSelect.webElement = _helper.GetElementWait(this.statusSelect.findsBy);

            var statusSelect = new SelectElement(this.statusSelect.webElement);

            statusSelect.SelectByText(status);
        }

        public void SetSearchBySelect(string by)
        {
            this.searchBySelect.webElement = _helper.GetElementWait(this.searchBySelect.findsBy);

            var searchBySelect = new SelectElement(this.searchBySelect.webElement);

            searchBySelect.SelectByText(by);
        }

        public void SetInputForSearchBy(string value)
        {
            this.searchByInput.webElement = _helper.GetElementWait(this.searchByInput.findsBy);

            this.searchByInput.webElement.SendKeys(value);
        }

        public bool ValidateSearchBy(string searchBy, string expectedValue)
        {
            return this.ValidateResultsInTable(expectedValue, ordersTable.GetColumnRecords(searchBy));
        }

        //private methods
        private bool SearchButtonIsEnabled()
        {
            this.searchButton.webElement = _helper.GetElementWait(this.searchButton.findsBy);

            return this.searchButton.webElement.Enabled && this.searchButton.webElement.Displayed;
        }

        private void SetDate(string date, DomElement datePicker)
        {
            datePicker.webElement = _helper.GetElementWait(datePicker.findsBy);

            datePicker.webElement.Clear();

            datePicker.webElement.SendKeys(date);
        }

        private bool GetDatePicker(By findBy)
        {
            //click on the date picker given via parameter
            _helper.GetElementWait(findBy).Click();

            datePicker.webElement = _helper.GetElementWait(this.datePicker.findsBy);

            return datePicker.webElement.Displayed && datePicker.webElement.Enabled;
        }

        private ICollection<string> GetOptionsFromDropdown(By dropdown)
        {
            var optionsHTML = _helper.GetElementsWait(dropdown);

            List<string> options = new List<string>();

            foreach (var option in optionsHTML)
            {
                options.Add(option.Text);
            }

            return options;
        }

        private bool TableHasHeaders(IDomTable table)
        {
            return false;
        }

        private bool ValidateResultsInTable(string expectedPoNumber, ICollection<string> results)
        {
            return !string.IsNullOrEmpty(results.FirstOrDefault(x => x.Contains(expectedPoNumber)));
        }

        public OrderDetailsPage ClickOnOrderButtonByIndex(int index)
        {
            base.ScrollToTop();
            Thread.Sleep(5000);
            List<DomElement> links = this.filterForm.GetElementsWaitByXpath(orderNumberLink.locator);
            if (index > links.Count - 1 || index < 0) index = links.Count - 1;
            try
            {
                links[index].webElement.Click();
            }
            catch (Exception)
            {
                Thread.Sleep(3000);
                links = this.filterForm.GetElementsWaitByXpath(orderNumberLink.locator);
            }
            return new OrderDetailsPage(Driver);
        }

    }
}