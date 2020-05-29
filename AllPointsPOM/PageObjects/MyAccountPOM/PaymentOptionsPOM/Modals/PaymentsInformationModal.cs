using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;
using OpenQA.Selenium;

namespace AllPoints.Features.MyAccount.PaymentOptions.Modals
{
    public class PaymentsInformationModal : InformationModal
    {
        #region constructor
        public PaymentsInformationModal(IWebDriver driver) : base(driver)
        {
        }
        #endregion

        public void ClickOnClose()
        {
            Close();
        }
    }
}
