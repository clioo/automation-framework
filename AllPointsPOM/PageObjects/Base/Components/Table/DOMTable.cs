using System;
using System.Collections.Generic;
using AllPoints.PageObjects.Base.Components.Contracts;
using OpenQA.Selenium;
using CommonHelper;
using System.Linq;

namespace AllPoints.PageObjects.Base.Components.Table
{
    public class DOMTable : IGenericTable
    {
        DomElement Table;

        #region constructor        
        public DOMTable(IWebDriver driver, DomElement container)
        {
            Table = container;
        }
        #endregion constructor

        public ICollection<string> GetColumns()
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetColumns(int nCol)
        {
            var cols = Table.GetElementsWaitByCSS($"tbody tr td:nth-child({nCol})").Select(el => el.webElement.Text);

            return cols.ToList();
        }

        public ICollection<string> GetColumns(string columnName)
        {
            DomElement column = Table.GetElementsWaitByCSS("thead th").FirstOrDefault(th => th.webElement.Text.Contains(columnName));

            if (column == null) new NotFoundException("Invalid column name");

            int i = Table.GetElementsWaitByCSS("thead th").FindIndex(th => th.webElement.Text.Contains(columnName));

            return GetColumns(i + 1);
        }

        public ICollection<string> GetRows()
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetRows(int nRow)
        {
            throw new NotImplementedException();
        }

        /*
         * static public string Td = "td";
         * static public string Tr = "tr";
         * static public string Th = "th";
         * static public string THead = "thead";
        static public string ColumnLocator = "tbody tr td:nth-child";
        static public string RowLocator = "tbody tr:nth-child";
        */
    }
}
