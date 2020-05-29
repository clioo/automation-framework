using System;

namespace DatabaseUtility.Models
{
    public class AccountMasterContent : ContentBase
    {
        public string ExternalIdentifier { get; set; }
        public string ExternalTaxIdentifier { get; set; }
        public Guid GroupPriceListIdentifier { get; set; }
        public bool IsWebEnabled { get; set; }
        public string Name { get; set; }
        public Guid PriceListIdentifier { get; set; }
        public string ProductPriceDiscount { get; set; }
        public TermsConfiguration TermsConfiguration { get; set; }

        public AccountMasterContent()
        {
            Identifier = Guid.NewGuid();
            IsWebEnabled = true;
            ProductPriceDiscount = "0";
            TermsConfiguration = new TermsConfiguration();
        }
    }

    public class TermsConfiguration
    {
        public bool HasPaymentTerms { get; set; }
        public string TermsDescription { get; set; }

        public TermsConfiguration()
        {
            HasPaymentTerms = true;
            TermsDescription = "NET 90 days";
        }
    }
}