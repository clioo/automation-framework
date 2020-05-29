using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using OpenQA.Selenium;
using System;

namespace FMPOnlinePOM.PageObjects.Base.Components
{
    public class ProductItem : BaseComponent
    {
        #region locators
        protected DomElement Container;
        protected DomElement SingleGridItemContent = new DomElement
        {
            locator = ".content-section"
        };
        protected DomElement SingleGridItemContentTitleSection = new DomElement
        {
            locator = ".title-section"
        };
        protected DomElement SingleGridItemContentLineItemSection = new DomElement
        {
            locator = ".line-item-section"
        };
        protected DomElement SingleGridItemContentLineItemSectionPriceSection = new DomElement
        {
            locator = ".price-section"
        };
        protected DomElement SingleGridItemContentLineItemSectionPriceSectionName = new DomElement
        {
            locator = ".line-label"
        };
        protected DomElement SingleGridItemContentLineItemSectionPriceSectionValue = new DomElement
        {
            locator = ".line-value"
        };
        protected DomElement SingleGridItemContentLineItemSectionSkuSection = new DomElement
        {
            locator = ".sku-section"
        };
        protected DomElement SingleGridItemContentLineItemSectionSkuSectionName = new DomElement
        {
            locator = "span.sku-label"
        };
        protected DomElement SingleGridItemContentLineItemSectionSkuSectionValue = new DomElement
        {
            locator = "span.sku"
        };
        #endregion locators

        #region constructor
        public ProductItem(IWebDriver driver, DomElement container) : base(driver)
        {
            Container = container;
        }
        #endregion constructor

        public string GetItemDetail(ProductItemDetailsEnum detail)
        {
            DomElement contentSection = Container.GetElementWaitByCSS(SingleGridItemContent.locator);
            DomElement lineItemSection = contentSection.GetElementWaitByCSS(SingleGridItemContentLineItemSection.locator);
            DomElement lineItemSectionPriceSection = lineItemSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionPriceSection.locator);
            DomElement skuSection = lineItemSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionSkuSection.locator);

            switch (detail)
            {
                case ProductItemDetailsEnum.PriceTag:
                    DomElement lineItemSectionPriceSectionLabel = lineItemSectionPriceSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionPriceSectionName.locator);
                    return lineItemSectionPriceSectionLabel.webElement.Text;

                //TODO:
                //add item sections
                /*
                DomElement titleSection = contentSection.GetElementWaitByCSS(SingleGridItemContentTitleSection.locator);
                DomElement lineItemSectionPriceSectionValue = lineItemSectionPriceSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionPriceSectionValue.locator);
                DomElement skuName = skuSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionSkuSectionName.locator);
                DomElement skuValue = skuSection.GetElementWaitByCSS(SingleGridItemContentLineItemSectionSkuSectionValue.locator);
                */

                default:
                    throw new ArgumentException($"{detail} is not supported");
            }
        }
    }
}