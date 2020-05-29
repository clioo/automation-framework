using CommonHelper.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.BaseComponents
{
    public class HtmlTable : IHtmlTable
    {
        DomElement Container;

        #region constructor        
        public HtmlTable(DomElement container)
        {
            Container = container;
        }
        #endregion constructor

        public IEnumerable<string> GetColumn(string columnName)
        {
            DomElement column = Container.GetElementsWaitByCSS("thead th").FirstOrDefault(th => th.webElement.Text.Contains(columnName));

            if (column == null)
                throw new NotFoundException($"Column: {columnName} is not found");

            int i = Container.GetElementsWaitByCSS("thead th").FindIndex(th => th.webElement.Text.Contains(columnName));

            return GetColumn(i + 1);
        }

        public IEnumerable<string> GetColumn(int colNumber)
        {
            var cols = Container.GetElementsWaitByCSS($"tbody tr td:nth-child({colNumber})").Select(el => el.webElement.Text);

            return cols;
        }

        public IEnumerable<string> GetColumns()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetHeaders()
        {
            return Container.GetElementsWaitByCSS("thead th").Select(th => th.webElement.Text);
        }

        public IEnumerable<string> GetRow(int nRow)
        {
            var rows = Container.GetElementsWaitByCSS($"tbody tr:nth-of-type({nRow})");

            return rows.Select(r => r.webElement.Text);
        }

        public IEnumerable<IEnumerable<string>> GetRows()
        {
            var htmlRows = Container.GetElementsWaitByCSS("tbody tr");

            List<List<string>> rows = new List<List<string>>();

            foreach (var row in htmlRows)
            {
                rows.Add(row.GetElementsWaitByCSS("td").Select(x => x.webElement.Text).ToList());
            }

            return rows;
        }
    }
}
