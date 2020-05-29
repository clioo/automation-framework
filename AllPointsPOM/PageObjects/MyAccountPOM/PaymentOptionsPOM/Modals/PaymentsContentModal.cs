using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using OpenQA.Selenium;
using AllPoints.PageObjects.MyAccountPOM.PaymentOptionsPOM;
using AllPoints.PageObjects.GenericWebPage.SharedElements.Modals.Enums;

namespace AllPoints.Features.MyAccount.PaymentOptions.Modals
{
    public class PaymentsContentModal : ContentModal
    {
        #region constructor
        public PaymentsContentModal(IWebDriver driver) : base(driver)
        {

        }
        #endregion

        public PaymentOptionsEditPage ClickOnEdit()
        {
            ClickOnAction(ModalContentActions.Edit);

            return new PaymentOptionsEditPage(Driver);
        }

        public void ClickOnCancel()
        {
            ClickOnAction(ModalContentActions.Cancel);
        }

        public void ClickOnDelete()
        {
            ClickOnAction(ModalContentActions.Delete);
        }

        public void ClickOnMakeDefault()
        {
            ClickOnAction(ModalContentActions.MakeDefault);
        }
    }
}
