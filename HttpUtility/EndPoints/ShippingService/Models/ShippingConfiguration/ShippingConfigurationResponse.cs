using HttpUtility.EndPoints.ShippingService.Models.Base;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingConfigurationResponse : ShippingConfiguration
    {
        public List<SCServiceLevelResponse> ServiceLevels { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
    }

    #region nested objects
    public class SCServiceLevelResponse : SCServiceLevel
    {
        public int ServiceLevelTypeId { get; set; }
        public SCServiceLevelType ServiceLevelType { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }

    public class SCServiceLevelType
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
    #endregion nested objects
}
