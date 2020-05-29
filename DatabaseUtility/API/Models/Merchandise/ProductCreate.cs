using DatabaseUtility.Models.Merchandise.ProductContents;
using System;
using System.Collections.Generic;

namespace DatabaseUtility.API.Models.Merchandise
{
    public class ProductCreate
    {
        public string BrandSku { get; set; }
        public string Cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Prop65Message { get; set; }
        public Guid PrimaryBrandId { get; set; }
        public bool IsPurchaseable { get; set; }
        public List<ProdImage> Images { get; set; }
        public ProdShipping Shipping { get; set; }
        public List<Guid> AltBrands { get; set; }
        public List<Specification> Specifications { get; set; }
        public List<ProductFacet> ProductFacets { get; set; }
    }
}