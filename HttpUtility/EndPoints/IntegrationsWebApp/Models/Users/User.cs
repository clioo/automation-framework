namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Users
{
    public class User
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsEnabled { get; set; }
    }
}
