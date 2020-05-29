using AllPoints.PageObjects.Base.Components.Modals;
using OpenQA.Selenium;
using AllPoints.Pages;
using AllPoints.PageObjects.Base.Components.Modals.Enums;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals
{
    public class AddressesNoEditsModal : NoEditsModal
    {
        public AddressesNoEditsModal(IWebDriver driver) : base(driver)
        {
        }

        public AddressesHomePage ClickOnCloseHeader()
        {
            ClickOnAction(ModalNoEditsActions.CloseFromHeader);

            return new AddressesHomePage(Driver);
        }

        public AddressesHomePage ClickOnCloseFooter()
        {
            ClickOnAction(ModalNoEditsActions.CloseOnFooter);

            return new AddressesHomePage(Driver);
        }
    }
}
