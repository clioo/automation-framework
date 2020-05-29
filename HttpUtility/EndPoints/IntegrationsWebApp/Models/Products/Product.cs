using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{
    public class Product : EntityBase
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<string> AlternateSkus { get; set; }
        public string BrandSku { get; set; }
        public decimal Cost { get; set; }
        public string DisplayProductCode { get; set; }
        public bool IsOcm { get; set; }
        public bool IsPurchaseable { get; set; }
        public string Name { get; set; }
        public List<ProductOem> OemRelationships { get; set; }
        public string PrimaryBrandIdentifier { get; set; }
        public string ProductCode { get; set; }
        public int ProductLeadType { get; set; }
        public ProductProp65Message Prop65Message { get; set; }
        public string SearchKeywords { get; set; }
        public ProductShippingAttributes Shipping { get; set; }
        public List<ProductSpecification> Specs { get; set; }
    }

    #region product nested objects
    public class ProductSpecification
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductShippingAttributes
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

    public class ProductOem
    {
        public string OemName { get; set; }
        public string OemSku { get; set; }
        public int Type { get; set; }
    }

    public class ProductProp65Message
    {
        public string MessageBody { get; set; }
    }
    #endregion product nested objects
}
