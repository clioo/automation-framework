using CommonHelper;
using FMPOnlinePOM.PageObjects.Base.Components;
using OpenQA.Selenium;

namespace FMPOnlinePOM.PageObjects.ProductList
{
    public class ProductItemOffering : ProductItem
    {
        #region locators
        private DomElement ImageSectionAnchor = new DomElement
        {
            locator = ".image-section a"
        };
        private DomElement QuantitySection = new DomElement
        {
            locator = ".quantity-section"
        };
        private DomElement QuantitySectionAddToCartButton = new DomElement
        {
            locator = "button"
        };
        #endregion locators

        public ProductItemOffering(IWebDriver driver, DomElement container) : base(driver, container)
        {

        }

        public ProductDetailPage Click()
        {
            DomElement imageSectionAnchor = Container.GetElementWaitByCSS(ImageSectionAnchor.locator);
            imageSectionAnchor.webElement.Click();
            return new ProductDetailPage(Driver);
        }

        public void ClickOnAddToCart()
        {
            DomElement itemContent = Container.GetElementWaitByCSS(SingleGridItemContent.locator);
            DomElement itemContentSection = itemContent.GetElementWaitByCSS(SingleGridItemContentLineItemSection.locator);
            DomElement quantitySectionContainer = itemContentSection.GetElementWaitByCSS(QuantitySection.locator);
            DomElement quantitySectionButton = quantitySectionContainer.GetElementWaitByCSS(QuantitySectionAddToCartButton.locator);
            quantitySectionButton.webElement.Click();
        }

        //TODO:
        //ClickOnAddToList()
        //SetQuantity()
    }
}
