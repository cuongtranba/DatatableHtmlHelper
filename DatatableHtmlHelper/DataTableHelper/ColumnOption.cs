using System.Collections.Generic;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class ColumnOption
    {
        public List<columns> columns { get; set; } = new List<columns>();
    }

    public class columns
    {
        public string data { get; set; }
    }
}