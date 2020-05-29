using CommonHelper;
using FMPOnlinePOM.Base;
using FMPOnlinePOM.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FMPOnlinePOM.PageObjects.ProductList
{
    public class ProductListPage : FmpBasePage
    {
        #region page locators
        private DomElement SectionContainer = new DomElement
        {
            locator = ".offering-grid"
        };
        private DomElement ItemsGridDivContainer = new DomElement
        {
            locator = ".detail-section:nth-of-type(4)"
        };
        private DomElement ItemsGridUlContainer = new DomElement
        {
            locator = "ul.grid-items"
        };
        //cart product item container
        private DomElement SingleGridItem = new DomElement
        {
            locator = ".grid-item"
        };
        #endregion page locators

        #region constructor
        public ProductListPage(IWebDriver driver) : base(driver)
        {

        }
        #endregion constructor

        public List<ProductItemOffering> GetResultItems()
        {
            List<ProductItemOffering> itemsNamesCollection = new List<ProductItemOffering>();
            SectionContainer = BodyContainer.GetElementWaitByCSS(SectionContainer.locator);
            DomElement divContainer = SectionContainer.GetElementWaitByCSS(ItemsGridDivContainer.locator);
            DomElement ulContainer = divContainer.GetElementWaitByCSS(ItemsGridUlContainer.locator);
            List<DomElement> items = ulContainer.GetElementsWaitByCSS(SingleGridItem.locator);

            foreach (var item in items)
            {                
                itemsNamesCollection.Add(new ProductItemOffering(Driver, item));
            }
            return itemsNamesCollection;
        }
    }
}