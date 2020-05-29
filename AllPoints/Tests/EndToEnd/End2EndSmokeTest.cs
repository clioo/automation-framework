using AllPoints.Features;
using AllPoints.Features.Models;
using AllPoints.Models;
using AllPoints.PageObjects.CartPOM;
using AllPoints.PageObjects.CartPOM.Enums;
using AllPoints.PageObjects.ListPOM.HomePagePOM;
using AllPoints.PageObjects.ListPOM.ListSummaryPOM;
using AllPoints.PageObjects.MyAccountPOM;
using AllPoints.PageObjects.MyAccountPOM.OrdersPOM;
using AllPoints.PageObjects.OderConfirmPOM;
using AllPoints.PageObjects.OfferingPOM;
using AllPoints.Pages;
using AllPoints.TestDataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllPoints.Tests.EndToEnd
{
    [TestClass]
    public class End2EndSmokeTest : FeatureBase
    {
        [TestMethod]
        [TestCategory("Smoke")]
        public void E2E01()
        {
            //Search & Price
            IndexPage indexPage = new IndexPage(driver, url);
            indexPage.Header.SetSearchFieldText("103-1035");
            CatalogItemsPage catalogItemsPage = indexPage.Header.ClickOnSearchButton();

            //Add product to the cart from the result page
            Assert.IsTrue(catalogItemsPage.IsListPriceDisplayed(), "There is no numeric price shown.");
            int quantityBeforeAdd = catalogItemsPage.Header.GetCartQuantity();
            catalogItemsPage.AddToCartElementByIndex(0);
            int quantityAfterAdd = catalogItemsPage.Header.GetCartQuantity();
            Assert.IsTrue(quantityAfterAdd > quantityBeforeAdd, "The item quantity did no increment");

            //Click on the product to go to the Product Detail Page
            OfferingProductsPage offeringProductsPage = catalogItemsPage.ClickOnFirstItemInCatalog();
            Assert.IsTrue(offeringProductsPage.ListPriceLabelforANON(), "List price tag not shown.");
            Assert.IsTrue(offeringProductsPage.NumericPriceDisplayed(), "No numeric price diplayed.");
            offeringProductsPage.UpdateAmountQuantity();
            int amountQuantityBeforeAdd = offeringProductsPage.GetAmountQuantity();

            //Add product to the cart from Product Detail Page and click on cart
            offeringProductsPage.AddToCartFirstProduct();
            offeringProductsPage.UpdateAmountQuantity();
            int amountQuantityAfterAdd = offeringProductsPage.GetAmountQuantity();
            Assert.IsTrue(offeringProductsPage.AumontQuantityIncremented(amountQuantityBeforeAdd, amountQuantityAfterAdd),
                "Quantity did not incremented.");
            CartPage cartPage = offeringProductsPage.ClickOnCart();
            Assert.AreEqual(1, cartPage.NumberOfItemsInCart(), "Should be just one product in cart.");
            Assert.AreEqual(2, cartPage.GetQuantityInput(), "Quantity must be 2.");

            //Checkout
            cartPage.ProceedToCheckOut();
            CheckoutPage checkoutPage = cartPage.CheckoutAsGuest();
            ContactInfoModel contactInfoModel =  new ContactInfoModel();
            contactInfoModel.FirstName = "Jesus Carlos";
            contactInfoModel.LastName = "Acosta Rocha";
            contactInfoModel.Email = "jesus_acosta1996@hotmaiil.com";
            contactInfoModel.Company = "Softtek";
            contactInfoModel.PhoneNumber = "6681596072";
            checkoutPage.SetContactElement(ContactInputs.FirstName, contactInfoModel.FirstName);
            checkoutPage.SetContactElement(ContactInputs.LastName, contactInfoModel.LastName);
            checkoutPage.SetContactElement(ContactInputs.Email, contactInfoModel.Email);
            checkoutPage.SetContactElement(ContactInputs.Company, contactInfoModel.Company);
            checkoutPage.SetContactElement(ContactInputs.PhoneNumber, contactInfoModel.PhoneNumber);
            checkoutPage.NextStep();
            AddressModel addressModel = GetAddressModel();
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, addressModel.street);
            checkoutPage.SetAddressElement(AddressInputs.City, addressModel.city);
            checkoutPage.SetAddressElement(AddressInputs.State, addressModel.state);
            checkoutPage.SetAddressElement(AddressInputs.Postal, addressModel.postal);
            checkoutPage.NextStep();
            SetCreditCard(checkoutPage);
            checkoutPage.NextStep();

            //Confirmation page and continue shopping
            OrderConfirmationPage orderConfirmationPage = checkoutPage.PlaceOrderSubmitClick();
            orderConfirmationPage.ClickOnContinueShoppingButton(ContinueShoppingButtons.PrintOrderConfirmation);
            Assert.AreEqual(2, driver.WindowHandles.Count, "Printable Order Confirmation Not Shown.");
            driver.SwitchTo().DefaultContent();
            orderConfirmationPage.ClickOnContinueShoppingButton(ContinueShoppingButtons.ContinueShopping);

        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void E2E02()
        {
            IndexPage indexPage = new IndexPage(driver, url);
            Login(indexPage);

            //Click on a Top Level Category (e.g.Hardware & Supplies)
            CatalogItemsPage catalogItemsPage = indexPage.Header.ClickOnCategory("Hardware & Supplies");
            Assert.IsTrue(catalogItemsPage.CheckIfYourPriceIsDisplayed(), "Your price is not diplayed");

            //Product list views
            catalogItemsPage.AddToCartElementByIndex(0);
            catalogItemsPage.ClickOnListViewIcon();
            catalogItemsPage.AddToCartElementByIndex(2);
            Assert.IsTrue(catalogItemsPage.ListViewDisplayed(), "The product view was not updated to List view(stack view).");

            //Page Navigation
            catalogItemsPage = catalogItemsPage.ClickOnNextPage();
            Assert.AreEqual("2", catalogItemsPage.GetActualPageListNumber(), "The page list number must be '2'.");
            catalogItemsPage.AddToCartElementByIndex(0);
            catalogItemsPage = catalogItemsPage.ClickOnLastPage();
            Assert.AreEqual(catalogItemsPage.GetLastPageListNumber(), catalogItemsPage.GetActualPageListNumber(), "Last page must be diplayed.");
            catalogItemsPage.AddToCartElementByIndex(0);

            //Category Navigation
            catalogItemsPage = catalogItemsPage.ClickOnSubCategory("Buzzers");
            catalogItemsPage.AddToCartFirstItemInCatalog();

            //Cart & Save for Later
            CartPage cartPage = catalogItemsPage.Header.ClickOnViewCart();
            Assert.AreEqual(5, cartPage.GetNumberOfItemsInCart(), "Must be 5 items in cart");
            Assert.IsTrue(cartPage.TotalAmountIsCorrect(), "The total amount doesn't reflect the sum of the items total.");
            cartPage.SelectAllItems();
            Assert.AreEqual(cartPage.GetNumberOfItemsInCart(), cartPage.GetNumberOfSelectedItems(), "Not all items were selected.");
            cartPage.MoveSelectedToSaveLater();
            Assert.AreEqual(0, cartPage.GetNumberOfItemsInCart(), "All items must be moved to 'Save for later' tab");
            cartPage.ClickOnSavedForLater();
            cartPage.SelectItemsByIndex(0);
            cartPage.SelectItemsByIndex(1);
            cartPage.SelectItemsByIndex(2);
            cartPage.MoveSelectedToCart();
            cartPage.ClickIndividualMoveToCartByIndex(1);
            cartPage.ClickOnCartTab();
            Assert.AreEqual(4, cartPage.GetNumberOfItemsInCart(), "Number of items in cart must be 4.");
            Assert.IsTrue(cartPage.InventoryAvailabilityIsDisplayed(), "Inventory availability must be displayed.");
            cartPage.SelectItemsByIndex(0);
            cartPage.SelectItemsByIndex(1);
            cartPage.ClickRemoveSelectedItems();
            cartPage.RemoveIndividualItemByIndex(0);
            Assert.AreEqual(1, cartPage.GetNumberOfItemsInCart(), "Number of items in cart must be 1.");

            //Checkout and continue shopping
            CheckoutPage checkoutPage = cartPage.ProceedToCheckOut();
            checkoutPage.UserInfoIsPopulated();
            AddressModel addressModel = GetAddressModel();
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, addressModel.street);
            checkoutPage.SetAddressElement(AddressInputs.City, addressModel.city);
            checkoutPage.SetAddressElement(AddressInputs.State, addressModel.state);
            checkoutPage.SetAddressElement(AddressInputs.Postal, addressModel.postal);
            checkoutPage.SetAddressElement(AddressInputs.ATTN, "Tundra Restaurant Supply");
            checkoutPage.NextStep();
            OrderConfirmationPage orderConfirmationPage = checkoutPage.PlaceOrderSubmitClick();
            orderConfirmationPage.ClickOnContinueShoppingButton(ContinueShoppingButtons.ContinueShopping);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void E2E03()
        {
            //Content
            IndexPage indexPage = new IndexPage(driver, url);
            Login(indexPage);

            //Shopping lists
            OfferingProductsPage offeringProductsPage = indexPage.ClickOnProductSpotlightDealsByIndex(0);
            offeringProductsPage.ClickOnAddToList();
            Assert.AreEqual(1, offeringProductsPage.AddListModal.GetOptionsFromChooseListModal().Count,
                "There must be just 1 option.");
            Assert.AreEqual("My List", offeringProductsPage.AddListModal.GetOptionsFromChooseListModal()[0].webElement.Text,
                "The option must be 'My List'");
            offeringProductsPage.AddListModal.CloseAddToListModal();
            offeringProductsPage.ClickOnAddToList();
            offeringProductsPage.AddListModal.ClickOnAddToListModal();
            offeringProductsPage.AddListModal.IsSuccessMessageInModal();
            offeringProductsPage.AddListModal.CloseAddToListModal();

            //Shopping lists
            offeringProductsPage.ChangeQuantity(2);
            offeringProductsPage.UpdateAmountQuantity();
            int amountQuantityBeforeAdd = offeringProductsPage.GetAmountQuantity();
            offeringProductsPage.AddToCartFirstProduct();
            offeringProductsPage.UpdateAmountQuantity();
            int amountQuantityAfterAdd = offeringProductsPage.GetAmountQuantity();
            Assert.IsTrue(offeringProductsPage.AumontQuantityIncremented(amountQuantityBeforeAdd, amountQuantityAfterAdd),
                "Quantity did not increment.");
            offeringProductsPage.ScrollToTop();
            offeringProductsPage.WaitForAppBusy(3);
            offeringProductsPage.Header.SetSearchFieldText("Amana");
            CatalogItemsPage catalogItemsPage = offeringProductsPage.Header.ClickOnSearchButton();
            catalogItemsPage.AddToListByIndex(0);
            catalogItemsPage.AddToListByIndex(1);
            catalogItemsPage.AddToListByIndex(2);
            ListSummaryPage listSummaryPage = catalogItemsPage.AddToListByIndex(3, visitList: true);
            Assert.AreEqual(5, listSummaryPage.NumberOfItemsInList(), "Quantity must be 5.");
            listSummaryPage.SelectByIndex(0);
            listSummaryPage.SelectByIndex(1);
            listSummaryPage.ClickOnRemoveSelected();
            Assert.AreEqual(3, listSummaryPage.NumberOfItemsInList(), "Quantity must be 3.");
            listSummaryPage.RemoveIndividualByIndex(0);
            Assert.AreEqual(2, listSummaryPage.NumberOfItemsInList(), "Quantity must be 2.");
            // if different in cart is true, it clicks on a different index
            listSummaryPage.ClickOnAddToCartByIndex(1, differentInCart:true);
            //*******************
            ListHomePage listHomePage = listSummaryPage.ClickOnBreadCrumbLists();
            listHomePage.ClickCreateaNewList();
            listHomePage.SendListName("new list");
            listHomePage.ClickCreateListButton();
            Assert.IsTrue(listHomePage.SuccessListCreated(), "List was not created");
            listHomePage.CloseModal();
            catalogItemsPage = listHomePage.Header.ClickOnCategory("Hardware & Supplies");
            listSummaryPage = catalogItemsPage.AddToListByIndex(0, visitList: true, list: "new list");
            Assert.AreEqual(1, listSummaryPage.NumberOfItemsInList(), "There must be 1 item.");
            listHomePage = listSummaryPage.ClickOnBreadCrumbLists();
            Assert.AreEqual(2, listHomePage.GetNumberOfLists());

            //Create new payment method at checkout
            CartPage cartPage = listHomePage.Header.ClickOnViewCart();
            Assert.AreEqual(2, cartPage.GetNumberOfItemsInCart());
            cartPage.GetQuantityInput();
            Assert.IsTrue(cartPage.IsQuantityInCartItem(2), "One of the quantity items must be 1.");
            CheckoutPage checkoutPage = cartPage.ProceedToCheckOut();
            checkoutPage.UserInfoIsPopulated();
            AddressModel addressModel = GetAddressModel();
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, addressModel.street);
            checkoutPage.SetAddressElement(AddressInputs.City, addressModel.city);
            checkoutPage.SetAddressElement(AddressInputs.State, addressModel.state);
            checkoutPage.SetAddressElement(AddressInputs.Postal, addressModel.postal);
            checkoutPage.SetAddressElement(AddressInputs.ATTN, "Tundra Restaurant Supply");
            checkoutPage.NextStep();
            checkoutPage.ClickEditAction(EditActions.SecureBillingInformation);
            checkoutPage.ClickOnAssingNewCard();
            SetCreditCard(checkoutPage);
            checkoutPage.NextStep();
            OrderConfirmationPage orderConfirmationPage = checkoutPage.PlaceOrderSubmitClick();
            orderConfirmationPage.ClickOnContinueShoppingButton(ContinueShoppingButtons.ContinueShopping);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void E2E04()
        {
            IndexPage indexPage = new IndexPage(driver, url);
            Login(indexPage);

            //Orders
            OrdersHomePage ordersHomePage = indexPage.Header.ClickOnOrders();
            ordersHomePage.SetFromDate("11/01/2019");
            ordersHomePage.SetToDate("12/26/2019");
            ordersHomePage.ClickOnSearchButton();
            //Assert.IsTrue(ordersHomePage.ValidateDatesAreInSearchRange("11/01/2019", "12/26/2019"), "Dates aren't correct");
            OrderDetailsPage orderDetailsPage = ordersHomePage.ClickOnOrderButtonByIndex(0);
            orderDetailsPage.ClickOnReorderButton();
            orderDetailsPage.ClickOnAddToCartButton();
            CartPage cartPage = orderDetailsPage.Header.ClickOnViewCart();
            cartPage.GetQuantityInput();

            //Edit Payment at Checkout
            CheckoutPage checkoutPage = cartPage.ProceedToCheckOut();
            checkoutPage.UserInfoIsPopulated();
            AddressModel addressModel = GetAddressModel();
            checkoutPage.SetAddressElement(AddressInputs.StreetAddress, addressModel.street);
            checkoutPage.SetAddressElement(AddressInputs.City, addressModel.city);
            checkoutPage.SetAddressElement(AddressInputs.State, addressModel.state);
            checkoutPage.SetAddressElement(AddressInputs.Postal, addressModel.postal);
            checkoutPage.SetAddressElement(AddressInputs.ATTN, "Tundra Restaurant Supply");
            checkoutPage.NextStep();
            checkoutPage.ClickEditAction(EditActions.SecureBillingInformation);
            checkoutPage.ClickOnAssingNewCard();
            SetCreditCard(checkoutPage);
            checkoutPage.NextStep();
            OrderConfirmationPage orderConfirmationPage = checkoutPage.PlaceOrderSubmitClick();
            orderConfirmationPage.ClickOnContinueShoppingButton(ContinueShoppingButtons.ContinueShopping);
        }

        private void SetCreditCard(CheckoutPage checkoutPage)
        {
            PaymentOptionModel paymentOptionModel = new PaymentOptionModel();
            paymentOptionModel.CardNumber = "4582416985071741";
            paymentOptionModel.ExpirationMont = "08";
            paymentOptionModel.ExpirationYear = "2020";
            paymentOptionModel.Cvv = "434";
            paymentOptionModel.HolderName = "Morgan Baker";
            paymentOptionModel.LastFourDigits = "1293";
            checkoutPage.SetBillingElement(BillingInputs.CardNumber, paymentOptionModel.CardNumber);
            checkoutPage.SetBillingElement(BillingInputs.CVV, paymentOptionModel.Cvv);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationMonth, paymentOptionModel.ExpirationMont);
            checkoutPage.SetBillingElement(BillingInputs.ExpirationYear, paymentOptionModel.ExpirationYear);
            checkoutPage.SetBillingElement(BillingInputs.CardHolderName, paymentOptionModel.HolderName);
        }

        private void Login(IndexPage indexPage)
        {
            LoginPage loginPage = indexPage.Header.ClickOnSignIn();
            loginPage.Login("jacosta@dfsupply.com", "test1234");
        }

        private AddressModel GetAddressModel()
        {
            AddressModel addressModel = new AddressModel();
            addressModel.street = "street 1";
            addressModel.state = "Alabama";
            addressModel.city = "city 1";
            addressModel.postal = "12345";
            addressModel.country = "Mexico";
            return addressModel;
        }
    }
}
