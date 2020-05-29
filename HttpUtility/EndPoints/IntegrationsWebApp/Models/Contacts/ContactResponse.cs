using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts
{
    public class ContactResponse : Contact
    {
        public string Identifier { get; set; }
        public string AccountIdentifier { get; set; }
        public string PlatformIdentifier { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
