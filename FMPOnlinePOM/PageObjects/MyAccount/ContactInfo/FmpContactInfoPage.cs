using CommonHelper.Pages.ContactInfoPage;
using FMPOnlinePOM.Components;
using FMPOnlinePOM.PageObjects.MyAccount.ContactInfo;
using FMPOnlinePOM.PageObjects.MyAccount.ContactInfo.Enums;
using OpenQA.Selenium;
using System;

namespace FMPOnlinePOM.PageObjects.MyAccount.Addresses
{
    public class FmpContactInfoPage : BaseContactInfoPage
    {
        public FmpHeader Header;

        public FmpContactInfoPage(IWebDriver driver) : base(driver) 
        {
            Header = new FmpHeader(driver);
        }

        public string GetContactInfoValue(FmpContactInfoFieldsEnum field)
        {
            string value;
            switch (field)
            {
                case FmpContactInfoFieldsEnum.FirstName:
                    value = base.GetContactTextValue(FirstNameSpan);
                    break;

                case FmpContactInfoFieldsEnum.LastName:
                    value = base.GetContactTextValue(LastNameSpan);
                    break;

                case FmpContactInfoFieldsEnum.EmailAddress:
                    value = base.GetContactTextValue(EmailAddressSpan);
                    break;

                case FmpContactInfoFieldsEnum.PhoneNumber:
                    value = base.GetContactTextValue(PhoneNumberSpan);
                    break;

                case FmpContactInfoFieldsEnum.Company:
                    value = base.GetContactTextValue(CompanyNameSpan);
                    break;

                default:
                    throw new ArgumentException($"{nameof(field)} is not supported yet");
                
            }
            return value;
        }

        public FmpEditContactInfoPage ClickOnEditLink()
        {
            base.SelectEditLink();
            return new FmpEditContactInfoPage(Driver);
        }
    }
}
