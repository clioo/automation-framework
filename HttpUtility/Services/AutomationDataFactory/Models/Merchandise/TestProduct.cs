using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Models.Merchandise
{
    public class TestProduct
    {
        public decimal Cost { get; set; }
        public string DisplayProductCode { get; set; }
        //public bool IsOcm { get; set; }
        public string Name { get; set; }
        //public List<TestOemRelationship> OemRelationships { get; set; }
        //public string ProductCode { get; set; }
        //public int ProductLeadType { get; set; }
        public TestProp65Message Prop65Message { get; set; }
        public string SearchKeywords { get; set; }
        public TestProductShipping Shipping { get; set; }
        public List<TestSpecification> Specs { get; set; }
    }

    #region nested product objects
    public class TestOemRelationship
    {
        public string OemName { get; set; }
        public string OemSku { get; set; }
        public int Type { get; set; }
    }
    public class TestProp65Message
    {
        public string MessageBody { get; set; }
    }
    public class TestSpecification
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class TestProductShipping
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
    #endregion
}
