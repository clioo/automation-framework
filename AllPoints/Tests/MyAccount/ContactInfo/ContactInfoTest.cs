using AllPoints.Constants;
using AllPoints.Models;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.Pages;
using AllPointsPOM.PageObjects.MyAccountPOM.ContactInfoPOM.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AllPoints.Features.MyAccount.ContactInfo
{
    [TestClass]
    public class ContactInfoTest : FeatureBase
    {
        private ContactInfoDataFactoryV2 TestDataFactory;

        #region constructor
        public ContactInfoTest()
        {
            TestDataFactory = new ContactInfoDataFactoryV2();
        }
        #endregion constructor

        #region View
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateContactInfoIsDislayed()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            string expectedHeading = "Contact information";
            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testData.Email, testData.Password);
            ContactInfoHomePage contactInfoHomePage = indexPage.Header.ClickOnContactInfo();

            //Validate that it is de correct page
            Assert.IsTrue(contactInfoHomePage.ContactInfoTitleExist(), "Contact information title does not exist");
            Assert.AreEqual(contactInfoHomePage.GetHeadingTitle(), expectedHeading, $"{expectedHeading} title is incorrect");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateContactInfoDetails()
        {
            var testData = TestDataFactory.UserContactCreate();
            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testData.Email, testData.Password);
            ContactInfoHomePage contactInfoHomePage = indexPage.Header.ClickOnContactInfo();

            Assert.AreEqual(testData.Contact.FirstName, contactInfoHomePage.GetContactFieldText(ContactInfoFields.FirstName));
            Assert.AreEqual(testData.Contact.LastName, contactInfoHomePage.GetContactFieldText(ContactInfoFields.LastName));
            Assert.AreEqual(testData.Contact.Company, contactInfoHomePage.GetContactFieldText(ContactInfoFields.Company));
            Assert.AreEqual(testData.Contact.PhoneNumber, contactInfoHomePage.GetContactFieldText(ContactInfoFields.PhoneNumber));
            Assert.AreEqual(testData.Contact.Email, contactInfoHomePage.GetContactFieldText(ContactInfoFields.EmailAddress));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateEditLink()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            var indexPage = new IndexPage(driver, url);

            var loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.Email, testData.Password);

            var contactInfoHomePage = indexPage.Header.ClickOnContactInfo();

            var contactInfoEditPage = contactInfoHomePage.ClickOnEditLink();

            Assert.IsNotNull(contactInfoEditPage, "Link does not go to Edit page");
        }
        #endregion View

        #region Edit
        //Test case on test rail -> C1133
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditContactInfoPOMElementsValidation()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            string expectedSectionTitle = "Edit contact information";
            string unexpectedText = "new company name";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testData.Email, testData.Password);

            ContactInfoHomePage contactInfoPage = loginPage.Header.ClickOnContactInfo();
            ContactInfoEditPage editContactInfoPage = contactInfoPage.ClickOnEditLink();

            string actualSectionTitle = editContactInfoPage.GetSectionTitle();
            editContactInfoPage.TypeOnCompanyName(unexpectedText);
            contactInfoPage = editContactInfoPage.ClickOnSubmit();

            Assert.IsTrue(expectedSectionTitle == actualSectionTitle, $"{actualSectionTitle} is not different to the expected ({expectedSectionTitle})");
            Assert.IsFalse(contactInfoPage.GetContactFieldText(ContactInfoFields.Company) == unexpectedText, "The company name cannot be editable!!");
        }

        //Test case on test rail -> C1136
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ErrorHandlingEditContactInfoForm()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            #region test data
            string invalidPhone = "ae34";
            string invalidEmail = "!_+++_oneemail@";
            #endregion test data
            string expectedEmptyFirstNameErrorMessage = "First name is required";
            string expectedEmptyLastNameErrorMessage = "Last name is required";
            string expectedEmptyPhoneNumberErrorMessage = "Phone number is required";
            string expectedWrongPhoneNumberErrorMessage = "The phone number entered is not valid";
            string expectedEmptyEmailErrorMessage = "Email address is required";
            string expectedWrongEmailErrorMessage = "The email address entered is not valid";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testData.Email, testData.Password);

            ContactInfoHomePage contactInfoPage = loginPage.Header.ClickOnContactInfo();
            ContactInfoEditPage editContactInfoPage = contactInfoPage.ClickOnEditLink();

            //leave each input empty
            editContactInfoPage.FillInputText(ContactInfoFields.FirstName, "");
            editContactInfoPage.FillInputText(ContactInfoFields.LastName, "");
            editContactInfoPage.FillInputText(ContactInfoFields.PhoneNumber, "");
            editContactInfoPage.FillInputText(ContactInfoFields.EmailAddress, "");

            //loose the focus on the email input, perform a tabulation key pressing
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Tab);
            action.Build().Perform();

            //Get actual error messages
            string actualFirstNameErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.FirstName);
            string actualLastNameErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.LastName);
            string actualEmptyPhoneErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.PhoneNumber);
            string actualEmptyEmailErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.EmailAddress);

            //send invalid inputs
            editContactInfoPage.FillInputText(ContactInfoFields.PhoneNumber, invalidPhone);
            editContactInfoPage.FillInputText(ContactInfoFields.EmailAddress, invalidEmail);

            //Get actual error messages
            string actualWrongPhoneErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.PhoneNumber);
            string actualWrongEmailErrorMessage = editContactInfoPage.GetErrorMessageFromInput(ContactInfoFields.EmailAddress);

            //empty error messages validations
            Assert.IsTrue(expectedEmptyFirstNameErrorMessage == actualFirstNameErrorMessage, $"{actualFirstNameErrorMessage} is not as expected");
            Assert.IsTrue(expectedEmptyLastNameErrorMessage == actualLastNameErrorMessage, $"{actualLastNameErrorMessage} is not as expected");
            Assert.IsTrue(expectedEmptyPhoneNumberErrorMessage == actualEmptyPhoneErrorMessage, $"{actualEmptyPhoneErrorMessage} is not as expected");
            Assert.IsTrue(expectedEmptyEmailErrorMessage == actualEmptyEmailErrorMessage, $"{actualEmptyEmailErrorMessage} is not as expected");

            //wrong messages validations
            Assert.IsTrue(expectedWrongPhoneNumberErrorMessage == actualWrongPhoneErrorMessage, $"'{actualWrongPhoneErrorMessage}' is not as expected: '{expectedWrongPhoneNumberErrorMessage}'");
            Assert.IsTrue(expectedWrongEmailErrorMessage == actualWrongEmailErrorMessage, $"{actualWrongEmailErrorMessage} is not as expected");

            //validate the update button is disabled if any input has an error message
            Assert.IsFalse(editContactInfoPage.SubmitButtonIsEnabled(), "The submit button should not be enabled");
        }

        //Test case on test rail -> C1141
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditContactInformation()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            var expectedContactInfo = new ContactInfoModel
            {
                FirstName = "john",
                LastName = "doe",
                Email = "someemail@email.com",
                PhoneNumber = "1231230312"
            };
            string expectedTitle = "Contact information";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testData.Email, testData.Password);

            ContactInfoHomePage contactInfoPage = loginPage.Header.ClickOnContactInfo();
            //store the company name before changes
            expectedContactInfo.Company = contactInfoPage.GetContactFieldText(ContactInfoFields.Company);

            ContactInfoEditPage editContactInfoPage = contactInfoPage.ClickOnEditLink();

            editContactInfoPage.FillInputText(ContactInfoFields.FirstName, expectedContactInfo.FirstName);
            editContactInfoPage.FillInputText(ContactInfoFields.LastName, expectedContactInfo.LastName);
            editContactInfoPage.FillInputText(ContactInfoFields.PhoneNumber, expectedContactInfo.PhoneNumber);
            editContactInfoPage.FillInputText(ContactInfoFields.EmailAddress, expectedContactInfo.Email);
            contactInfoPage = editContactInfoPage.ClickOnSubmit();

            string actualContactFirstName = contactInfoPage.GetContactFieldText(ContactInfoFields.FirstName);
            string actualContactLastName = contactInfoPage.GetContactFieldText(ContactInfoFields.LastName);
            string actualContactCompanyName = contactInfoPage.GetContactFieldText(ContactInfoFields.Company);
            string actualContactPhone = contactInfoPage.GetContactFieldText(ContactInfoFields.PhoneNumber);
            string actualContactEmail = contactInfoPage.GetContactFieldText(ContactInfoFields.EmailAddress);

            //validate redirection is to contact info page
            Assert.IsTrue(driver.Title == expectedTitle, $"The page title is not {expectedTitle}");
            Assert.IsTrue(actualContactFirstName == expectedContactInfo.FirstName, $"{actualContactFirstName} is different from {expectedContactInfo.FirstName}");
            Assert.IsTrue(actualContactLastName == expectedContactInfo.LastName, $"{actualContactLastName} is different from {expectedContactInfo.LastName}");
            Assert.IsTrue(actualContactCompanyName == expectedContactInfo.Company, $"{actualContactCompanyName} is different from {expectedContactInfo.Company}");
            Assert.IsTrue(actualContactPhone == expectedContactInfo.PhoneNumber, $"{actualContactPhone} is different from {expectedContactInfo.PhoneNumber}");
            Assert.IsTrue(actualContactEmail == expectedContactInfo.Email, $"{actualContactEmail} is different from {expectedContactInfo.Email}");
        }
        #endregion Edit
    }
}
