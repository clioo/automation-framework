using DatabaseUtility.Models.Merchandise.ProductContents;
using System;
using System.Collections.Generic;

namespace DatabaseUtility.Models
{
    public class ProductContent : ContentBase
    {
        public List<Guid> AlternateBrandIdentifiers { get; set; }
        public List<string> AlternateSkus { get; set; }
        public string BrandSku { get; set; }
        public string Cost { get; set; }
        public string Description { get; set; }
        public string DisplayProductCode { get; set; }
        public string ExternalIdentifier { get; set; }
        public List<ProdImage> Images { get; set; }
        public string Inventory { get; set; }
        public bool IsPurchaseable { get; set; }
        public string Name { get; set; }
        public List<string> Notes { get; set; }
        public List<OemItem> OemRelationships { get; set; }
        public Guid PrimaryBrandIdentifier { get; set; }
        public string ProductCode { get; set; }
        public Prop65Message Prop65Message { get; set; }
        public ProdShipping Shipping { get; set; }
        public List<Specification> Specs { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductFacet> ProductFacets { get; set; }

        public ProductContent(string productCode)
        {
            Identifier = Guid.NewGuid();
            DisplayProductCode = productCode;
            ProductCode = productCode;
            AlternateBrandIdentifiers = new List<Guid>();
            AlternateSkus = new List<string>();
            Images = new List<ProdImage>();
            Notes = new List<string>();
            OemRelationships = new List<OemItem>();
            Specs = new List<Specification>();
            ProductAttributes = new List<ProductAttribute>();
            ProductFacets = new List<ProductFacet>();
        }
    }
}