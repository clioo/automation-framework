namespace DatabaseUtility.API.Models.Configuration
{
    public class UserCreate
    {
        public string Email { get; set; }
        public string AccountMasterExternalIdentifier { get; set; }
        public bool IsPayingWithTerms = false;
    }
}