using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using OpenQA.Selenium;

namespace AllPoints.Features.MyAccount.PaymentOptions.Modals
{
    public class PaymentsConfirmationModal : ConfirmationModal
    {
        #region constructor
        public PaymentsConfirmationModal(IWebDriver driver) : base(driver)
        {
            
        }
        #endregion

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
