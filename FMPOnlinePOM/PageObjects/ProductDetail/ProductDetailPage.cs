using CommonHelper;
using FMPOnlinePOM.Base;
using FMPOnlinePOM.PageObjects.ProductDetail;
using OpenQA.Selenium;

namespace FMPOnlinePOM
{
    public class ProductDetailPage : FmpBasePage
    {
        private DomElement ProductDetalContainer = new DomElement
        {
            locator = ".offering-detail"
        };
        private DomElement ProductSection = new DomElement
        {
            locator = "section.product-section"
        };
        private DomElement PriceContainer = new DomElement
        {
            locator = ".price-container"
        };
        private DomElement PriceTagContent = new DomElement
        {
            locator = ".price-availability-container"
        };
        private DomElement PriceValueContainer = new DomElement
        {
            locator = ".offering-price"
        };
        private DomElement PriceCurrency = new DomElement
        {
            locator = "span.currency"
        };
        private DomElement PriceValue = new DomElement
        {
            locator = "span.price"
        };
        private DomElement PriceUnit = new DomElement
        {
            locator = "span.price-units"
        };

        #region constructor
        public ProductDetailPage(IWebDriver driver) : base(driver)
        {
            
        }
        #endregion constructor

        public string GetProductSection(ProductDetailSections section)
        {
            DomElement productDetailContainer = BodyContainer.GetElementWaitByCSS(ProductDetalContainer.locator);
            DomElement productSection = productDetailContainer.GetElementWaitByCSS(ProductSection.locator);

            switch (section)
            {
                case ProductDetailSections.PriceTag:
                    DomElement priceContainer = productSection.GetElementWaitByCSS(PriceContainer.locator);
                    return priceContainer.GetElementWaitByCSS(PriceTagContent.locator).webElement.Text;

                default: throw new System.ArgumentException($"{section} is not supported yet");
            }
        }
    }
}
