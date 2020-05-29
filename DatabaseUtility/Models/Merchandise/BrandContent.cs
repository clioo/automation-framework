using DatabaseUtility.Models.Merchandise.Common;
using System;

namespace DatabaseUtility.Models.Merchandise
{
    public class BrandContent : ContentBase
    {
        public int Favorability { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string UrlSegment { get; set; }
        public HtmlPage HtmlPage { get; set; }
        public string ExternalIdentifier { get; set; }

        public BrandContent(int favorability, string fullName)
        {
            Identifier = Guid.NewGuid();
            Favorability = favorability;
            FullName = fullName;
            ShortName = fullName.ToLower();
            UrlSegment = "testing/" + fullName.Replace(' ', '-');
            HtmlPage = new HtmlPage(fullName);
        }
    }
}