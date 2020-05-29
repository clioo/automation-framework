namespace DatabaseUtility.API.Models.Membership
{
    public class CardTokenCreate
    {
        public bool setAsDefault { get; set; }
        public string CardType { get; set; }
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string ExternalIdentifier { get; }
        public bool IsReadonly { get; set; }
        public string LastFourDigits { get; set; }
        public string NameOnCard { get; set; }
        public string TokenId { get; set; }
        public string CompanyName { get; set; }
        public string AddressAddressLine1 { get; set; }
        public string AddressAddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string AddressStateProvinceRegion { get; set; }
        public string AddressPostal { get; set; }
        public bool isAccountMasterLevel { get; set; }
    }
}