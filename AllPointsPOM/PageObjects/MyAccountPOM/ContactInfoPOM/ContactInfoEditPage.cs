using System;
using AllPoints.PageObjects.NewFolder1;
using AllPointsPOM.PageObjects.MyAccountPOM.ContactInfoPOM.Enums;
using CommonHelper;
using OpenQA.Selenium;

namespace AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM
{
    public class ContactInfoEditPage : AllPointsBaseWebPage
    {
        #region containers and section title
        private DomElement Container = new DomElement(By.CssSelector)
        {
            locator = "article.container"
        };
        private DomElement ContactInfoContainer = new DomElement
        {
            locator = ".detail-section"
        };
        private DomElement SectionTitle = new DomElement
        {
            locator = "h1"
        };
        #endregion containers and section title

        #region form inputs section
        private DomElement Form = new DomElement
        {
            locator = "form"
        };
        private DomElement FirstNameField = new DomElement
        {
            locator = "input[name='FirstName']"
        };
        private DomElement LastNameField = new DomElement
        {
            locator = "input[name='LastName']"
        };
        private DomElement CompanyLabel = new DomElement
        {
            locator = "span[name='CompanyName']"
        };
        private DomElement PhoneNumberField = new DomElement
        {
            locator = "input[name='PhoneNumber']"
        };
        private DomElement EmailAddressField = new DomElement
        {
            locator = "input[name='ContactEmail']"
        };
        private DomElement FormSubmitButton = new DomElement
        {
            locator = "input[type='submit']"
        };
        private DomElement FormCancelButton = new DomElement
        {
            locator = "input#cancel"
        };
        #endregion form inputs section

        #region form inputs error messages
        private DomElement FirstNameErrorMessage = new DomElement
        {
            locator = "input[name='FirstName'] ~ .help-block span"
        };
        private DomElement LastNameErrorMessage = new DomElement
        {
            locator = "input[name='LastName'] ~ .help-block span"
        };
        private DomElement PhoneNumberErrorMessage = new DomElement
        {
            locator = "input[name='PhoneNumber'] ~ .help-block span"
        };
        private DomElement EmailAddressErrorMessage = new DomElement
        {
            locator = "input[name='ContactEmail'] ~ .help-block span"
        };
        #endregion form inputs error messages

        public ContactInfoEditPage(IWebDriver driver) : base(driver)
        {
            Container.Init(Driver, SeleniumConstants.defaultWaitTime);
        }

        public string GetSectionTitle()
        {
            var element = Container.GetElementWaitByCSS(ContactInfoContainer.locator).GetElementWaitByCSS(SectionTitle.locator);

            return element.webElement.Text;
        }

        public void FillInputText(ContactInfoFields input, string text)
        {
            DomElement contactInfoField = null;
            var contactSectionContainer = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            var editContactForm = contactSectionContainer.GetElementWaitByCSS(Form.locator);

            switch (input)
            {
                case ContactInfoFields.FirstName:
                    contactInfoField = editContactForm.GetElementWaitByCSS(FirstNameField.locator);
                    break;

                case ContactInfoFields.LastName:
                    contactInfoField = editContactForm.GetElementWaitByCSS(LastNameField.locator);
                    break;

                case ContactInfoFields.PhoneNumber:
                    contactInfoField = editContactForm.GetElementWaitByCSS(PhoneNumberField.locator);
                    break;

                case ContactInfoFields.EmailAddress:
                    contactInfoField = editContactForm.GetElementWaitByCSS(EmailAddressField.locator);
                    break;

                default: throw new Exception($"{input} is not supported yet...");
            }

            contactInfoField.webElement.Clear();
            contactInfoField.webElement.SendKeys(text);
        }

        public void TypeOnCompanyName(string text)
        {
            var contactContainer = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            var contactForm = contactContainer.GetElementWaitByCSS(Form.locator);
            var contactInfoField = contactForm.GetElementWaitByCSS(CompanyLabel.locator);

            try
            {
                contactInfoField.webElement.Clear();
                contactInfoField.webElement.SendKeys(text);
            }
            catch (Exception)
            {
                //This is ok. Company name cannot be editable
            }
        }

        public ContactInfoHomePage ClickOnSubmit()
        {
            Form = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            FormSubmitButton = Form.GetElementWaitByCSS(FormSubmitButton.locator);

            if (FormSubmitButton.webElement.Enabled)
            {
                FormSubmitButton.webElement.Click();

                return new ContactInfoHomePage(Driver);
            }
            throw new Exception("The submit button is not enabled");
        }

        public ContactInfoHomePage ClickOnCancel()
        {
            Form = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            FormCancelButton = Form.GetElementWaitByCSS(FormCancelButton.locator);
            FormCancelButton.webElement.Click();

            return new ContactInfoHomePage(Driver);
        }

        public string GetErrorMessageFromInput(ContactInfoFields input)
        {
            var contactContainer = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            var contactForm = contactContainer.GetElementWaitByCSS(Form.locator);
            DomElement inputMessage = null;

            switch (input)
            {
                case ContactInfoFields.FirstName:
                    inputMessage = contactForm.GetElementWaitByCSS(FirstNameErrorMessage.locator);
                    break;

                case ContactInfoFields.LastName:
                    inputMessage = contactForm.GetElementWaitByCSS(LastNameErrorMessage.locator);
                    break;

                case ContactInfoFields.PhoneNumber:
                    inputMessage = contactForm.GetElementWaitByCSS(PhoneNumberErrorMessage.locator);
                    break;

                case ContactInfoFields.EmailAddress:
                    inputMessage = contactForm.GetElementWaitByCSS(EmailAddressErrorMessage.locator);
                    break;

                default:
                    throw new ArgumentException($"{input} is not supported");
            }

            return inputMessage.webElement.Text;
        }

        public bool SubmitButtonIsEnabled()
        {
            Form = Container.GetElementWaitByCSS(ContactInfoContainer.locator);
            FormSubmitButton = Form.GetElementWaitByCSS(FormSubmitButton.locator);

            return FormSubmitButton.webElement.Enabled;
        }
    }
}
