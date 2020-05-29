using System;

namespace DatabaseUtility.Models
{
    public class AccountContent : ContentBase
    {
        public Guid AccountMasterIdentifier { get; set; }

        public AccountContent(Guid accountMasterIdentifier)
        {
            AccountMasterIdentifier = accountMasterIdentifier;
            Identifier = Guid.NewGuid();
        }
    }
}