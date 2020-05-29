using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{     
    public class AccountMaster
    {
        //externalIdentifier
        public string Identifier { get; set; }
        public bool CreateAccount { get; set; }
        public string ExternalData { get; set; }
        public string TaxIdentifier { get; set; }
        public string GroupIdentifier { get; set; }
        public bool IsWebEnabled { get; set; }
        public string Name { get; set; }
        public decimal ProductPriceDiscount { get; set; }
        public AccountMasterTermConfiguration TermsConfiguration { get; set; }
        public bool UseAccountTermsAsDefaultPayment { get; set; }
    }
}
