using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class Datatable
    {
        private string tableHtml = "<table {id} {htmlattributes}><thead><tr>{columnheader}</tr></thead><tbody></tbody></table>";

        private string tableScript = "<script type=\"text/javascript\">$(document).ready(function(){$(\'#{id}\').DataTable({dataoption});});</script>";

        private StringBuilder tableOptionBuilder=new StringBuilder();

        public Datatable(string id)
        {
            FillValueDataTable("{id}", $"id=\"{id}\"");
            FillValueDataTableScript("{id}", id);
        }

        private Datatable FillValueDataTableScript(string attribute, string value)
        {
            tableScript = tableScript.Replace(attribute, value);
            return this;
        }

        public Datatable AddColumns(List<DataTableColumn> dataTableColumns)
        {
            var headerBuilder = new StringBuilder();
            var columnOption =new ColumnOption();

            dataTableColumns = dataTableColumns.OrderBy(c => c.Order).ToList();
            foreach (var dataTableColumn in dataTableColumns)
            {
                headerBuilder.Append($"<th>{dataTableColumn.ColumnName}</th>");
                var column=new columns()
                {
                    data = dataTableColumn.ColumnName.Replace(" ",String.Empty)
                };
                columnOption.columns.Add(column);
            }
            var columnAttributeString = JsonConvert.SerializeObject(columnOption).TrimStart('{').TrimEnd('}')+",";
            tableOptionBuilder.Append(columnAttributeString);
            return FillValueDataTable("{columnheader}", headerBuilder.ToString());
        }
        public Datatable AddHtmlAttributes(object htmlAttributes)
        {
            var properties = htmlAttributes.GetType().GetProperties();
            var attributeBuilder = new StringBuilder();
            foreach (var propertyInfo in properties)
            {
                attributeBuilder.Append($" {propertyInfo.Name.ToLower()}=\"{propertyInfo.GetValue(htmlAttributes, null)}\"");
            }
            
            return FillValueDataTable("{htmlattributes}", attributeBuilder.ToString());
        }

        public Datatable AddDataTableOption(object options)
        {
            if (options == null)
            {
                return FillValueDataTableScript("dataoption", string.Empty);
            }
            var properties = options.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(options, null);
                if (propertyValue is bool)
                {
                    propertyValue = propertyValue.ToString().ToLower();
                }
                if (propertyValue is string)
                {
                    propertyValue = $"\"{propertyValue}\"";
                }
                tableOptionBuilder.Append($"\"{propertyName}\":{propertyValue},");
            }
            return FillValueDataTableScript("dataoption", tableOptionBuilder.ToString().TrimEnd(','));
        }
        private Datatable FillValueDataTable(string attribute, string value)
        {
            tableHtml = tableHtml.Replace(attribute, value);
            return this;
        }

        public HtmlString ToHtml()
        {
            return new HtmlString(tableHtml + tableScript);
        }
    }
}