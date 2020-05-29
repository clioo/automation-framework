using System;

namespace DatabaseUtility.Models
{
    public class UserContent : ContentBase
    {
        public Guid AccountIdentifier { get; set; }
        public Guid ContactIdentifier { get; set; }
        public Guid DefaultAddressIdentifier { get; set; }
        public Guid DefaultCreditCardPaymentIdentifier { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsEnabled { get; set; }
        public Guid LoginIdentifier { get; set; }
        public bool UseAccountTermsAsDefaultPayment { get; set; }

        public UserContent(Guid loginIdentifier, Guid accountIdentifier, Guid contactIdentifier)
        {
            AccountIdentifier = accountIdentifier;
            ContactIdentifier = contactIdentifier;
            DefaultAddressIdentifier = new Guid();
            Identifier = Guid.NewGuid();
            IsAnonymous = false;
            IsEnabled = true;
            LoginIdentifier = loginIdentifier;
            UseAccountTermsAsDefaultPayment = false;
        }
    }
}