using DatabaseUtility.Models.Merchandise;
using System;
using System.Collections.Generic;

namespace DatabaseUtility.Models.OrderCapture
{
    public class CartContent : ContentBase
    {
        public CartAddress BillingAddress { get; set; }
        public bool BlindShippingEnabled { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string FreeParcelShipAmount { get; set; }
        public bool IncludesHandling { get; set; }
        public bool IsBlindShippingSelected { get; set; }
        public bool IsFreeParcelShip { get; set; }
        public Guid OrderIdentifier { get; set; }
        public string OrderNotes { get; set; }
        public OrderSummary OrderSummary { get; set; }
        public string SelectedAccessorials { get; set; }
        public ShipmentRate SelectedShipmentRate { get; set; }
        public Shipment Shipment { get; set; }
        public CartAddress ShippingAddress { get; set; }
        public Guid ShopperIdentifier { get; set; }
        public Guid StoreIdentifier { get; set; }
        public string Tax { get; set; }

        public CartContent()
        {
            Identifier = Guid.NewGuid();
            BlindShippingEnabled = true;
        }
    }

    //Property of Cart
    public class CartItem
    {
        public PriceContent Price { get; set; }
        public CheckoutPrice CheckoutPrice { get; set; }
        public string CheckoutPriceExtended { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public Guid OfferingIdentifier { get; set; }
        public OfferingMinimal OfferingMinimal { get; set; }
        public Guid ProductIdentifier { get; set; }
        public string ProductSKU { get; set; }
        public int Quantity { get; set; }
        public ShippingAttributes ShippingAttributes { get; set; }
    }

    //Property of Cart
    public class OrderSummary
    {
        public string Discount { get; set; }
        public string Handling { get; set; }
        public string ItemTotal { get; set; }
        public string FreightOptions { get; set; }
        public string Shipping { get; set; }
        public string ShippingHandling { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
    }

    //Property of Cart
    public class ShipmentRate
    {
        public string Amount { get; set; }
        public bool IsFreight { get; set; }
        public bool IsTbd { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public int ServiceLevelCode { get; set; }
        public string ServiceLevelName { get; set; }
        public bool UseCustomerCarrier { get; set; }
        public string Handling { get; set; }
    }

    //Property of Cart
    public class Shipment
    {
        public string Accessorials { get; set; }
        public CartAddress AddressDestination { get; set; }
        public CartAddress AddressOrigin { get; set; }
        public string FreeGroundDiscount { get; set; }
        public string InEstimableReason { get; set; }
        public bool IsEstimable { get; set; }
        public bool IsFreight { get; set; }
        public List<ShipmentItem> ShipmentItems { get; set; }
        public List<ShipmentRate> ShipmentRates { get; set; }
    }

    //Property of Cart and Shipment
    public class CartAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExternalIdentifier { get; set; }
        public string Postal { get; set; }
        public string StateProvinceRegion { get; set; }
    }

    //Property of CartItem
    public class CheckoutPrice
    {
        public string Amount { get; set; }
        public int Reason { get; set; }
    }

    //Property of CartItem
    public class OfferingMinimal
    {
        public string DisplayProductCode { get; set; }
        public Guid OfferingIdentifier { get; set; }
        public string OfferingFullName { get; set; }
        public string OfferingUrl { get; set; }
        public string ProductCode { get; set; }
        public Guid ProductIdentifier { get; set; }
        public string ProductTitle { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string BrandShortName { get; set; }
    }

    //Property of CartItem
    public class ShippingAttributes
    {
        public string FreightClass { get; set; }
        public bool IsFreeShip { get; set; }
        public bool IsFreightOnly { get; set; }
        public bool IsQuickShip { get; set; }
        public string WeightActual { get; set; }
        public string WeightDimensional { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
    }

    //Property of SelectedShipmentRate
    public class ServiceLevel
    {
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public string CalculationMethod { get; set; }
        public int Code { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string ShipmentRateAdjustment { get; set; }
        public int SortOrder { get; set; }
        public string Amount { get; set; }
        public string ExternalData { get; set; }
    }

    //Property of Shipment
    public class ShipmentItem
    {
        public string DeclaredValue { get; set; }
        public string FreightClass { get; set; }
        public string ProductSku { get; set; }
        public int Quantity { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
    }
}