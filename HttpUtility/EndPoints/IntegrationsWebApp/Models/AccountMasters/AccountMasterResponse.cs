using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters
{
    public class AccountMasterResponse : AccountMaster
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
        public int Version { get; set; }
        public int VersionStatus { get; set; }
        public string ExternalIdentifier { get; set; }
        public List<string> PriceLists { get; set; }
        public new string Identifier { get; set; }
        public string PlatformIdentifier { get; set; }
    }
}
