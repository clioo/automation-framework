using AllPoints.AllPoints;
using AllPoints.Constants;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using CommonHelper.Pages.CartPage.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace AllPoints.Features.OrderConfirm
{
    [TestClass]
    [TestCategory(TestCategoriesConstants.AllPoints)]
    public class OrderConfirmationTest : AllPointsBaseTest
    {
        public string manufacturerOption = "";
        public string searchField = "";

        [TestMethod]
        [TestCategory(TestCategoriesConstants.Regression)]
        public void ValidateAllElementsInConfirmationPage()
        {
            APIndexPage indexPage = new APIndexPage(Driver, Url);

            var user = DataFactory.Users.CreateTestUser();

            APLoginPage loginPage = indexPage.Header.ClickOnSignIn();

            indexPage = loginPage.Login(user.Username, user.Password);

            indexPage.Header.SelectManufacturer(2);

            CatalogItemsPage catalogItemPage = indexPage.Header.ClickOnSearchButton();

            catalogItemPage.AddtoCartbuttonInCatalog();

            Thread.Sleep(5000);

            APCartPage CartMainPage = catalogItemPage.Header.ClickOnViewCart();

            //Waiting For Loading Cart
            Thread.Sleep(7000);

            //Click no Proceed to checkout button
            APCheckoutPage checkoutPage = CartMainPage.ProceedToCheckOut();

            Thread.Sleep(5000);

            checkoutPage.SelectAddressRadioButton(AddressSelectOptions.Existing);

            Thread.Sleep(5000);

            checkoutPage.SelectFirstInAddressDropDown();

            checkoutPage.ClickShippingButton();

            Thread.Sleep(5000);

            PaymentOptionModel cardToken = new PaymentOptionModel
            {
                CardNumber = "4596502148863902",
                ExpirationMont = "03",
                ExpirationYear = "20",
                HolderName = "Dixie Green",
                Cvv = "775"
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

            Assert.IsTrue(orderpage.SuccessMsgIsPresent(), "Success Alert Message is not Displayed");

            Assert.IsTrue(orderpage.CustomerInfoIsDisplayed(), "Customer Information is not displayed");

            Assert.IsTrue(orderpage.ShippingInfoIsDisplayed(), "Shipping Information is not displayed");

            Assert.IsTrue(orderpage.BillingInfoIsDisplayed(), "Billing Information is not displayed");

            Assert.IsTrue(orderpage.listOfItemsDisplayed(), "List Item View is not Displaying Items");


        }
    }
}
