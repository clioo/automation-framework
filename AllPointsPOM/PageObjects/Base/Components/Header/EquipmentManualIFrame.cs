using AllPoints.PageObjects.NewFolder1;
using CommonHelper;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AllPoints.Pages.Components
{
    public class EquipmentManualIframe : AllPointsBaseWebPage
    {

        private string FrameName = "davisware-iframe";

        private string parentWindow;

        private IWebDriver iFrame;

        #region containers
        private DomElement FormContainer = new DomElement(By.CssSelector) { locator = "form#form1" };

        private DomElement TableParts = new DomElement(By.CssSelector) { locator = "table#dgvpartResult tbody" };


        #endregion

        public EquipmentManualIframe(IWebDriver driver) : base(driver)
        {
            parentWindow = Driver.WindowHandles[0];


        }

        #region stub for future automation tasks
        public void AddToCartPartNumberWithQty(string partNumber)
        {


            // similar to select part number but modifying the quantity to add to cart
        }
        #endregion

        #region Search submenu slects
        public void AddToCartPartNumber(string partNumber)
        {
            iFrame = Driver.SwitchTo().Frame(FrameName);
            TableParts.Init(iFrame, SeleniumConstants.defaultWaitTime);
            //IWebElement partsTable = iFrame.FindElement(By.CssSelector(TableParts.locator));
            List<DomElement> rows = TableParts.GetElementsWaitByCSS("tr");

            int columnNum = GetColumnForHeader(rows, "PART #");
            int rowNum = GetRowNumberForItem(rows, columnNum, partNumber);

            //rows[rowNum - 1].GetElementWaitByCSS("td > div.gridconqty input").webElement.Click();

            var addBtn = rows[rowNum - 1].GetElementWaitByCSS("td > div.gridconqty input").webElement;

            if (addBtn == null) throw new NotFoundException($"{partNumber} is not found in the table");

            addBtn.Click();

            Driver.SwitchTo().Window(parentWindow);

        }
        #endregion

        #region internal functions 

        private int GetColumnForHeader(List<DomElement> table, string header)
        {
            for (int i = 0; i < table.Count; i++)
            {
                List<DomElement> columnas = table[i].GetElementsWaitByCSS("th");
                for (int j = 0; j < columnas.Count; j++)
                {
                    //columnas[j];
                    if (columnas[j].webElement.Text.Equals(header))
                    {
                        return j + 1;
                        // j = columnas.Count;
                    }
                }
            }
            return 0;
        }

        private int GetRowNumberForItem(List<DomElement> table, int columNum, string item)
        {
            for (int i = 0; i < table.Count; i++)
            {
                List<DomElement> row = table[i].GetElementsWaitByCSS("td:nth-child(3)");
                //columnas[j];
                for (int j = 0; j < row.Count; j++)
                {
                    //columnas[j];
                    if (row[j].webElement.Text.Equals(item))
                    {
                        return i + 1;
                        // j = row.Count;
                    }
                }
            }
            return 0;
        }
        #endregion
    }
}

