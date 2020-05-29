using AllPoints.Features.Models;
using AllPoints.TestDataModels;

namespace AllPoints.Features.MyAccount.PaymentOptions
{
    public class PaymentsTestData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }
        public PaymentOptionModel PaymentOption { get; set; }
        public AddressModel PreviouslyStoredAddress { get; set; }
    }
}
