using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Contracts;
using AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Helpers;
using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Components
{
    public class OrdersTable : AllPointsBaseWebPage, IDomTable
    {
        private GenericPage _helper;

        private DomElement tableHeaders = new DomElement()
        {
            locator = "order-table table th"
        };

        private DomElement statusColumn = new DomElement()
        {
            locator = "order-table table tbody tr td:nth-of-type(5)"
        };

        private DomElement processingStatusElement = new DomElement()
        {
            locator = "table tr.processing td:nth-child(5)"
        };

        private DomElement shippedStatusElement = new DomElement()
        {
            locator = "table tr.shipped td:nth-child(5)"
        };

        private DomElement canceledStatusElement = new DomElement()
        {
            locator = "table tr.canceled td:nth-child(5)"
        };

        private DomElement undefinedStatusElement = new DomElement()
        {
            locator = "table tr.undefined td:nth-child(5)"
        };

        private DomElement dateColumn = new DomElement()
        {
            locator = "order-table table tbody tr td:nth-of-type(1)"
        };

        private DomElement orderNumber = new DomElement()
        {
            locator = "order-table table tbody tr td:nth-of-type(2)"
        };

        private DomElement poNumberColumn = new DomElement()
        {
            locator = "order-table table tbody tr td:nth-of-type(3)"
        };

        private DomElement webReferenceNumberColumn = new DomElement()
        {
            locator = "order-table table tbody tr td:nth-of-type(4)"
        };

        private DomElement rows = new DomElement()
        {
            locator = "order-table table tr"
        };

        //class constructor
        public OrdersTable(IWebDriver driver) : base(driver)
        {
            _helper = new GenericPage(driver);

            this.Initialize();
        }

        public void Initialize()
        {
            //Todo: initialize all web elements
            this.tableHeaders.findsBy = By.CssSelector(this.tableHeaders.locator);
            this.rows.findsBy = By.CssSelector(this.rows.locator);

            this.processingStatusElement.findsBy = By.CssSelector(this.processingStatusElement.locator);
            this.shippedStatusElement.findsBy = By.CssSelector(this.shippedStatusElement.locator);
            this.canceledStatusElement.findsBy = By.CssSelector(this.canceledStatusElement.locator);
            this.undefinedStatusElement.findsBy = By.CssSelector(this.undefinedStatusElement.locator);

            this.dateColumn.findsBy = By.CssSelector(this.dateColumn.locator);
            this.orderNumber.findsBy = By.CssSelector(this.orderNumber.locator);
            this.poNumberColumn.findsBy = By.CssSelector(this.poNumberColumn.locator);
            this.webReferenceNumberColumn.findsBy = By.CssSelector(this.webReferenceNumberColumn.locator);
            this.statusColumn.findsBy = By.CssSelector(this.statusColumn.locator);
        }

        public bool StatusColorIsOk(string status, string hexacolor)
        {
            if (status.Equals("Processing"))
            {
                if (!_helper.ElementExist(processingStatusElement.findsBy))
                {
                    throw new Exception("Theres any element with the given status: " + status);
                }

                return CheckStatusColor(processingStatusElement, hexacolor);
            }

            if (status.Equals("Shipped"))
            {
                if (!_helper.ElementExist(shippedStatusElement.findsBy))
                    throw new Exception("Theres any element with the given status: " + status);

                return CheckStatusColor(shippedStatusElement, hexacolor);
            }

            if (status.Equals("Canceled"))
            {
                if (!_helper.ElementExist(canceledStatusElement.findsBy))
                    throw new Exception("Theres any element with the given status: " + status);

                return CheckStatusColor(canceledStatusElement, hexacolor);
            }

            //undefined status
            if (!_helper.ElementExist(canceledStatusElement.findsBy))
                throw new Exception("Theres any element with the given status: " + status);

            return CheckStatusColor(undefinedStatusElement, hexacolor);
        }

        public bool ClickOnOrderNumber(string order)
        {
            var orderNumbers = _helper.GetElementsWait(this.orderNumber.findsBy);

            var element = orderNumbers.FirstOrDefault(num => num.Text.Equals(order));

            if (element != null)
            {
                element.Click();

                return true;
            }

            return false;
        }

        public bool ColumnHasNotEmptyValues(string columnHeaderName)
        {
            By columnFindsBy;

            switch (columnHeaderName)
            {
                case "Order #":
                    columnFindsBy = this.orderNumber.findsBy;
                    break;

                case "PO #":
                    columnFindsBy = this.poNumberColumn.findsBy;
                    break;

                case "Date":
                    columnFindsBy = this.dateColumn.findsBy;
                    break;

                case "Status":
                    columnFindsBy = this.statusColumn.findsBy;
                    break;

                //case "Total":
                //    columnFindsBy = this.to.findsBy;
                //break;

                default:
                    throw new ArgumentException("Invalid column name.");
            }

            //TODO: search any empty order number field
            return _helper.GetElementsWait(this.orderNumber.findsBy)
                .All(number => !string.IsNullOrWhiteSpace(number.Text) && !string.IsNullOrEmpty(number.Text));
        }

        public bool DatesAreInSearchRange(DateTime from, DateTime to)
        {
            bool result = true;

            //current dates
            var tableDates = this.GetDatesFromDateColumn();

            //date range
            var dateRange = this.GetDatesOnRange(from, to);

            foreach (var date in tableDates)
            {
                result &= this.DateIsInRange(date, dateRange);
            }

            return result;
        }

        public bool DatesAreInDefaultRange(int days)
        {
            bool result = true;

            //calculate the default range in base with the given days
            var defaultRange = this.GetDatesOnRange(DateTime.Now.AddDays(-days + 1), DateTime.Now);

            var tableDates = this.GetDatesFromDateColumn();

            //compare each date to verify if its in the default range
            foreach (var date in tableDates)
            {
                result &= this.DateIsInRange(date, defaultRange);
            }

            return result;
        }

        public ICollection<string> GetHeaders()
        {
            var tHeads = _helper.GetElementsWait(this.tableHeaders.findsBy);

            var headers = new List<string>();

            foreach (var header in tHeads)
            {
                headers.Add(header.Text);
            }

            return headers;
        }

        public int GetRowsCount()
        {
            return _helper.GetElementsWait(this.rows.findsBy).Count - 1;
        }

        public bool StatusesAreCorrect(string expectedStatus)
        {
            var webStatuses = _helper.GetElementsWait(this.statusColumn.findsBy);

            if (webStatuses.Count <= 0) return false;

            foreach (var webStatus in webStatuses)
            {
                if (!expectedStatus.Equals(webStatus.Text)) return false;
            }

            return true;
        }

        //generic method to check if any column is ordered
        public bool ColumnIsOrdered(string column, bool ascending)
        {
            if (column.Equals("Date")) return this.DatesAreSorted(ascending);

            //case "Status, Total, etc"

            return false;
        }

        public bool DatesAreSorted(bool ascendent)
        {
            var orderDates = this.GetDatesFromDateColumn();

            var orderedDates = DatesManager.SortDates(orderDates, ascendent);

            try
            {
                CollectionAssert.AreEqual(orderedDates.ToList(), orderDates.ToList());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        //private methods
        private bool DateIsInRange(DateTime dt, ICollection<DateTime> dates)
        {
            var datesList = dates.ToArray();
            var lastDate = datesList[datesList.Length - 1];

            if (dt >= datesList[0] && dt <= lastDate) return true;

            return false;
        }

        private ICollection<DateTime> GetDatesOnRange(DateTime from, DateTime to)
        {
            List<DateTime> dates = new List<DateTime>();

            for (var d = from; d <= to; d = d.AddDays(1))
            {
                dates.Add(d.Date);
            }

            return dates;
        }

        public ICollection<string> GetColumnRecords(string columnName)
        {
            IReadOnlyCollection<IWebElement> htmlRecords;

            List<string> results = new List<string>();

            switch (columnName)
            {
                case "PO #":
                    htmlRecords = _helper.GetElementsWait(this.poNumberColumn.findsBy);
                    break;

                case "Web reference #":
                    htmlRecords = _helper.GetElementsWait(this.webReferenceNumberColumn.findsBy);
                    break;

                case "Order #":
                    htmlRecords = _helper.GetElementsWait(this.orderNumber.findsBy);
                    break;

                default:
                    throw new ArgumentException("Invalid column name ;c");
            }

            foreach (var record in htmlRecords)
            {
                results.Add(record.Text);
            }

            return results.ToList();
        }

        //private methods

        private ICollection<DateTime> GetDatesFromDateColumn()
        {
            List<DateTime> dates = new List<DateTime>();

            var datesColumn = _helper.GetElementsWait(this.dateColumn.findsBy);

            foreach (var dateElement in datesColumn)
            {
                dates.Add(DateTime.Parse(dateElement.Text));
            }

            return dates.ToList();
        }

        private bool CheckStatusColor(DomElement row, string hexaColor)
        {
            row.webElement = _helper.GetElement(row.findsBy);

            string cssColor = row.webElement.GetCssValue("color");

            hexaColor = OrdersHelper.ToRGBAColor(hexaColor);

            return hexaColor.Equals(cssColor);
        }
    }
}