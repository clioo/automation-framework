using AllPoints.Constants;
using AllPoints.Features.Models;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using AllPoints.Tests.MyAccount.PaymentOptions;
using AllPointsPOM.PageObjects.MyAccountPOM.Enums;
using AllPointsPOM.PageObjects.MyAccountPOM.PaymentOptionsPOM.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AllPoints.Features.MyAccount.PaymentOptions
{
    [TestClass]
    public class PaymentsTests : FeatureBase
    {
        private PaymentsDataFactory dataFactory;
        private PaymentsDataFactoryV2 TestDataFactory;

        #region constructor
        public PaymentsTests()
        {
            TestDataFactory = new PaymentsDataFactoryV2();
            dataFactory = new PaymentsDataFactory();
        }
        #endregion

        #region View
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AccountLevelSectionIsDisplayed()
        {
            var testUser = TestDataFactory.CreateLoginAccount();

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);

            Assert.IsNotNull(loginPage, "login failed");

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            bool accountLevelExist = viewPaymentsPage.AccountLevelExist();

            Assert.IsTrue(accountLevelExist, "Account section is not displayed");

            viewPaymentsPage.Header.ClickOnSignOut();
        }

        //Test Case on test rail -> C1101, C1121
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AccountSectionNotDisplayed()
        {
            var testUser = TestDataFactory.UserWithNoTermsAsPaymentOption();
            string term = "40 Net Days, cool Stuff";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);

            Assert.IsNotNull(indexPage, "Login failed");

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();
            bool accountLevelExist = viewPaymentsPage.AccountLevelExist();

            Assert.IsFalse(accountLevelExist, "Account section is not hidden as expected");

            //check for user: cannot have terms
            var paymentsInDropdown = viewPaymentsPage.GetPaymentsDropdownItems(AccessLevel.User);
            string termInDropdown = paymentsInDropdown.FirstOrDefault(item => item.Equals(term));

            Assert.IsNull(termInDropdown, "User level cannot have terms");

            viewPaymentsPage.Header.ClickOnSignOut();
        }

        //pending to rework
        //Test Case on test rail -> C1119, C1120 C1122, C1123
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        //[TestCategory(TestCategoriesConstants.Smoke)]
        public void SetAccountLevelPaymentAsDefault()
        {
            var testData = dataFactory.AccountCardTokenSetDefault();
            string paymentData = $"{testData.PaymentOption.LastFourDigits} {testData.PaymentOption.ExpirationMont}/{testData.PaymentOption.ExpirationYear}";

            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.Email, testData.Password);

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //void actions
            viewPaymentsPage.ClickOnPaymentOption(AccessLevel.Account, paymentData);

            viewPaymentsPage.ContentModal.ClickOnMakeDefault();

            viewPaymentsPage.InfoModal.ClickOnClose();

            string actualDefaultPaymentData = viewPaymentsPage.GetDefaultTilePaymentData(AccessLevel.Account);

            bool defaultLabelInDropdownItem = viewPaymentsPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.Account);

            //default tile contains the exp date
            Assert.IsTrue(actualDefaultPaymentData.Contains($"{testData.PaymentOption.ExpirationMont}/{testData.PaymentOption.ExpirationYear}"));
            //default tile contains the last four card numbers
            Assert.IsTrue(actualDefaultPaymentData.Contains($"{testData.PaymentOption.LastFourDigits}"));

            Assert.IsTrue(defaultLabelInDropdownItem);

            List<string> paymentsInDropdown = viewPaymentsPage.GetPaymentsDropdownItems(AccessLevel.Account).ToList();
            string firstPayment = paymentsInDropdown.ElementAt(0);

            //sort order check
            Assert.IsTrue(firstPayment.Contains(paymentData), "first payment is not the default one");

            //final steps (log off)
            viewPaymentsPage.Header.ClickOnSignOut();
        }

        //Test Case on test rail -> C1355
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void SetTermAsDefaultPayment()
        {
            var testUser = TestDataFactory.CreateLoginAccount();
            string termDetail = "40 Net Days";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);
            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //void actions
            viewPaymentsPage.ClickOnPaymentOption(AccessLevel.Account, termDetail);
            viewPaymentsPage.ContentModal.ClickOnMakeDefault();
            viewPaymentsPage.IsAppBusy();
            viewPaymentsPage.InfoModal.ClickOnClose();
            string actualDefaultPaymentData = viewPaymentsPage.GetDefaultTilePaymentData(AccessLevel.Account);

            Assert.IsTrue(actualDefaultPaymentData.Contains(termDetail));
        }

        //Test Case on test rail -> C1127, C1120
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void SetUserLevelPaymentAsDefault()
        {
            var testUser = TestDataFactory.CreateLoginAccount();
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            AddressModel testAddress = new AddressModel
            {
                country = "US",
                CompanyName = "QA Softtek Automation",
                state = "CO",
                city = "Denver",
                street = "Elm street",
                postal = "22780",
                apartment = "24"
            };
            //needs to be formated like that
            string stringTestPayment = $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}" + $@"
{testCardtoken.HolderName}";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);

            PaymentOptionsHomePage viewPaymentsPage = indexPage.Header.ClickOnPaymentOptions();

            //add payment process
            PaymentOptionsCreatePage addNewPaymentPage = viewPaymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testAddress.CompanyName);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, testAddress.street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testAddress.apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, testAddress.city);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, testAddress.state);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, testAddress.postal);
            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.IsAppBusy();
            viewPaymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //validations begins here
            viewPaymentsPage.ClickOnPaymentOption(AccessLevel.User, stringTestPayment);
            viewPaymentsPage.ContentModal.ClickOnMakeDefault();
            viewPaymentsPage.IsAppBusy();
            viewPaymentsPage.InfoModal.ClickOnClose();

            bool defaultLabelInDropdownItem = viewPaymentsPage.IsDefaultLabelPresentOnDropdownItem(AccessLevel.User);

            List<string> paymentsInDropdown = viewPaymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string firstPayment = paymentsInDropdown.ElementAt(0);
            string actualDefaultPaymentData = viewPaymentsPage.GetDefaultTilePaymentData(AccessLevel.User);

            Assert.IsTrue(defaultLabelInDropdownItem, "default fayment has not a label");
            //sort order check
            Assert.IsTrue(firstPayment.Contains(stringTestPayment), "first payment is not the default one");
            Assert.IsTrue(actualDefaultPaymentData.Contains(testCardtoken.LastFourDigits));
            Assert.IsTrue(actualDefaultPaymentData.Contains($"{testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}"));

            bool accountTileIsPresent = viewPaymentsPage.ElementExistOnPage(AccessLevel.Account, ViewPaymentsElements.DefaultTile);

            Assert.IsFalse(accountTileIsPresent, "Account default tile should not exist here o:");
        }

        //needs rework on test data generation
        //Test case on test rail -> C1392
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        public void MoreThan20CreditCardsOnUserLevelCase()
        {
            var testData = dataFactory.UserAccountManyItems();

            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.Email, testData.Password);

            PaymentOptionsHomePage paymentOptionsPage = indexPage.Header.ClickOnPaymentOptions();

            Assert.IsTrue(paymentOptionsPage.GetPaymentsDropdownItems(AccessLevel.User).Count() > 20, "dropdown does not have the expected items count");

            paymentOptionsPage.Header.ClickOnSignOut();
        }

        //needs rework on test data generation
        //Test Case on test rail -> C1102
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        public void MoreThan20CreditCardsOnAccountLevelCase()
        {
            var testData = dataFactory.UserAccountManyItems();

            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.Email, testData.Password);

            PaymentOptionsHomePage paymentOptionsPage = indexPage.Header.ClickOnPaymentOptions();

            Assert.IsTrue(paymentOptionsPage.GetPaymentsDropdownItems(AccessLevel.Account).Count() > 20, "dropdown does not have the expected items count");

            //validate that -Make default -Delete -Edit exists
            //paymentOptionsPage.ClickOnPaymentOption(AccessLevel.User, "");

            paymentOptionsPage.Header.ClickOnSignOut();
        }
        #endregion View

        #region delete
        //Test Case on test rail -> C1142
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void DeleteUserLevelPayment()
        {
            #region testData
            var testUser = TestDataFactory.CreateLoginAccount();
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                CardNumber = "5582508629687473",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077",
                LastFourDigits = "7473"
            };
            AddressModel testAddress = new AddressModel
            {
                country = "US",
                CompanyName = "QA Softtek Automation",
                state = "CO",
                city = "Denver",
                street = "Elm street",
                postal = "22780",
                apartment = "24"
            };
            #endregion testData

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);

            Assert.IsNotNull(indexPage, "Login failed");

            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.ClickOnBillingAddressOption(BillingAddressOptions.NewOne);

            //Fill billing address form
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testAddress.CompanyName);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, testAddress.street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testAddress.apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, testAddress.city);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, testAddress.state);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, testAddress.postal);

            bool makeDefaultCheckboxExist = addNewPaymentPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.IsAppBusy();
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
            paymentsPage.IsAppBusy();

            int paymentsCountAfter = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList().Count;

            Assert.IsTrue(paymentsCountAfter == (paymentsCountBefore - 1), "Payment has not deleted");
        }
        #endregion delete

        #region Add
        //Test Case on test rail -> C1134, C1135, C1179
        //[TestMethod]
        //[TestCategory(TestCategoriesConstants.Regression)]
        //[TestCategory(TestCategoriesConstants.Smoke)]
        public void AddCreditCardUsingPrevStoredAddress()
        {
            var testData = TestDataFactory.CreateLoginAccount();
            AddressModel testAddress = new AddressModel
            {
                country = "US",
                street = "street test",
                CompanyName = "address company name",
                state = "CO",
                city = "boulder",
                postal = "52809"
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

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testData.Email, testData.Password);

            //Manually add an address
            AddressesHomePage addressesPage = indexPage.Header.ClickOnAddresses();
            AddAddressPage createAddressPage = addressesPage.ClickOnAddNewAddress();

            //Fill address form
            createAddressPage.SetInputValue(AddressInputs.CompanyName, testAddress.CompanyName);
            createAddressPage.SetInputValue(AddressInputs.Street, testAddress.street);
            createAddressPage.SetInputValue(AddressInputs.Apartment, testAddress.apartment);
            createAddressPage.SetInputValue(AddressInputs.City, testAddress.city);
            createAddressPage.SetInputValue(AddressInputs.State, testAddress.state);
            createAddressPage.SetInputValue(AddressInputs.Postal, testAddress.postal);

            addressesPage = createAddressPage.ClickOnSubmit();
            addressesPage.IsAppBusy();
            //TODO: check the logic on address create process
            addressesPage.IsAppBusy();
            addressesPage.InformationModal.ClickOnClose();

            //test begins here
            PaymentOptionsHomePage paymentsPage = addressesPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();

            string storedAddress = string.IsNullOrEmpty(testAddress.apartment) ?
                $"{testAddress.street}, {testAddress.city} {testAddress.country} {testAddress.postal}"
                :
                $"{testAddress.street}, {testAddress.apartment}, {testAddress.city} {testAddress.country} {testAddress.postal}";

            addNewPaymentPage.FillCardTokenForm(testCardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.Stored);
            addNewPaymentPage.SelectPreviouslyStoreAddress(storedAddress);
            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.IsAppBusy();
            paymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            addNewPaymentPage.IsAppBusy();
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
            var testData = TestDataFactory.CreateLoginAccount();

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
            var address = new AddressModel
            {
                country = "US",
                CompanyName = "QA Softtek Automation",
                state = "CO",
                city = "Denver",
                street = "Elm street",
                postal = "22780",
                apartment = "24"
            };
            #endregion

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testData.Email, testData.Password);

            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addNewPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addNewPaymentPage.FillCardTokenForm(cardtoken);
            addNewPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);

            //Fill billing address form
            addNewPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, address.CompanyName);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Street, address.street);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Apartment, address.apartment);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.City, address.city);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.State, address.state);
            addNewPaymentPage.SetInputAddressValue(AddressInputs.Postal, address.postal);

            bool makeDefaultCheckboxExist = addNewPaymentPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            addNewPaymentPage.ClickOnSubmit();
            addNewPaymentPage.IsAppBusy();
            paymentsPage = addNewPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            List<string> items = paymentsPage.GetPaymentsDropdownItems(AccessLevel.User).ToList();
            string payment = items.FirstOrDefault(it => it.Contains($"{cardtoken.LastFourDigits} {cardtoken.ExpirationMont}/{cardtoken.ExpirationYear}"));

            Assert.IsNotNull(payment, "Payment is not found");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void AddPaymentOptionIntlAddress()
        {
            var testUser = TestDataFactory.CreateLoginAccount();

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
            var testBillingAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "SDET",
                state = "Estado de baja california",
                city = "Ensenada",
                street = "Calle 10",
                postal = "22790",
                apartment = "Suite 24"
            };
            #endregion test data

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);
            PaymentOptionsHomePage paymentsHomePage = indexPage.Header.ClickOnPaymentOptions();
            PaymentOptionsCreatePage addPaymentPage = paymentsHomePage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.CompanyName);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.city);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.state);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.IsAppBusy();
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
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditPaymentMakeDefault()
        {
            var testUser = TestDataFactory.CreateLoginAccount();
            PaymentOptionModel testCardtoken = new PaymentOptionModel
            {
                LastFourDigits = "1111",
                Cvv = "077",
                ExpirationMont = "12",
                ExpirationYear = "26",
                HolderName = "pika",
                CardNumber = "4111111111111111"
            };
            AddressModel testBillingAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "SDET",
                state = "Estado de baja california",
                city = "Ensenada",
                street = "Calle 3",
                postal = "22790",
                apartment = "Suite 24"
            };
            string selectPayment = $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}";

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login(testUser.Email, testUser.Password);
            PaymentOptionsHomePage paymentsPage = indexPage.Header.ClickOnPaymentOptions();

            PaymentOptionsCreatePage addPaymentPage = paymentsPage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.CompanyName);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.city);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.state);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.IsAppBusy();
            paymentsPage = addPaymentPage.CloseModal(ModalsEnum.Information);

            //begin the test
            paymentsPage.ClickOnPaymentOption(AccessLevel.User, selectPayment);
            PaymentOptionsEditPage editPaymentsPage = paymentsPage.ContentModal.ClickOnEdit();
            bool makeDefaultCheckboxExist = editPaymentsPage.ElementOnPageIsPresent(AddPaymentElements.DefaultCheckbox);

            Assert.IsTrue(makeDefaultCheckboxExist, "Default checkbox does not exist");

            editPaymentsPage.ClickOnMakeDefault();
            bool isSubmitEnabled = editPaymentsPage.IsSubmitButtonEnabled();

            Assert.IsTrue(isSubmitEnabled, "The form cannot be submited");

            editPaymentsPage.ClickOnSubmit();
            editPaymentsPage.IsAppBusy();
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

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void EditInternationalAddress()
        {
            var testUser = TestDataFactory.CreateLoginAccount();
            AddressModel testBillingAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "SDET",
                state = "CO",
                city = "test city",
                street = "elm street",
                postal = "22770",
                apartment = "Suite s4"
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
            AddressModel expectedBillingAddress = new AddressModel
            {
                country = "MX",
                CompanyName = "SDET",
                state = "Estado de baja california",
                city = "Ensenada",
                street = "Calle 3",
                postal = "22790",
                apartment = "Suite 24"
            };
            string currentPaymentData = $"{testCardtoken.LastFourDigits} {testCardtoken.ExpirationMont}/{testCardtoken.ExpirationYear}";
            AddressModel actualBillingAddressValues = new AddressModel();

            IndexPage indexPage = new IndexPage(driver, url);
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            indexPage = loginPage.Login(testUser.Email, testUser.Password);
            PaymentOptionsHomePage paymentsHomePage = indexPage.Header.ClickOnPaymentOptions();

            //Add a new credit card
            PaymentOptionsCreatePage addPaymentPage = paymentsHomePage.ClickOnAddNewCreditCard();
            addPaymentPage.FillCardTokenForm(testCardtoken);
            addPaymentPage.SelectBillingAddressOption(BillingAddressOptionsEnum.AddNew);
            addPaymentPage.SetInputAddressValue(AddressInputs.Country, testBillingAddress.country);
            addPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, testBillingAddress.CompanyName);
            addPaymentPage.SetInputAddressValue(AddressInputs.Street, testBillingAddress.street);
            addPaymentPage.SetInputAddressValue(AddressInputs.Apartment, testBillingAddress.apartment);
            addPaymentPage.SetInputAddressValue(AddressInputs.City, testBillingAddress.city);
            addPaymentPage.SetInputAddressValue(AddressInputs.State, testBillingAddress.state);
            addPaymentPage.SetInputAddressValue(AddressInputs.Postal, testBillingAddress.postal);

            addPaymentPage.ClickOnSubmit();
            addPaymentPage.IsAppBusy();
            paymentsHomePage = addPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //Begin testing here
            paymentsHomePage.ClickOnPaymentOption(AccessLevel.User, currentPaymentData);

            PaymentOptionsEditPage editPaymentPage = paymentsHomePage.ContentModal.ClickOnEdit();
            editPaymentPage.IsAppBusy();
            editPaymentPage.ClickOnBillingAddressOption(BillingAddressOptions.NewOne);
            editPaymentPage.SetInputAddressValue(AddressInputs.Country, expectedBillingAddress.country);
            editPaymentPage.SetInputAddressValue(AddressInputs.CompanyName, expectedBillingAddress.CompanyName);
            editPaymentPage.SetInputAddressValue(AddressInputs.Street, expectedBillingAddress.street);
            editPaymentPage.SetInputAddressValue(AddressInputs.Apartment, expectedBillingAddress.apartment);
            editPaymentPage.SetInputAddressValue(AddressInputs.City, expectedBillingAddress.city);
            editPaymentPage.SetInputAddressValue(AddressInputs.State, expectedBillingAddress.state);
            editPaymentPage.SetInputAddressValue(AddressInputs.Postal, expectedBillingAddress.postal);

            editPaymentPage.ClickOnSubmit();
            editPaymentPage.IsAppBusy();
            paymentsHomePage = editPaymentPage.CloseModal(ModalsEnum.Information);
            Thread.Sleep(1000);

            //make sure the update has done
            paymentsHomePage.ClickOnPaymentOption(AccessLevel.User, currentPaymentData);
            editPaymentPage = paymentsHomePage.ContentModal.ClickOnEdit();
            editPaymentPage.IsAppBusy();

            actualBillingAddressValues.country = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Country);
            actualBillingAddressValues.CompanyName = editPaymentPage.GetBillingAddressInputValue(AddressInputs.CompanyName);
            actualBillingAddressValues.street = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Street);
            actualBillingAddressValues.apartment = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Apartment);
            actualBillingAddressValues.city = editPaymentPage.GetBillingAddressInputValue(AddressInputs.City);
            actualBillingAddressValues.postal = editPaymentPage.GetBillingAddressInputValue(AddressInputs.Postal);
            actualBillingAddressValues.state = editPaymentPage.GetBillingAddressInputValue(AddressInputs.State);

            paymentsHomePage = editPaymentPage.ClickOnCancel();

            Assert.IsTrue(expectedBillingAddress.CompanyName == actualBillingAddressValues.CompanyName, $"Expected: '{expectedBillingAddress.CompanyName}' Got: '{actualBillingAddressValues.CompanyName}'");
            Assert.IsTrue(actualBillingAddressValues.country.Contains(expectedBillingAddress.country), $"Expected: '{expectedBillingAddress.country}' Got: '{actualBillingAddressValues.country}'");
            Assert.IsTrue(expectedBillingAddress.street == actualBillingAddressValues.street, $"Expected: '{expectedBillingAddress.street}' Got: '{actualBillingAddressValues.street}'");
            Assert.IsTrue(expectedBillingAddress.apartment == actualBillingAddressValues.apartment, $"Expected: '{expectedBillingAddress.apartment}' Got: '{actualBillingAddressValues.apartment}'");
            Assert.IsTrue(expectedBillingAddress.city == actualBillingAddressValues.city, $"Expected: '{expectedBillingAddress.city}' Got: '{actualBillingAddressValues.city}'");
            Assert.IsTrue(expectedBillingAddress.postal == actualBillingAddressValues.postal, $"Expected: '{expectedBillingAddress.postal}' Got: '{actualBillingAddressValues.postal}'");
            Assert.IsTrue(expectedBillingAddress.state == actualBillingAddressValues.state, $"Expected: '{expectedBillingAddress.state}' Got: '{actualBillingAddressValues.state}'");
        }
        #endregion edit
    }
}