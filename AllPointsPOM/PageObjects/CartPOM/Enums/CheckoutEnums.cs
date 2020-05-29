namespace AllPoints.PageObjects.CartPOM.Enums
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