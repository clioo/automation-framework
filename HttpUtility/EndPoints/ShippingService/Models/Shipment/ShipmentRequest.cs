using HttpUtility.EndPoints.ShippingService.Models.Base;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models.Shipment
{
    public class ShipmentRequest
    {
        public string OrderId { get; set; }
        public Address ShipFromAddress { get; set; }
        public Address ShipToAddress { get; set; }
        public List<ShipmentProduct> Products { get; set; }
    }

    #region nested objects
    public class ShipmentProduct
    {
        public string ProductSku { get; set; }
        public decimal CheckoutPriceExtended { get; set; }
        public int Quantity { get; set; }
        public ShipmentProductShipping Shipping { get; set; }
    }
    public class ShipmentProductShipping
    {
        public decimal FreightClass { get; set; }
        public bool IsFreeShip { get; set; }
        public bool IsFreightOnly { get; set; }
        public bool IsQuickShip { get; set; }
        public decimal WeightActual { get; set; }
        public decimal WeightDimensional { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
    }
    #endregion nested objects
}
