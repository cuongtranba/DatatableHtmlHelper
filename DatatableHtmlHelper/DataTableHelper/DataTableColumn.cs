using Newtonsoft.Json;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class DataTableColumn
    {
        public DataTableColumn(string title, string columnName, int order, bool orderable, bool searchable)
        {
            Title = title;
            ColumnName = columnName;
            Order = order;
            Orderable = orderable;
            Searchable = searchable;
        }
        public string Title { get; set; }
        [JsonIgnore]
        public string ColumnName { get; set; }
        [JsonIgnore]
        public int Order { get; set; }
        public bool Orderable { get; set; }
        public string Data => ColumnName;
        public string Render { get; set; }
        public bool Searchable { get; set; }
    }
}