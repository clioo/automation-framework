namespace DatabaseUtility.Models.Merchandise.ProductContents
{
    public class ProdShipping
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
}