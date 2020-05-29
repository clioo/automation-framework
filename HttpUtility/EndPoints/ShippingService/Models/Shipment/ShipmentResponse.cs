using HttpUtility.EndPoints.ShippingService.Models.Base;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShipmentResponse
    {
        public ShipmentAddressResponse AddressDestination { get; set; }
        public ShipmentAddressResponse AddressOrigin { get; set; }
        public decimal FeeGroundDiscount { get; set; }
        public string InEstimableReason { get; set; }
        public bool IsEstimable { get; set; }
        public bool IsFreight { get; set; }
        public List<ShipmentItem> ShipmentItems { get; set; }
        public ShipmentRate DefaultShipmentRate { get; set; }
        public List<ShipmentRate> ShipmentRates { get; set; }
    }

    #region nested objects
    public class ShipmentAddressResponse : Address
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
    public class ShipmentItem
    {
        public decimal DeclarativeValue { get; set; }
        public decimal FreightClass { get; set; }
        public string ProductSku { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
    public class ShipmentRate
    {
        public decimal? Amount { get; set; }
        public bool IsFlatRate { get; set; }
        public bool IsFreight { get; set; }
        public bool IsTbd { get; set; }
        public SCServiceLevel ServiceLevel { get; set; }
        public int ServiceLevelCode { get; set; }
        public string ServiceLevelName { get; set; }
        public bool UseCustomerCarrier { get; set; }
        public decimal? Handling { get; set; }
        public SCServiceLevelType ServiceLevelType { get; set; }
    }
    #endregion nested objects
}