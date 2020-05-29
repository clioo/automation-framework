namespace DatabaseUtility.Models.Merchandise.Common
{
    public class HtmlPage
    {
        public string H1 { get; set; }
        public string Meta { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public HtmlPage(string productName)
        {
            this.H1 = $"H1 {productName}";
            this.Meta = $"Meta {productName}";
            this.Title = $"Title {productName}";
            this.Url = $"testing/{productName.Replace(' ', '-')}";
        }
    }
}