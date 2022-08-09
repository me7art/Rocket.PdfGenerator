using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class CoverFilePathParameter : SimpleValueParameter<string>, IConsoleParameterHolder
    {
        public CoverFilePathParameter(string value, CustomWkHtmlArgsParameter customWkHtmlCoverArgs) : base("cover", quoteValue: true)
        {
            this.Enabled = value != null;
            this.Value = value;
            this.CustomWkHtmlCoverArgs = customWkHtmlCoverArgs;
        }

        public List<IConsoleParameterMapper> GetConsoleParameters()
        {
            return new List<IConsoleParameterMapper> { CustomWkHtmlCoverArgs };
        }

        public CustomWkHtmlArgsParameter CustomWkHtmlCoverArgs;
    }
}
