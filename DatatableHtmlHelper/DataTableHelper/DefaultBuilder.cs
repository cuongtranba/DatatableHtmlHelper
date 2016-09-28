using System;

namespace DatatableHtmlHelper.DataTableHelper
{
    public class DefaultOptionBuilder : OptionBuilder
    {
        public DefaultOptionBuilder(object options) : base(options)
        {

        }

        public override string Build()
        {
            if (Options == null)
            {
                return String.Empty;
            }
            var properties = Options.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(Options, null);
                if (propertyValue is bool)
                {
                    propertyValue = propertyValue.ToString().ToLower();
                }
                optionBuilder.Append($"\"{propertyName}\":{propertyValue},");
            }
            return optionBuilder.ToString();
        }


    }
}