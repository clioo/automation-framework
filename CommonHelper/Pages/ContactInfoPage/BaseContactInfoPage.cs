using CommonHelper.BaseComponents;
using OpenQA.Selenium;
using System;

namespace CommonHelper.Pages.ContactInfoPage
{
    public abstract class BaseContactInfoPage : BasePOM
    {
        protected DomElement ArticleContainer = new DomElement(By.CssSelector)
        {
            locator = "article.container"
        };
        protected DomElement DetailSection = new DomElement
        {
            locator = ".detail-section"
        };
        protected DomElement HeadingTitle = new DomElement
        {
            locator = "h1"
        };
        #region Contact summary
        protected DomElement ContactSummaryContainer = new DomElement
        {
            locator = ".ci-summary"
        };
        protected DomElement FirstNameSpan = new DomElement
        {
            locator = ".ci-summary-row:nth-of-type(1) .ci-summary-row-value span:nth-of-type(1)"
        };
        protected DomElement LastNameSpan = new DomElement
        {
            locator = ".ci-summary-row:nth-of-type(1) .ci-summary-row-value span:nth-of-type(2)"
        };
        protected DomElement CompanyNameSpan = new DomElement
        {
            locator = ".ci-summary-row:nth-of-type(2) .ci-summary-row-value"
        };
        protected DomElement PhoneNumberSpan = new DomElement
        {
            locator = ".ci-summary-row:nth-of-type(3) .ci-summary-row-value"
        };
        protected DomElement EmailAddressSpan = new DomElement
        {
            locator = ".ci-summary-row:nth-of-type(4) .ci-summary-row-value"
        };
        #endregion Contact summary

        protected DomElement DetailsInfo = new DomElement()
        {
            locator = ".contact-info.detail-section"
        };
        protected DomElement EditLink = new DomElement()
        {
            locator = "a.edit"
        };
        public BaseContactInfoPage(IWebDriver driver) : base(driver) { }

        public string GetHeadingTitleText()
        {
            var contactInfoContainer = BodyContainer.GetElementWaitByCSS(ArticleContainer.locator);
            var h1 = contactInfoContainer.GetElementWaitByCSS(HeadingTitle.locator);
            return h1.webElement.Text;
        }

        protected void SelectEditLink()
        {
            var pageContainer = BodyContainer.GetElementWaitByCSS(ArticleContainer.locator);
            var goToEditLink = pageContainer.GetElementWaitByCSS(EditLink.locator);
            goToEditLink.webElement.Click();
        }

        protected virtual string GetContactTextValue(DomElement field)
        {
            var pageContainer = BodyContainer.GetElementWaitByCSS(ArticleContainer.locator);
            var detailSection = pageContainer.GetElementWaitByCSS(DetailSection.locator);
            var contactSummary = detailSection.GetElementWaitByCSS(ContactSummaryContainer.locator);

            bool fieldExist = contactSummary.IsElementPresent(field.locator);

            if (!fieldExist)
            {
                throw new Exception($"{field} is not supported yet");
            }

            string fieldText = contactSummary.GetElementWaitByCSS(field.locator).webElement.Text;
            return fieldText;
        }
    }
}
