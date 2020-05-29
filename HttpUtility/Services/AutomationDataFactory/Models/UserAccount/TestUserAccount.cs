namespace HttpUtility.Services.AutomationDataFactory.Models.UserAccount
{
    public class TestUserAccount
    {
        public string Email { get; set; }
        public string Username { get => GetUsername(Email); }
        public string Password { get => "1234"; }
        public TestExternalIdentifiers AccountExternalIds { get; set; }
        public TestContactInformation ContactInformation { get; set; }

        private string GetUsername(string userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail) && userEmail.Contains("@"))
            {
                int atIndex = userEmail.LastIndexOf('@');
                return userEmail.Substring(0, atIndex);
            }
            return userEmail;
        }
    }
}
