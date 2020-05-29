using CommonHelper;

namespace AllPoints.PageObjects.GenericWebPage.SharedElements
{
    public class CategoriesMenu
    {
        #region
        public DomElement CategoriesMenuContainer = new DomElement
        {
            locator = ".tmenu"
        };

        public DomElement CategoriesMenuContainerNav = new DomElement
        {
            locator = "nav"
        };
        #endregion

        #region Main items
        public DomElement CategoriesMenuContainerNavItemsContainer = new DomElement
        {
            locator = "ul"
        };

        public DomElement CategoriesMenuContainerNavItemsContainerItem = new DomElement()
        {
            locator = "li"
        };

        public DomElement CategoriesMenuContainerNavItemsContainerItemLink = new DomElement
        {
            locator = "a"
        };

        public DomElement CategoriesMenuContainerNavItemsContainerItemSubItemContainer = new DomElement
        {
            locator = "ul"
        };

        //public DomElement CategoriesMenuContainerNavItemsContainerItemSubItemContainerItem { get; set; }

        //public DomElement CategoriesMenuContainerNavItemsContainerItemSubItemContainerItemLink { get; set; }
        #endregion
    }
}