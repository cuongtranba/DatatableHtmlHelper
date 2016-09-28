using System.Web.Mvc.Html;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class DataTableColumn
    {
        public DataTableColumn(string columnName, int order)
        {
            ColumnName = columnName;
            Order = order;
        }

        public string ColumnLabel { get; set; }
        public string ColumnName { get; set; }
        public int Order { get; set; }
    }
}