using System.Collections.Generic;

namespace AllPoints.PageObjects.Base.Components.Contracts
{
    public interface IGenericTable
    {
        ICollection<string> GetRows();
        ICollection<string> GetRows(int nRow);
        ICollection<string> GetColumns();
        ICollection<string> GetColumns(string columnName);
        ICollection<string> GetColumns(int nCol);
    }
}
