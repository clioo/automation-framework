namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Users
{
    public class UserRequest : User
    {
        public string AccountMasterExtId { get; set; }
        public string ContactExtId { get; set; }
        public string LoginExtId { get; set; }
    }
}
