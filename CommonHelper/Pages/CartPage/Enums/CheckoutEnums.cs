using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Pages.CartPage.Enums
{
    public enum ContactInputs
    {
        FirstName,
        LastName,
        Company,
        PhoneNumber,
        Email,
        OPT
    }

    public enum AddressInputs
    {
        Country,
        ATTN,
        StreetAddress,
        Apt,
        City,
        State,
        Region,
        Postal
    }

    public enum BillingInputs
    {
        CardNumber,
        ExpirationMonth,
        ExpirationYear,
        CardHolderName,
        CVV
    }

    public enum AddressSelectOptions
    {
        Existing,
        New
    }

    public enum BillingSelectOptions
    {
        Existing,
        New
    }

    public enum ContinueShoppingButtons
    {
        PrintOrderConfirmation,
        ContinueShopping
    }

    public enum EditActions
    {
        ContactInformation,
        ShippingInformation,
        SecureBillingInformation,
        CartInformation
    }

}
