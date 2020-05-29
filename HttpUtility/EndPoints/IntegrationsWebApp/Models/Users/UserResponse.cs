using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Users
{
    public class UserResponse : User
    {
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
        public string AccountIdentifier { get; set; }
        public string ContactIdentifier { get; set; }
        public string CookieString { get; set; }
        public string DefaultAddressIdentifier { get; set; }
        public string DefaultCreditCardPaymentIdentifier { get; set; }
        public string Identifier { get; set; }
        public bool IsAnonymous { get; set; }
        public string LoginIdentifier { get; set; }
        public string PlatformIdentifier { get; set; }
        public object RoleGuids { get; set; }
        public bool UseAccountTermsAsDefaultPayment { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
    }
}
