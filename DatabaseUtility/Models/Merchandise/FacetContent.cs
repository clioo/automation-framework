using System;
using System.Collections.Generic;

namespace DatabaseUtility.Models.Merchandise
{
    public class FacetContent : ContentBase
    {
        public string Name { get; set; }
        public List<PossibleFacetValue> PossibleFacetValues { get; set; }
        public int SortOrder { get; set; }

        public FacetContent()
        {
            Identifier = Guid.NewGuid();
        }
    }

    public class PossibleFacetValue
    {
        public Guid Identifier { get; set; }
        public int SortOrder { get; set; }
        public string Value { get; set; }

        public PossibleFacetValue(string value, int sort)
        {
            Identifier = Guid.NewGuid();
        }
    }
}