using System;

namespace HttpUtility.EndPoints.CustomerServiceWebApp.Models
{
    public class User
    {
        public Guid Identifier { get; set; }
        public Guid LoginIdentifier { get; set; }
        public Guid AccountIdentifier { get; set; }
        public Guid ContactIdentifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
    }
}