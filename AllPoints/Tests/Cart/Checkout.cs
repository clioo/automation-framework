using AllPoints.Constants;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.CartPOM.Enums;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AllPoints.Features.Cart
{
    [TestClass]
    public class Checkout : FeatureBase
    {
        private CartDataFactory dataFactory;

        [ClassInitialize]
        public static void InitClassSuite(TestContext tstContext)
        {
            testContext = tstContext;
        }

        public Checkout()
        {
            dataFactory = new CartDataFactory();
        }

        public string manufacturerOption = "";
        public string searchField = "";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\checkout.csv", "checkout#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\checkout.csv")]
        public void CheckoutPageANON_T6208()
        {
            bool isAppBusy;

            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                pwd = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            //Redirected to Login Page
            Assert.IsNull(checkoutPage);

            //Click on Proceed as Guest
            LoginPage loginPage = new LoginPage(driver);
            checkoutPage = loginPage.clickOnGuestbutton();

            //Wait for loading checkout
            Thread.Sleep(3000);

            checkoutPage.SetContactElement(ContactInputs.FirstName, testData.firstname);

            checkoutPage.SetContactElement(ContactInputs.LastName, testData.lastname);

            checkoutPage.SetContactElement(ContactInputs.PhoneNumber, testData.phonenumber);

            checkoutPage.SetContactElement(ContactInputs.Email, testData.email);

            Assert.IsTrue(checkoutPage.ContactButtonIsEnable(), "Proceed to Shipping Information button is not available");

            checkoutPage.ContactSubmitClick();

            isAppBusy = checkoutPage.IsAppBusy();

            Assert.IsFalse(isAppBusy);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, testData.attn);

            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, testData.address);

            checkoutPage.SetAddressElement(AddressInputs.Apt, testData.apt);

            checkoutPage.SetAddressElement(AddressInputs.City, testData.city);

            checkoutPage.SetAddressElement(AddressInputs.State, testData.state);

            checkoutPage.SetAddressElement(AddressInputs.Postal, testData.zipCode);

            Assert.IsTrue(checkoutPage.ShippingButtonIsEnable(), "Proceed to Secure Billing button is not available");

            checkoutPage.ShippingSubmitClick();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\checkout.csv", "checkout#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\checkout.csv")]
        public void GoogleTypeAheadAUTH_T6208()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                email = (string)testContext.DataRow["Email"],
                password = testContext.DataRow["Password"].ToString(),
                country = (string)testContext.DataRow["Country"],
                countryShort = (string)testContext.DataRow["CountryShort"],
                address = (string)testContext.DataRow["StreetAddress"],
                state = (string)testContext.DataRow["State"],
                city = (string)testContext.DataRow["City"],
                zipCode = testContext.DataRow["ZipCode"].ToString(),
                apt = testContext.DataRow["Apt"].ToString(),
                firstname = (string)testContext.DataRow["FirstName"],
                lastname = (string)testContext.DataRow["LastName"],
                company = (string)testContext.DataRow["Company"],
                phonenumber = testContext.DataRow["PhoneNumber"].ToString(),
                attn = testContext.DataRow["ATTN"].ToString(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.email, testData.password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.New);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, testData.attn);

            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, testData.address);

            checkoutPage.SetAddressElement(AddressInputs.Apt, testData.apt);

            checkoutPage.SetAddressElement(AddressInputs.City, testData.city);

            checkoutPage.SetAddressElement(AddressInputs.State, testData.state);

            checkoutPage.SetAddressElement(AddressInputs.Postal, testData.zipCode);

            Assert.IsTrue(checkoutPage.ShippingButtonIsEnable(), "Proceed to Secure Billing button is not available");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void BillingInfoAUTH_T6208()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                user = dataFactory.CreateLoginAccount(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(3000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            //checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ClickShippingButton();

            checkoutPage.SelectBillingRadioButton(BillingSelectOptions.Existing);

            //checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            checkoutPage.BillingSubmitClick();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        public void BillingInfoNewCCAUTH_T6208()
        {
            var indexPage = new IndexPage(driver,url);

            var testData = new
            {
                user = dataFactory.CreateLoginAccount(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            Thread.Sleep(5000);

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            //checkoutPage.SelectBillingRadioButton(BillingSelectOptions.New);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077"
            };

            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, cardToken.HolderName);
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, cardToken.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, cardToken.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, cardToken.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CVV, cardToken.Cvv);


            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            //CheckoutPage.ReviewPlaceOrderbuttonclick();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\placeorder.csv", "placeorder#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\placeorder.csv")]
        public void ReviewPlaceOrderAUTH_T6208()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                user = dataFactory.CreateLoginAccount(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click on Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077"
            };

            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, cardToken.HolderName);
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, cardToken.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, cardToken.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, cardToken.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CVV, cardToken.Cvv);

            checkoutPage.BillingSubmitClick();

            Thread.Sleep(5000);

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            checkoutPage.PlaceOrderSubmitClick();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CSVs\\placeorder.csv", "placeorder#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("CSVs\\placeorder.csv")]
        public void OrderConfirmationAUTH_()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                user = dataFactory.CreateLoginAccount(),
            };

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(5000);

            //Click on Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4111111111111111",
                ExpirationMont = "12",
                ExpirationYear = "24",
                HolderName = "Test corp",
                Cvv = "077"
            };

            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, cardToken.HolderName);
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, cardToken.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, cardToken.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, cardToken.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CVV, cardToken.Cvv);

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            checkoutPage.BillingSubmitClick();

            Thread.Sleep(2000);

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(2000);

            checkoutPage.PlaceOrderSubmitClick();

            Thread.Sleep(2000);

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");

            Thread.Sleep(2000);

            Assert.IsTrue(checkoutPage.SuccessText(), "Success text is not available");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateAvailabilityIsDisplay()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTag = checkoutPage.AvailabiltyTagGet();
            Assert.IsTrue(availabiltyItemsTag.Count() > 0);
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_InStock()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);

            IDictionary<string, string> availabiltyItemsTag = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.InStock)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_InStock_AddressSelect()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTagPre = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTagPre.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.InStock)));

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ShippingSubmitClick();

            IDictionary<string, string> availabiltyItemsTagPos = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTagPos.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.InStock)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_Limited()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTag = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.Limited)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_Limited_AddressSelect()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTagPre = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTagPre.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.Limited)));

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ShippingSubmitClick();
            IDictionary<string, string> availabiltyItemsTagPos = checkoutPage.AvailabiltyTagGet();

            Assert.IsNotNull(availabiltyItemsTagPos.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.Limited)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_OutOfStock()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTag = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTag.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.OutOfStockGeneral)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void Availability_OutOfStock_AddressSelect()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("STKtest4@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(2000);

            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(2000);
            IDictionary<string, string> availabiltyItemsTagPre = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTagPre.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.OutOfStockGeneral)));

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ShippingSubmitClick();

            IDictionary<string, string> availabiltyItemsTagPos = checkoutPage.AvailabiltyTagGet();
            Assert.IsNotNull(availabiltyItemsTagPos.FirstOrDefault(t => t.Value.Contains(AvailabiltyConstants.OutOfStockGeneral)));
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateAvailability_Cart_Equals_Checkout()
        {
            IndexPage indexPage = new IndexPage(driver, url);

            LoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login("test@etundra.com", "1234");

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Thread.Sleep(5000);

            IDictionary<string, string> cartAvailabiltyItemsTag = CartMainPage.AvailabiltyTagGet();
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);
            IDictionary<string, string> checkoutAvailabiltyItemsTag = checkoutPage.AvailabiltyTagGet();
            foreach (var tag in checkoutAvailabiltyItemsTag)
            {
                Assert.AreEqual(cartAvailabiltyItemsTag[tag.Key], tag.Value);
            }
        }
    }
}