using CommonHelper;
using CommonHelper.BaseComponents;
using FMPOnlinePOM.PageObjects.Base.Components;
using OpenQA.Selenium;
using UtilityMenu = FMPOnlinePOM.PageObjects.Base.Components.UtilityMenu;

namespace FMPOnlinePOM.Base
{
    public class FmpBasePage : BasePOM
    {
        public UtilityMenu UtilityMenu;
        public TopHeader TopHeader;

        public FmpBasePage(IWebDriver driver) : base(driver)
        {
            //Initialize dependencies
            UtilityMenu = new UtilityMenu(Driver);
            TopHeader = new TopHeader(Driver);
        }
    }
}
