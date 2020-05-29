using OpenQA.Selenium;
using AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Components;

namespace AllPoints.PageObjects.MyAccountPOM.AddressesPOM.Modals
{
    public class AddressesInformationModal : InformationModal
    {
        #region constructor
        public AddressesInformationModal(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructor

        public void ClickOnClose()
        {
            Close();
        }
    }
}
