using OpenQA.Selenium;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals
{
    public class AddressesConfirmationModal : ConfirmationModal
    {
        #region constructor
        public AddressesConfirmationModal(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public void ClickOnDelete()
        {
            ClickAnyAction(ModalConfirmationActions.Delete);
        }

        public void ClickOnCancel()
        {
            ClickAnyAction(ModalConfirmationActions.Cancel);
        }

        public void ClickOnClose()
        {
            ClickAnyAction(ModalConfirmationActions.Close);
        }
    }
}
