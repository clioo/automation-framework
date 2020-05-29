using DatabaseUtility.Constants;
using System;

namespace DatabaseUtility.Models
{
    public class LoginContent : ContentBase
    {
        public string Email { get; set; }
        public int FailedLoginCount { get; set; }
        public bool IsEnabled { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Username { get; set; }

        public LoginContent()
        {
            FailedLoginCount = 0;
            Identifier = Guid.NewGuid();
            IsEnabled = true;
            PasswordHash = ConfigurationConstants.DefaultPasswordHash;
            PasswordSalt = ConfigurationConstants.DefaultPasswordSalt;
        }
    }
}