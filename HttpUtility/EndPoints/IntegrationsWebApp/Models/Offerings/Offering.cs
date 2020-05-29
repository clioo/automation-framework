using System;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Models
{
    public class Offering
    {
        public List<OfferingLink> Links { get; set; }
        public List<OfferingCompanionProduct> CompanionProducts { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Description { get; set; }
        public List<OfferingEnsembleEntry> EnsembleEntries { get; set; }
        public string FullName { get; set; }
        public HtmlPage HtmlPage { get; set; }
        public bool IsPreviewAble { get; set; }
        public bool IsPublishAble { get; set; }
    }

    #region Offering nested objects
    public class OfferingVariantEntry
    {
    }

    public class OfferingEnsembleEntry
    {
    }

    public class OfferingCompanionProduct
    {
    }

    public class OfferingLink
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public string Reference { get; set; }
    }

    public class HtmlPage
    {
        public string H1 { get; set; }
        public string Meta { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
    #endregion Offering nested objects
}
