namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins
{
    public class LoginResponse : Login
    {
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
        public int FailedLoginCount { get; set; }
        public string LastFailedLoginUtc { get; set; }
        public string LastLoginUtc { get; set; }
        public string Identifier { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
        public string PlatformIdentifier { get; set; }
    }
}
