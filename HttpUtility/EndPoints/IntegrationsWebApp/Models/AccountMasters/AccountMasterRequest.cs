using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters
{
    public class AccountMasterRequest : AccountMaster
    {
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public List<string> PriceListIds { get; set; }
    }
}
