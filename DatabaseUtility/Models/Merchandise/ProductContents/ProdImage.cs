namespace DatabaseUtility.Models.Merchandise.ProductContents
{
    public class ProdImage
    {
        public string AltText { get; set; }
        public string ExternalIdentifier { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
        public string SourceUrl { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}