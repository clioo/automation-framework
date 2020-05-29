namespace AllPoints.TestDataModels
{
    public class PaymentOptionModel
    {
        public string CardNumber { get; set; }
        public string ExpirationMont { get; set; }
        public string ExpirationYear { get; set; }
        public string HolderName { get; set; }
        public string Cvv { get; set; }
        public string LastFourDigits { get; set; }
    }
}