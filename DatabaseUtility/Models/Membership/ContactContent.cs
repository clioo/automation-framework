using System;

namespace DatabaseUtility.Models
{
    public class ContactContent : ContentBase
    {
        public Guid AccountIdentifier { get; set; }
        public string ContactEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ContactContent(Guid accountIdentifier)
        {
            AccountIdentifier = accountIdentifier;
            Identifier = Guid.NewGuid();
        }
    }
}