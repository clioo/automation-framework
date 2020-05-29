using System;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{
    public class Category
    {
        public int CollapseOrder { get; set; }
        public string FullName { get; set; }
        public string ThumbnailImage { get; set; }
        public Guid ContentZoneId { get; set; }
        public bool IsLanding { get; set; }
        public bool IsMore { get; set; }
        public bool IsSubcatalog { get; set; }
        public bool IsTopMenu { get; set; }
        public string ShortName { get; set; }
        public int SortOrder { get; set; }
        public string UrlSegment { get; set; }
        public HtmlPage HtmlPage { get; set; }        
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
