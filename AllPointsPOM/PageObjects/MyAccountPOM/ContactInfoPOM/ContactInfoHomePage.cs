using CommonHelper;
using OpenQA.Selenium;
using AllPoints.PageObjects.NewFolder1;
using AllPointsPOM.PageObjects.MyAccountPOM.ContactInfoPOM.Enums;
using System;
using CommonHelper.Pages.ContactInfoPage;
using AllPoints.Pages.Components;

namespace AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM
{
    public class ContactInfoHomePage : BaseContactInfoPage
    {
        public Header Header;
        public AccountMenuLeft AccountMenuLeft;

        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "article.container"
        };
        private DomElement PageTitle = new DomElement
        {
            locator = "h1"
        };

        #region constructor
        public ContactInfoHomePage(IWebDriver driver) : base(driver)
        {
            Header = new Header(driver);
            AccountMenuLeft = new AccountMenuLeft(driver);
            Container.Init(driver, SeleniumConstants.defaultWaitTime);
        }
        #endregion constructor

        //Validate exist title in the page?
        public bool ContactInfoTitleExist()
        {
            DomElement title = Container.GetElementWaitByCSS(PageTitle.locator);

            return title.webElement.Displayed && title.webElement.Enabled;
        }

        public string GetHeadingTitle()
        {
            DomElement hTitle = Container.GetElementWaitByCSS(PageTitle.locator);
            return hTitle.webElement.Text;
        }

        public ContactInfoEditPage ClickOnEditLink()
        {
            DomElement goToEditLink = Container.GetElementWaitByCSS(EditLink.locator);

            goToEditLink.webElement.Click();

            return new ContactInfoEditPage(Driver);
        }

        public string GetContactFieldText(ContactInfoFields field)
        {
            string fieldText = string.Empty;

            var contactSummary = Container.GetElementWaitByCSS(DetailSection.locator).GetElementWaitByCSS(ContactSummaryContainer.locator);

            switch (field)
            {
                case ContactInfoFields.FirstName:
                    fieldText = contactSummary.GetElementWaitByCSS(FirstNameSpan.locator).webElement.Text;
                    break;

                case ContactInfoFields.LastName:
                    fieldText = contactSummary.GetElementWaitByCSS(LastNameSpan.locator).webElement.Text;
                    break;

                case ContactInfoFields.Company:
                    fieldText = contactSummary.GetElementWaitByCSS(CompanyNameSpan.locator).webElement.Text;
                    break;

                case ContactInfoFields.PhoneNumber:
                    fieldText = contactSummary.GetElementWaitByCSS(PhoneNumberSpan.locator).webElement.Text;
                    break;

                case ContactInfoFields.EmailAddress:
                    fieldText = contactSummary.GetElementWaitByCSS(EmailAddressSpan.locator).webElement.Text;
                    break;

                default:
                    throw new Exception($"{field} is not supported yet");
            }

            return fieldText;
        }
    }
}
