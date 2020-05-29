using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.PageObjects.CartPOM.Enums;
using AllPoints.PageObjects.MyAccountPOM.Enums;
using System.Linq;
using AllPoints.TestDataModels;
using AllPoints.Constants;

namespace AllPoints.Features.Cart
{
    [TestClass]
    public class CartRefresh : FeatureBase
    {
        private CartDataFactory dataFactory;


        [ClassInitialize]
        public static void InitClassSuite(TestContext tstContext)
        {
            testContext = tstContext;
        }

        public string manufacturerOption = "";
        public string searchField = "";

        public CartRefresh()
        {
            dataFactory = new CartDataFactory();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        public void CarRefreshWhenAddedANON_T5504()
        {
            var indexPage = new IndexPage(driver, url);

            //select the third option on the dropdown by index
            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            //wait until busy animation is present
            catalogItemPage.WaitForAppBusy();

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        public void CarRefreshWhenRemovedANON_T5503()
        {
            var indexPage = new IndexPage(driver, url);

            //New method for Manufacturer dropdown
            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();
            catalogItemPage.AddtoCartbuttonInCatalog();

            //wait until busy animation is done
            catalogItemPage.WaitForAppBusy();

            CartPage cartMainPage = catalogItemPage.Header.ClickOnViewCart();

            Assert.IsTrue(cartMainPage.HeadsUpCartInfo(), "Totals are not displayed");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        public void CarRefreshSFLANON_T5502()
        {
            var indexPage = new IndexPage(driver, url);

            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            catalogItemPage.WaitForAppBusy();

            CartPage cartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Move items to Save For Later section
            cartMainPage.MovetoSFL();

            //TODO
            //wait until save for later section has rendered
            Thread.Sleep(5000);

            Assert.IsTrue(cartMainPage.HeadsUpCartInfo(), "Cart is not Empty");

            cartMainPage.SeeSFLItems();

            Assert.IsTrue(cartMainPage.MovetoCartLinkEnable(), "Save For Later Section is Empty");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        public void CarRefresh_ProductDetails()
        {
            var indexPage = new IndexPage(driver, url);

            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            catalogItemPage.WaitForAppBusy();

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //TODO
            //wait until cart loading is done
            //Thread.Sleep(5000);

            Assert.IsTrue(CartMainPage.ImageSectionInCart(), "Image is not Displayed");

            Assert.IsTrue(CartMainPage.TitleSectionIsDisplayed(), "Title is not Displayed");

            Assert.IsTrue(CartMainPage.SKUSectionIsDisplayed(), "SKU Section is not Displayed");

            Assert.IsTrue(CartMainPage.LineItemSectionIsDisplayed(), "Prices or Quantity are not Displayed");

            Assert.IsTrue(CartMainPage.AddToListLink(), "Add to list Link is not displayed");

        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]        
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\cart.csv", "cart#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\cart.csv")]
        public void ValidateMergeCart()
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

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            var offeringProductpage = catalogItemPage.ClickOnFirstItemInCatalog();

            offeringProductpage.AddtoCartInOffering();

            Thread.Sleep(2000);

            LoginPage loginPage = offeringProductpage.ClickOnSignInOffering();

            indexPage = loginPage.Login(testData.email, testData.password);

            Thread.Sleep(2000);

            offeringProductpage.SavingsMsgDisplayed();

            offeringProductpage.YourPriceLabelforAUTH();
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\checkout.csv", "checkout#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\checkout.csv")]
        public void AddAddressChecktoutoMyAccount()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                user = dataFactory.CreateLoginAccount(),
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

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            //checkoutPage.SelectAddressRadioButton(AddressSelectOptions.New);

            Thread.Sleep(5000);

            checkoutPage.SetAddressElement(AddressInputs.ATTN, testData.attn);

            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, testData.address);

            checkoutPage.SetAddressElement(AddressInputs.Apt, testData.apt);

            checkoutPage.SetAddressElement(AddressInputs.City, testData.city);

            checkoutPage.SetAddressElement(AddressInputs.State, testData.state);

            checkoutPage.SetAddressElement(AddressInputs.Postal, testData.zipCode);

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            //checkoutPage.SelectBillingRadioButton(BillingSelectOptions.Existing);
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

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            Thread.Sleep(5000);

            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            //checkoutPage.PlaceOrderSubmitClick();
            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");

            //CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            indexPage = orderpage.ContinueShoppingClick();

            //AddressesHomePage addresspage = indexPage.Header.ClickOnAddresses();

            //var dropdownItems = addresspage.GetAddressesDropdownItems(AccessLevel.User).ToList();

            ////just created address data
            //string createdAddress = string.IsNullOrEmpty(testData.apt) ?
            //    $"{testData.address}, {testData.city} {testData.countryShort} {testData.zipCode}"
            //    :
            //    $"{testData.address}, {testData.apt}, {testData.city} {testData.countryShort} {testData.zipCode}";

            ////search the address in the user level dropdown
            //string expectedAddress = dropdownItems.FirstOrDefault(x => x.Contains(createdAddress));

            //Assert.IsNotNull(expectedAddress, "Address is not found in dropdown");
        }

        //Script with a new user with Contact Info and Address
        [TestMethod]
        [TestCategory(TestCategoriesConstants.Smoke)]        
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\checkout.csv", "checkout#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("TestData\\checkout.csv")]
        public void E2ETestwithContactInfoandAddressUser()
        {
            var indexPage = new IndexPage(driver, url);

            var testData = new
            {
                //Make sure to change name of email
                user = dataFactory.CreateLoginAccount(), 
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

            indexPage = loginPage.Login(testData.user.Email, testData.user.Password);

            var manufacturesItems = indexPage.Header.GetManufacturerOptions();

            manufacturerOption = manufacturesItems.ElementAtOrDefault(2).webElement.Text;

            indexPage.Header.SelectManufacturer(manufacturerOption);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            CartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            CheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInAddressDropDown();

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

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInBillingDropDown();

            Assert.IsTrue(checkoutPage.BillingButtonIsEnable(), "Proceed to Review and Place Your Order button is not available");

            Thread.Sleep(5000);

            checkoutPage.BillingSubmitClick();

            Assert.IsTrue(checkoutPage.PlaceOrderButtonIsEnable(), "Place Your Order button is not available");

            Thread.Sleep(5000);

            OrderConfirmationPage orderpage = checkoutPage.PlaceOrderSubmitClick();

            Assert.IsTrue(checkoutPage.OrderConfirmationText(), "Order Confirmation text is not available");
        }

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        [TestCategory(TestCategoriesConstants.NoTestData)]
        public void CartEmptyHeadsUpMsg_ANON()
        {
            var indexPage = new IndexPage(driver, url);

            CartPage cartMainPage = indexPage.Header.ClickOnViewCart();

            cartMainPage.ContentHeadsUpInfo();   
        }        
    }
}