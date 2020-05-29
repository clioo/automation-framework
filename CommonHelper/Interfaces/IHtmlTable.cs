using System.Collections.Generic;

namespace CommonHelper.Interfaces
{
    public interface IHtmlTable
    {
        IEnumerable<IEnumerable<string>> GetRows();
        IEnumerable<string> GetRow(int nRow);
        IEnumerable<string> GetColumns();
        IEnumerable<string> GetColumn(string columnName);
        IEnumerable<string> GetColumn(int colNumber);
        IEnumerable<string> GetHeaders();
    }
}
