using CommonHelper.Pages.IndexPage;
using FMPOnlinePOM.Components;
using FMPOnlinePOM.PageObjects.IndexHome.Contracts;
using OpenQA.Selenium;

namespace FMPOnlinePOM.PageObjects.IndexHome
{
    public class FmpIndexPage : BaseIndexPage, IFmpIndexPage
    {
        public FmpHeader Header;

        //TODO
        //remove
        public string FmpSubcatalog { get; set; } = "Fmp subcatalog";

        public FmpIndexPage(IWebDriver driver) : base(driver) 
        {
            Header = new FmpHeader(Driver);
        }

        public FmpIndexPage(IWebDriver driver, string url) : base(driver, url) 
        { 
            Header = new FmpHeader(Driver); 
        }
    }
}
