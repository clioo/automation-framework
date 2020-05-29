using System.Collections.Generic;

namespace AllPoints.PageObjects.MyAccountPOM.OrdersPOM.Contracts
{
    public interface IDomTable
    {
        ICollection<string> GetHeaders();

        bool ColumnIsOrdered(string column, bool ascending = true);

        int GetRowsCount();
    }
}