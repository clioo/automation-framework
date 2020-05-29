using OpenQA.Selenium;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals
{
    public class AddressesContentModal : ContentModal
    {
        #region constructor
        public AddressesContentModal(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public EditAddressPage ClickOnEdit()
        {
            ClickOnAction(ModalContentActions.Edit);

            return new EditAddressPage(Driver);
        }
        
        public void ClickOnDelete()
        {            
            ClickOnAction(ModalContentActions.Delete);
        }

        public void ClickOnCancel()
        {
            ClickOnAction(ModalContentActions.Cancel);
        }

        public void ClickOnMakeDefault()
        {
            ClickOnAction(ModalContentActions.MakeDefault);
        }
    }
}
