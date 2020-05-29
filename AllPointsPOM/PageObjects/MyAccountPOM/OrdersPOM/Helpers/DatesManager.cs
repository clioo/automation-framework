using System;
using System.Collections.Generic;
using System.Linq;

namespace AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Helpers
{
    public static class DatesManager
    {
        public static ICollection<DateTime> SortDates(ICollection<DateTime> dates, bool ascendent)
        {
            if (ascendent) return dates.ToList().OrderByDescending((x) => x.Date).ToList();

            return dates.ToList().OrderBy((x) => x.Date).ToList();
        }
    }
}