using Newtonsoft.Json;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class DataTableColumn
    {
        public DataTableColumn(string columnLabel, string columnName, int order, bool orderable)
        {
            ColumnLabel = columnLabel;
            ColumnName = columnName;
            Order = order;
            Orderable = orderable;
        }
        [JsonIgnore]
        public string ColumnLabel { get; set; }
        [JsonIgnore]
        public string ColumnName { get; set; }
        [JsonIgnore]
        public int Order { get; set; }
        public bool Orderable { get; set; }
        public string Data => ColumnName;
    }
}