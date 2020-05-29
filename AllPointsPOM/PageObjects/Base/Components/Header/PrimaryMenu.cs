using CommonHelper;

namespace AllPoints.PageObjects.Base.Components.Header
{
    public class PrimaryMenu
    {
        #region containers
        public DomElement PrimaryMenuContainer = new DomElement
        {
            locator = ".tnav"
        };
        public DomElement PrimaryMenuContainerContact = new DomElement
        {
            locator = "nav.contact"
        };
        public DomElement PrimaryMenuContainerNav = new DomElement
        {
            locator = "nav:nth-of-type(2) ul"
        };
        #endregion

        #region main elements
        public DomElement PrimaryMenuContainerNavSignInButton = new DomElement
        {
            locator = "li.user a"
        };
        public DomElement ContactSectionPhoneNumber = new DomElement
        {
            locator = ".phone a span"
        };
        #region my account submenu
        public DomElement PrimaryMenuContainerNavAccountMenu = new DomElement
        {
            locator = "li.dropdown.user"
        };
        public DomElement PrimaryMenuContainerNavMyAccountMenuItem = new DomElement
        {
            locator = "li a.btn"
        };

        private DomElement MyAccountDashboardSubItem = new DomElement
        {
            locator = "a.nav-link.dashboard"
        };

        private DomElement MyAccountContactInfoSubItem = new DomElement
        {
            locator = "a.nav-link.dashboard"
        };

        private DomElement MyAccountAddressesSubItem = new DomElement()
        {
            locator = "a.nav-link.addresses"
        };

        private DomElement MyAccountPaymentOptionsSubItem = new DomElement()
        {
            locator = "a.nav-link.payment-options"
        };

        //lists

        private DomElement MyAccountOrdersSubItem = new DomElement()
        {
            locator = "a.nav-link.orders"
        };

        //signout button
        private DomElement MyAccountLogoutSubItem = new DomElement()
        {
            locator = "li .icon.icon-sign-out"
        };

        //pending
        private DomElement TrackOrderMenuItem = new DomElement()
        {
            locator = ""
        };

        //pending
        public DomElement QuickOrderMenuItem = new DomElement()
        {
            locator = ".quick-order"
        };

        //pending
        private DomElement cartMenuItem = new DomElement()
        {
            locator = "li#miniCart"
        };
        #endregion

        #region cart and mini cart
        public DomElement PrimaryMenuContainerNavLiCart = new DomElement
        {
            locator = "li.dropdown.cart"
        };

        public DomElement PrimaryMenuContainerNavLiCartButton = new DomElement
        {
            locator = "li.dropdown.cart button.minicart"
        };

        public DomElement PrimaryMenuContainerNavLiCartMinicart = new DomElement
        {
            locator = "article.mini-cart"
        };

        public DomElement PrimaryMenuContainerNavLiCartMinicartActions = new DomElement
        {
            locator = "section.actions"
        };

        public DomElement PrimaryMenuContainerNavLiCartMinicartActionsViewCartButton = new DomElement
        {
            locator = "a.btn"
        };

        public DomElement PrimaryMenuContainerNavLiCartMinicartActionsContinueShoppingLink = new DomElement
        {
            locator = "button.btn"
        };
        #endregion
        #endregion
    }
}
