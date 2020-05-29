using System;

namespace DatabaseUtility.Models
{
    public class CardTokenContent : ContentBase
    {
        public CardTokenAddress Address { get; set; }
        public string CardType { get; set; }
        public string CustomerId { get; set; }
        public Owner Owner { get; set; }
        public string Email { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsReadonly { get; set; }
        public string LastFourDigits { get; set; }
        public string NameOnCard { get; set; }
        public string TokenId { get; set; }

        public CardTokenContent(string tokenId)
        {
            Identifier = Guid.NewGuid();
            ExternalIdentifier = tokenId;
            TokenId = tokenId;
        }
    }

    public class CardTokenAddress
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StateProvinceRegion { get; set; }
        public string Postal { get; set; }
    }
}