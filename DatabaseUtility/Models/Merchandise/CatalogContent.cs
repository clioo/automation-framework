using System;
using System.Collections.Generic;

namespace DatabaseUtility.Models.Merchandise
{
    public class CatalogContent : ContentBase
    {
        public string ExternalIdentifier { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }
        public List<Oem> OEMs { get; set; }

        public CatalogContent()
        {
            Identifier = Guid.NewGuid();
        }
    }

    public class Oem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class Menu
    {
    }
}