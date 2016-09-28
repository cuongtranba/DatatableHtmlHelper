using System.Web.Mvc;

namespace DatatableHtmlHelper.DataTableHelper
{
    public static class DatatableHtmlHelper
    {
        public static Datatable Datatable(this HtmlHelper helper, string id)
        {
            return new Datatable(id);
        }

    }
}