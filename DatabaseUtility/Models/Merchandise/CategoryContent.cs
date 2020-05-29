using DatabaseUtility.Models.Merchandise.Common;
using System;

namespace DatabaseUtility.Models.Merchandise
{
    public class CategoryContent : ContentBase
    {
        public Guid CatalogIdentifier { get; set; }
        public int CollapseOrder { get; set; }
        public string ExternalIdentifier { get; set; }
        public string FullName { get; set; }
        public bool IsLanding { get; set; }
        public bool IsTopMenu { get; set; }
        public HtmlPage HtmlPage { get; set; }
        public Guid? ParentIdentifier { get; set; }
        public string ShortName { get; set; }
        public int SortOrder { get; set; }
        public string UrlSegment { get; set; }
        public int HierarchyLevel { get; set; }
        public int LeftBower { get; set; }
        public int RightBower { get; set; }

        public CategoryContent(string fullName)
        {
            Identifier = Guid.NewGuid();

            HtmlPage = new HtmlPage(fullName);
            FullName = fullName;
            ShortName = fullName;
            UrlSegment = $"/{fullName}";
        }
    }
}