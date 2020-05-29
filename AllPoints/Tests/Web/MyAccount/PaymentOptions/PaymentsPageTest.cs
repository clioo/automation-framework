using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.Features.Models;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using AllPointsPOM.PageObjects.MyAccountPOM.Enums;
using AllPointsPOM.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AllPoints.Features.MyAccount.PaymentOptions
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class PaymentsPageTest : AllPointsBaseTest
    {

        #region View
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void AccountLevelSectionIsDisplayed()
        {
            var testUser = DataFactory.Users.CreateTestUserWithTerms("30 days cool stuff");

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            bool accountLevelExist = viewPaymentsPage.AccountLevelExist();

            Assert.IsTrue(accountLevelExist, "Account section is not displayed");
        }

        //Test Case on test rail -> C1101, C1121
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void AccountSectionNotDisplayed()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            string term = "40 Net Days, cool Stuff";

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();
            bool accountLevelExist = viewPaymentsPage.AccountLevelExist();

            Assert.IsFalse(accountLevelExist, "Account section is not hidden as expected");

            //check for user: cannot have terms
            var paymentsInDropdown = viewPaymentsPage.GetPaymentsDropdownItems(AccessLevel.User);
            string termInDropdown = paymentsInDropdown.FirstOrDefault(item => item.Equals(term));

            Assert.IsNull(termInDropdown, "User level cannot have terms");

        }

        //TODOS
        //pending to rework
        //dependency on account level payment data factory creation
        //Test Case on test rail -> C1119, C1120 C1122, C1123
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        //[TestCategory(TestCategoriesConstants.Smoke)]
        //public void SetAccountLevelPaymentAsDefault

        //Test Case on test rail -> C1355
        //not passing busy app
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void SetTermAsDefaultPayment()
        {
            string termDetail = "40 Net Days";
            var testUser = DataFactory.Users.CreateTestUserWithTerms(termDetail);

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //async Task actions
            viewPaymentsPage.ClickOnPaymentOption(AccessLevel.Account, termDetail);
            viewPaymentsPage.ContentModal.ClickOnMakeDefault();
            viewPaymentsPage.WaitForAppBusy();
            viewPaymentsPage.InfoModal.ClickOnClose();
            string actualDefaultPaymentData = viewPaymentsPage.GetDefaultTilePaymentData(AccessLevel.Account);

            Assert.IsTrue(actualDefaultPaymentData.Contains(termDetail));
        }

        //Test Case on test rail -> C1127, C1120
        //not passing
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void SetUserLevelPaymentAsDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            TestAddress testAddress = new TestAddress
            {
                Country = "US",
                Name = "QA Softtek Automation",
                StateProvinceRegion = "CO",
                City = "Denver",
                Street = "Elm street",
                Postal = "22780",
                Apartment = "24"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //add payment process
            PaymentOptionsCreatePage addNewPaymentPage = viewPaymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testAddress.Name);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, testAddress.Street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testAddress.Apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, testAddress.City);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, testAddress.StateProvinceRegion);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, testAddress.Postal);
            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.WaitForAppBusy();
            viewPaymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //validations begins here
            viewPaymentsPage.ClickOnPaymentOption(AccessLevel.User, testCardtoken);
            viewPaymentsPage.ContentModal.ClickOnMakeDefault();
            viewPaymentsPage.WaitForAppBusy();
            viewPaymentsPage.InfoModal.ClickOnClose();

            bool defaultLabelInDropdownItem = viewPaymentsPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);

            List<string> paymentsInDropdown = viewPaymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string firstPayment = paymentsInDropdown.ElementAt(0);
            string actualDefaultPaymentData = viewPaymentsPage.GetDefaultTilePaymentData(AccessLevel.User);

            Assert.IsTrue(defaultLabelInDropdownItem, "default fayment has not a label");
            //sort order check
            //Assert.IsTrue(firstPayment.Contains(stringTestPayment), "first payment is not the default one");
            Assert.IsTrue(actualDefaultPaymentData.Contains(testCardtoken.LastFourDigits));
            Assert.IsTrue(actualDefaultPaymentData.Contains($"{testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}"));

            bool accountTileIsPresent = viewPaymentsPage.ElementExistOnPage(AccessLevel.Account, ViewPaymentsElements.DefaultTile);

            Assert.IsFalse(accountTileIsPresent, "Account default tile should not exist here o:");
        }

        //TODO
        //needs rework on test data generation
        //Test case on test rail -> C1392
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        //public async Task MoreThan20CreditCardsOnUserLevelCase()

        //TODO
        //needs rework on test data generation
        //Test Case on test rail -> C1102
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        //public async Task MoreThan20CreditCardsOnAccountLevelCase()        
        #endregion View

        #region delete
        //Test Case on test rail -> C1142
        //not passing busy app
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void DeleteUserLevelPayment()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            #region testData
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            TestAddress testAddress = new TestAddress
            {
                Country = "US",
                Name = "QA Softtek Automation",
                StateProvinceRegion = "CO",
                City = "Denver",
                Street = "Elm street",
                Postal = "22780",
                Apartment = "24"
            };
            #endregion testData

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.ClickOnBillingAddressOption(BillingAddressOptions.NewOne);

            //Fill billing address form
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testAddress.Name);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, testAddress.Street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testAddress.Apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, testAddress.City);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, testAddress.StateProvinceRegion);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, testAddress.Postal);

            bool makeDefaultCheckboxExist = addNewPaymentPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.WaitForAppBusy();
            paymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //start deleting the payment
            int paymentsCountBefore = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList().Count;

            paymentsPage.ClickOnPaymentOption(
            AccessLevel.User,
            $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}");

            Thread.Sleep(2000);
            paymentsPage.ContentModal.ClickOnDelete();
            Thread.Sleep(1000);
            paymentsPage.ConfirmationModal.ClickOnDelete();
            paymentsPage.WaitForAppBusy();

            int paymentsCountAfter = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList().Count;

            Assert.IsTrue(paymentsCountAfter == (paymentsCountBefore - 1), "Payment has not deleted");
        }
        #endregion delete

        #region Add
        //TODO
        //.modal-dialog .modal-content NoSuchElementException
        //Test Case on test rail -> C1134, C1135, C1179
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void AddCreditCardUsingPrevStoredAddress()
        {
            var testData = DataFactory.Users.CreateTestUser();
            TestAddress testAddress = new TestAddress
            {
                Country = "US",
                Street = "street test",
                Name = "address company name",
                StateProvinceRegion = "CO",
                City = "boulder",
                Postal = "52809"
            };
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "22",
                HolderName = "Test holder",
                Cvv = "077",
                LastFourDigits = "1111"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testData.Username, testData.Password);

            //Manually add an address
            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.Name);
            createAddressPage.SetInputValue(AddressInputs.Street, testAddress.Street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.Apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testAddress.City);
            createAddressPage.SetInputValue(AddressInputs.State, testAddress.StateProvinceRegion);
            createAddressPage.SetInputValue(AddressInputs.Postal, testAddress.Postal);

            addressesPage = createAddressPage.ClickOnSubmit();
            addressesPage.WaitForAppBusy();
            //TODO: check the logic on address create process
            addressesPage.WaitForAppBusy();
            addressesPage.InformationModal.ClickOnClose();

            //test begins here
            PaymentOptionsHomePage paymentsPage = addressesPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();

            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.Stored);
            addNewPaymentPage.SelectPreviouslyStoredAddress(testAddress);
            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.WaitForAppBusy();
            paymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            addNewPaymentPage.WaitForAppBusy();
            Thread.Sleep(500);

            List<string> items = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string payment = items.FirstOrDefault(it => it.Contains($"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}"));

            Assert.IsNotNull(payment, "Payment option is not found");
        }

        //Test Case on test rail -> C1144, C1145, C1146, C1179
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void AddCreditCardUsingNewAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            #region test data models
            var cardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            var address = new TestAddress
            {
                Country = "US",
                Name = "QA Softtek Automation",
                StateProvinceRegion = "CO",
                City = "Denver",
                Street = "Elm street",
                Postal = "22780",
                Apartment = "24"
            };
            #endregion

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);

            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(cardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);

            //Fill billing address form
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, address.Name);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, address.Street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, address.Apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, address.City);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, address.StateProvinceRegion);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, address.Postal);

            bool makeDefaultCheckboxExist = addNewPaymentPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.WaitForAppBusy();
            paymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            List<string> items = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string payment = items.FirstOrDefault(it => it.Contains($"{cardtoken.LastFourDigits} {cardtoken.ExpirationMont}/{cardtoken.ExpirationYear}"));

            Assert.IsNotNull(payment, "Payment is not found");
        }

        //TODO
        //NoSuchElementException (.modal-dialog .modal-content)
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        //[TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void AddPaymentOptionIntlAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();

            #region test data
            var testCardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            var testBillingAddress = new TestAddress
            {
                Country = "MX",
                Name = "SDET",
                StateProvinceRegion = "Estado de baja california",
                City = "Ensenada",
                Street = "Calle 10",
                Postal = "22790",
                Apartment = "Suite 24"
            };
            #endregion test data

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            PaymentOptionsHomePage paymentsHomePage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addPaymentPage = paymentsHomePage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.Country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.Name);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.Street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.Apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.City);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.StateProvinceRegion);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.Postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.WaitForAppBusy();
            paymentsHomePage = addPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(500);

            List<string> items = paymentsHomePage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string expectedResult = $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}";
            string payment = items.FirstOrDefault(option => option.Contains(expectedResult));

            Assert.IsNotNull(payment, "Payment is not found");
        }
        #endregion add

        #region Edit
        //Test Case on test rail -> C1180, C1182
        //not passing modal dialog is not being found
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditPaymentMakeDefault()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                LastFourDigits = "1111",
                Cvv = "077",
                ExpirationMont = "12",
                ExpirationYear = "26",
                HolderName = "pika",
                CardNumber = "4111111111111111"
            };
            TestAddress testBillingAddress = new TestAddress
            {
                Country = "MX",
                Name = "SDET",
                StateProvinceRegion = "Estado de baja california",
                City = "Ensenada",
                Street = "Calle 3",
                Postal = "22790",
                Apartment = "Suite 24"
            };

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testUser.Username, testUser.Password);
            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();

            PaymentOptionsCreatePage addPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.Country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.Name);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.Street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.Apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.City);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.StateProvinceRegion);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.Postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.WaitForAppBusy();
            paymentsPage = addPaymentPage.CloseModal(ModalsEnum.Information);

            //begin the test
            paymentsPage.ClickOnPaymentOption(AccessLevel.User, testCardtoken);
            PaymentOptionsEditPage editPaymentsPage = paymentsPage.ContentModal.ClickOnEdit();
            bool makeDefaultCheckboxExist = editPaymentsPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            editPaymentsPage.ClickOnMakeDefault();
            bool isSubmitEnabled = editPaymentsPage.IsSubmitButtonEnabled();

            Assert.IsTrue(isSubmitEnabled, "The form cannot be submited");

            editPaymentsPage.ClickOnSubmit();
            editPaymentsPage.WaitForAppBusy();
            paymentsPage = editPaymentsPage.CloseModal(ModalsEnum.Information);

            List<string> items = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();

            string payment = items.FirstOrDefault(it => it.Contains(
                $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}"));

            bool defaultLabelInDropdownItem = paymentsPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);
            string actualDefaultPaymentData = paymentsPage.GetDefaultTilePaymentData(AccessLevel.User);

            Assert.IsTrue(defaultLabelInDropdownItem, "Default label does not exist");
            Assert.IsTrue(actualDefaultPaymentData.Contains(testCardtoken.LastFourDigits), "Card last four digits cannot be found");
            Assert.IsTrue(actualDefaultPaymentData.Contains($"{testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}"), "Expiration date does not correspond");
            Assert.IsNotNull(payment, "Payment is not found");
        }

        //TODO
        //intermitent fail .modal content
        //[TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.ReadyToGo)]
        public void EditInternationalAddress()
        {
            var testUser = DataFactory.Users.CreateTestUser();
            TestAddress testBillingAddress = new TestAddress
            {
                Country = "MX",
                Name = "SDET",
                StateProvinceRegion = "CO",
                City = "test city",
                Street = "elm street",
                Postal = "22770",
                Apartment = "Suite s4"
            };
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                LastFourDigits = "1111",
                Cvv = "077",
                ExpirationMont = "12",
                ExpirationYear = "26",
                HolderName = "pika",
                CardNumber = "4111111111111111"
            };
            TestAddress expectedBillingAddress = new TestAddress
            {
                Country = "MX",
                Name = "SDET",
                StateProvinceRegion = "Estado de baja california",
                City = "Ensenada",
                Street = "Calle 3",
                Postal = "22790",
                Apartment = "Suite 24"
            };

            TestAddress actualBillingAddressValues = new TestAddress();

            APIndexPage indexPage = new APIndexPage(Driver, Url);
            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Username, testUser.Password);
            PaymentOptionsHomePage paymentsHomePage = indexPage.Header.ClickOnPaymentOptions();

            //Add a new credit card
            PaymentOptionsCreatePage addPaymentPage = paymentsHomePage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.Country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.Name);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.Street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.Apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.City);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.StateProvinceRegion);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.Postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.WaitForAppBusy();
            paymentsHomePage = addPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //Begin testing here
            paymentsHomePage.ClickOnPaymentOption(AccessLevel.User, testCardtoken);

            PaymentOptionsEditPage editPaymentPage = paymentsHomePage.ContentModal.ClickOnEdit();
            editPaymentPage.WaitForAppBusy();
            editPaymentPage.ClickOnBillingAddressOption(BillingAddressOptions.NewOne);
            editPaymentPage.SetInputAddressValue(AddressInputs.Country, expectedBillingAddress.Country);
            editPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, expectedBillingAddress.Name);
            editPaymentPage.SetInputAddressValue(AddressInputs.Street, expectedBillingAddress.Street);
            editPaymentPage.SetInputAddressValue(AddressInputs.Apartment, expectedBillingAddress.Apartment);
            editPaymentPage.SetInputAddressValue(AddressInputs.City, expectedBillingAddress.City);
            editPaymentPage.SetInputAddressValue(AddressInputs.State, expectedBillingAddress.StateProvinceRegion);
            editPaymentPage.SetInputAddressValue(AddressInputs.Postal, expectedBillingAddress.Postal);

            editPaymentPage.ClickOnSubmit();
            editPaymentPage.WaitForAppBusy();
            paymentsHomePage = editPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //make sure the update has done
            paymentsHomePage.ClickOnPaymentOption(AccessLevel.User, testCardtoken);
            editPaymentPage = paymentsHomePage.ContentModal.ClickOnEdit();
            editPaymentPage.WaitForAppBusy();

            actualBillingAddressValues.Country = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Country);
            actualBillingAddressValues.Name = editPaymentPage.GetBillingAddressInputValue(AddressInputs.CompanyName);
            actualBillingAddressValues.Street = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Street);
            actualBillingAddressValues.Apartment = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Apartment);
            actualBillingAddressValues.City = editPaymentPage.GetBillingAddressInputValue(AddressInputs.City);
            actualBillingAddressValues.Postal = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Postal);
            actualBillingAddressValues.StateProvinceRegion = editPaymentPage.GetBillingAddressInputValue(AddressInputs.State);

            paymentsHomePage = editPaymentPage.ClickOnCancel();

            Assert.IsTrue(expectedBillingAddress.Name == actualBillingAddressValues.Name, $"Expected: '{expectedBillingAddress.Name}' Got: '{actualBillingAddressValues.Name}'");
            Assert.IsTrue(actualBillingAddressValues.Country.Contains(expectedBillingAddress.Country), $"Expected: '{expectedBillingAddress.Country}' Got: '{actualBillingAddressValues.Country}'");
            Assert.IsTrue(expectedBillingAddress.Street == actualBillingAddressValues.Street, $"Expected: '{expectedBillingAddress.Street}' Got: '{actualBillingAddressValues.Street}'");
            Assert.IsTrue(expectedBillingAddress.Apartment == actualBillingAddressValues.Apartment, $"Expected: '{expectedBillingAddress.Apartment}' Got: '{actualBillingAddressValues.Apartment}'");
            Assert.IsTrue(expectedBillingAddress.City == actualBillingAddressValues.City, $"Expected: '{expectedBillingAddress.City}' Got: '{actualBillingAddressValues.City}'");
            Assert.IsTrue(expectedBillingAddress.Postal == actualBillingAddressValues.Postal, $"Expected: '{expectedBillingAddress.Postal}' Got: '{actualBillingAddressValues.Postal}'");
            Assert.IsTrue(expectedBillingAddress.StateProvinceRegion == actualBillingAddressValues.StateProvinceRegion, $"Expected: '{expectedBillingAddress.StateProvinceRegion}' Got: '{actualBillingAddressValues.StateProvinceRegion}'");
        }
        #endregion edit
    }
}