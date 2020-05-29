using System;

namespace HttpUtility.EndPoints.CustomerServiceWebApp.Models
{
    public class Login
    {
        public Guid Identifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
        public string Email { get; set; }
    }
}