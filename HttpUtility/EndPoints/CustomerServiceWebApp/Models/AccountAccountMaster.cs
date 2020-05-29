using System;

namespace HttpUtility.EndPoints.CustomerServiceWebApp.Models
{
    public class AccountAccountMaster
    {
        public Guid AccountIdentifier { get; set; }

        public Guid AccountMasterIdentifier { get; set; }

        public string AccountName { get; set; }

        public string ExternalIdentifier { get; set; }
    }
}