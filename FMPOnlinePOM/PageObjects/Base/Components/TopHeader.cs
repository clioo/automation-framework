using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.HomePage;
using FMPOnlinePOM.PageObjects.ProductList;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FMPOnlinePOM.PageObjects.Base.Components
{
    public class TopHeader : BaseComponent
    {
        #region locators
        private DomElement SecondarySearchContainer = new DomElement(By.CssSelector)
        {
            locator = ".brand-promo"
        };
        private DomElement LogoContainerItem = new DomElement
        {
            locator = ".logo a"
        };
        private DomElement ProductSearchContainer = new DomElement
        {
            locator = ".product-search-container"
        };
        private DomElement ProductSearchContainerManufacturerSelect = new DomElement
        {
            locator = "select"
        };
        private DomElement ProductSearchContainerManufacturerSelectItem = new DomElement
        {
            locator = "option"
        };
        private DomElement ProductSearchContainerTextField = new DomElement
        {
            locator = ".search-input"
        };
        private DomElement ProductSearchContainerSubmitButton = new DomElement
        {
            locator = ".btn"
        };
        #endregion locators

        public TopHeader(IWebDriver driver) : base(driver)
        {
            SecondarySearchContainer.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public List<string> GetManufacturerMenuOptionsText()
        {
            DomElement searchContainer = SecondarySearchContainer.GetElementWaitByCSS(ProductSearchContainer.locator);
            DomElement select = searchContainer.GetElementWaitByCSS(ProductSearchContainerManufacturerSelect.locator);

            select.webElement.Click();
            return select.GetElementsWaitByCSS(ProductSearchContainerManufacturerSelectItem.locator).Select(el => el.webElement.Text).ToList();
        }

        public void SetManufacturer(string manufacturer)
        {
            DomElement searchContainer = SecondarySearchContainer.GetElementWaitByCSS(ProductSearchContainer.locator);
            DomElement searchBar = searchContainer.GetElementWaitByCSS(ProductSearchContainerTextField.locator);
            searchBar.webElement.SendKeys(manufacturer);
        }

        public void SetManufacturerDropdown(string manufacturer)
        {
            DomElement searchContainer = SecondarySearchContainer.GetElementWaitByCSS(ProductSearchContainer.locator);
            DomElement select = searchContainer.GetElementWaitByCSS(ProductSearchContainerManufacturerSelect.locator);

            select.webElement.Click();
            DomElement selectedManufacturer = select.GetElementsWaitByCSS("option").FirstOrDefault((e => e.webElement.Text == manufacturer));
            selectedManufacturer.webElement.Click();
        }

        public ProductListPage ClickOnSubmit()
        {
            DomElement searchContainer = SecondarySearchContainer.GetElementWaitByCSS(ProductSearchContainer.locator);
            DomElement submitButton = searchContainer.GetElementWaitByCSS(ProductSearchContainerSubmitButton.locator);
            submitButton.webElement.Click();

            return new ProductListPage(Driver);
        }

        public IndexHomePage ClickOnLogo()
        {
            DomElement logoItem = SecondarySearchContainer.GetElementWaitByCSS(LogoContainerItem.locator);
            logoItem.webElement.Click();
            return new IndexHomePage(Driver);
        }
    }
}
