using DatabaseUtility.Models.Merchandise.Common;
using System;

namespace DatabaseUtility.Models
{
    public class OfferingContent : ContentBase
    {
        public Guid CatalogIdentifier { get; set; }
        public string Description { get; set; }
        public string ExternalIdentifier { get; set; }
        public string FullName { get; set; }
        public Guid HomeCategory { get; set; }
        public HtmlPage HtmlPage { get; set; }
        public bool IsPreviewAble { get; }
        public bool IsPublishAble { get; }
        public Guid ProductIdentifier { get; set; }

        public OfferingContent(string fullName)
        {
            IsPreviewAble = true;
            IsPublishAble = true;
            HtmlPage = new HtmlPage(fullName);
            Identifier = Guid.NewGuid();
        }
    }
}