using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class Datatable
    {
        private string tableHtml = "<table {id} {htmlattributes}><thead></thead><tbody></tbody></table>";

        private string tableScript = "<script type=\"text/javascript\">$(document).ready(function(){$(\'#{id}\').DataTable({{dataoption}{columnSetting}});});</script>";
        StringBuilder tableOptionBuilder = new StringBuilder();
        Stack<string> stackAttribute = new Stack<string>();

        public Datatable(string id)
        {
            FillValueDataTable("{id}", $"id=\"{id}\"");
            FillValueDataTableScript("{id}", id);
        }


        public Datatable AddColumns(List<DataTableColumn> dataTableColumns)
        {
            dataTableColumns = dataTableColumns.OrderBy(c => c.Order).ToList();
            var columnSettingObject = new { columns = dataTableColumns };
            var columnSettingString = JsonConvert.SerializeObject(columnSettingObject, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() }).TrimStart('{').TrimEnd('}');
            return FillValueDataTableScript("{columnSetting}", columnSettingString);
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
                return FillValueDataTableScript("{dataoption}", string.Empty);
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
                else if (propertyValue is string)
                {
                    propertyValue = propertyValue.ToString().Contains("function") ? $"{propertyValue}" : $"\"{propertyValue}\"";
                }
                tableOptionBuilder.Append($"\"{propertyName}\":{propertyValue},");
            }
            return FillValueDataTableScript("{dataoption}", tableOptionBuilder.ToString());
        }

        public HtmlString ToHtml()
        {
            return new HtmlString(tableHtml + tableScript);
        }

        #region private method
        private Datatable FillValueDataTable(string attribute, string value)
        {
            tableHtml = tableHtml.Replace(attribute, value);
            return this;
        }

        private Datatable FillValueDataTableScript(string attribute, string value)
        {
            tableScript = tableScript.Replace(attribute, value);
            return this;
        }
        #endregion
    }
}