using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.Base.Components.Enums;
using OpenQA.Selenium;
using System;

namespace FMPOnlinePOM.PageObjects.Base.Components
{
    public class MiniCart : BaseComponent
    {
        #region locators section
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "article.mini-cart"
        };
        private DomElement SectionNotifications = new DomElement
        {
            locator = "section.notifications"
        };
        //TODO: 
        //add notification message text dom element
        private DomElement SectionAmounts = new DomElement
        {
            locator = "section.amounts"
        };
        private DomElement AmountQuantitySection = new DomElement
        {
            locator = ".quantity"
        };
        //TODO:
        //add quantity tag
        //quantity value
        private DomElement AmountUnitPriceSection = new DomElement
        {
            locator = ".unit-price"
        };
        private DomElement AmountUnitPriceSectionPriceTag = new DomElement
        {
            locator = "label"
        };
        private DomElement AmountUnitPriceSectionPriceValue = new DomElement
        {
            locator = "span.value"
        };
        private DomElement AmountTotalPrice = new DomElement
        {
            locator = ".total-price"
        };
        //TODO:
        //add total tag
        //value
        private DomElement AmountSubtotalPriceSection = new DomElement
        {
            locator = ".subtotal-price"
        };
        //TODO:
        //add actions section
        //action view cart button
        //continue shopping action link
        private DomElement SectionActions = new DomElement
        {
            locator = "section.actions"
        };
        #endregion locators section

        #region constructor
        public MiniCart(IWebDriver driver) : base(driver)
        {
            //do not initialize the minicart container here
        }
        #endregion constructor

        public string GetSection(MiniCartSections section)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);

            switch (section)
            {
                case MiniCartSections.PricingTag:
                    DomElement amountContainer = Container.GetElementWaitByCSS(SectionAmounts.locator);
                    DomElement amountUnitPriceContainer = amountContainer.GetElementWaitByCSS(AmountUnitPriceSection.locator);
                    return amountUnitPriceContainer.GetElementWaitByCSS(AmountUnitPriceSectionPriceTag.locator).webElement.Text;
                default:
                    throw new ArgumentException($"Section {section} is not supported");
            }
        }
    }
}
