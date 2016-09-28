using System.Text;

namespace DatatableHtmlHelper.DataTableHelper
{
    public abstract class OptionBuilder
    {
        protected object Options;
        protected StringBuilder optionBuilder;
        protected OptionBuilder(object options)
        {
            this.Options = options;
            optionBuilder = new StringBuilder();
        }
        public abstract string Build();
    }
}