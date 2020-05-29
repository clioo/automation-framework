using System;
using System.Collections.Generic;

namespace HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Catalogs
{
    public class Catalog
    {
        public string ExternalIdentifier { get; set; }
        public string Name { get; set; }
        public Guid Identifier { get; set; }
        public CatalogMenu Menu { get; set; }
        public List<CatalogOEM> OEMs { get; set; }
        public Guid PlatformIdentifier { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    #region nested catalog objects
    public class CatalogMenu
    {
        public List<CatalogMenuItem> MenuItems { get; set; }
        public CatalogOwner Owner { get; set; }
        public Guid Identifier { get; set; }
        public Guid PlatformIdentifier { get; set; }
    }

    public class CatalogMenuItem
    {
        public string Label { get; set; }
        public List<CatalogMenuItemSection> MenuItemSections { get; set; }
        public string Url { get; set; }
        public bool IsMore { get; set; }
    }

    public class CatalogMenuItemSection
    {
        public string Label { get; set; }
        public List<CatalogMenuSubItem> MenuSubItems { get; set; }
    }

    public class CatalogMenuSubItem
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public int Column { get; set; }
        public int ReferenceCount { get; set; }
    }

    public class CatalogOwner
    {
        public string Collection { get; set; }
        public Guid Identifier { get; set; }
    }

    public class CatalogOEM
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    #endregion nested catalog objects
}
