namespace HttpUtility.EndPoints.ShippingService.Models.Auth
{

    public class ShippingAuthRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
    }
}
