using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.PageObjects.MyAccountPOM.ContactInfoPOM;
using AllPoints.Pages;
using AllPointsPOM.PageObjects.MyAccountPOM.ContactInfoPOM.Enums;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading.Tasks;

namespace AllPoints.Features.MyAccount.ContactInfo
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.ReadyToGo)]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class ContactInfoTest : AllPointsBaseTest
    {
        #region View
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void ValidateContactInfoIsDislayed()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            string expectedHeading = "Contact information";

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            ContactInfoHomePage contactInfoHomePage = indexPage.Header.ClickOnContactInfo();

            //Validate that it is de correct page
            Assert.IsTrue(contactInfoHomePage.ContactInfoTitleExist(), "Contact information title does not exist");
            Assert.AreEqual(contactInfoHomePage.GetHeadingTitle(), expectedHeading, $"{expectedHeading} title is incorrect");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateContactInfoDetails()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            ContactInfoHomePage contactInfoHomePage = indexPage.Header.ClickOnContactInfo();

            Assert.AreEqual(testUser.ContactInformation.FirstName, contactInfoHomePage.GetContactFieldText(ContactInfoFields.FirstName));
            Assert.AreEqual(testUser.ContactInformation.LastName, contactInfoHomePage.GetContactFieldText(ContactInfoFields.LastName));
            Assert.AreEqual(testUser.ContactInformation.CompanyName, contactInfoHomePage.GetContactFieldText(ContactInfoFields.Company));
            Assert.AreEqual(testUser.ContactInformation.PhoneNumber, contactInfoHomePage.GetContactFieldText(ContactInfoFields.PhoneNumber));
            Assert.AreEqual(testUser.ContactInformation.Email, contactInfoHomePage.GetContactFieldText(ContactInfoFields.EmailAddress));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateEditLink()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            var indexPage = new APIndexPage(Driver, Url);
            var loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            var contactInfoHomePage = indexPage.Header.ClickOnContactInfo();
            var contactInfoEditPage = contactInfoHomePage.ClickOnEditLink();

            Assert.IsNotNull(contactInfoEditPage, "Link does not go to Edit page");
        }
        #endregion View

        #region Edit
        //Test case on test rail -> C1133
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditContactInfoElementsValidation()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            string expectedSectionTitle = "Edit contact information";
            string unexpectedText = "new company name";

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testUser.Username, testUser.Password);

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
            var testUser = DataFactory.Users.CreateTestUser();

            //test data
            string invalidPhone = "ae34";
            string invalidEmail = "!_+++_oneemail@";

            //expected
            string expectedEmptyFirstNameErrorMessage = "First name is required";
            string expectedEmptyLastNameErrorMessage = "Last name is required";
            string expectedEmptyPhoneNumberErrorMessage = "Phone number is required";
            string expectedWrongPhoneNumberErrorMessage = "The phone number entered is not valid";
            string expectedEmptyEmailErrorMessage = "Email address is required";
            string expectedWrongEmailErrorMessage = "The email address entered is not valid";

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testUser.Username, testUser.Password);

            ContactInfoHomePage contactInfoPage = loginPage.Header.ClickOnContactInfo();
            ContactInfoEditPage editContactInfoPage = contactInfoPage.ClickOnEditLink();

            //leave each input empty
            editContactInfoPage.FillInputText(ContactInfoFields.FirstName, "");
            editContactInfoPage.FillInputText(ContactInfoFields.LastName, "");
            editContactInfoPage.FillInputText(ContactInfoFields.PhoneNumber, "");
            editContactInfoPage.FillInputText(ContactInfoFields.EmailAddress, "");

            //loose the focus on the email input, perform a tabulation key pressing
            Actions action = new Actions(Driver);
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
            var testUser = DataFactory.Users.CreateTestUser();

            //expected contact information
            string expectedFirstName = "john";
            string expectedLastName = "doe";
            string expectedEmail = "someemail@email.com";
            string expectedPhoneNumber = "1231230312";
            string expectedCompanyName = string.Empty;
            string expectedTitle = "Contact information";

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testUser.Username, testUser.Password);

            ContactInfoHomePage contactInfoPage = loginPage.Header.ClickOnContactInfo();
            //store the company name before changes
            expectedCompanyName = contactInfoPage.GetContactFieldText(ContactInfoFields.Company);

            ContactInfoEditPage editContactInfoPage = contactInfoPage.ClickOnEditLink();

            editContactInfoPage.FillInputText(ContactInfoFields.FirstName, expectedFirstName);
            editContactInfoPage.FillInputText(ContactInfoFields.LastName, expectedLastName);
            editContactInfoPage.FillInputText(ContactInfoFields.PhoneNumber, expectedPhoneNumber);
            editContactInfoPage.FillInputText(ContactInfoFields.EmailAddress, expectedEmail);
            contactInfoPage = editContactInfoPage.ClickOnSubmit();

            string actualContactFirstName = contactInfoPage.GetContactFieldText(ContactInfoFields.FirstName);
            string actualContactLastName = contactInfoPage.GetContactFieldText(ContactInfoFields.LastName);
            string actualContactCompanyName = contactInfoPage.GetContactFieldText(ContactInfoFields.Company);
            string actualContactPhone = contactInfoPage.GetContactFieldText(ContactInfoFields.PhoneNumber);
            string actualContactEmail = contactInfoPage.GetContactFieldText(ContactInfoFields.EmailAddress);

            //validate redirection is to contact info page
            Assert.IsTrue(Driver.Title == expectedTitle, $"The page title is not {expectedTitle}");
            Assert.IsTrue(actualContactFirstName == expectedFirstName, $"{actualContactFirstName} is different from {expectedFirstName}");
            Assert.IsTrue(actualContactLastName == expectedLastName, $"{actualContactLastName} is different from {expectedLastName}");
            Assert.IsTrue(actualContactCompanyName == expectedCompanyName, $"{actualContactCompanyName} is different from {expectedCompanyName}");
            Assert.IsTrue(actualContactPhone == expectedPhoneNumber, $"{actualContactPhone} is different from {expectedPhoneNumber}");
            Assert.IsTrue(actualContactEmail == expectedEmail, $"{actualContactEmail} is different from {expectedEmail}");
        }
        #endregion Edit
    }
}
