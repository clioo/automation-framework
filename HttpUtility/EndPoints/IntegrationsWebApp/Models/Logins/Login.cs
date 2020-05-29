namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins
{
    public class Login
    {
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsEnabled { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string UserName { get; set; }
    }
}
