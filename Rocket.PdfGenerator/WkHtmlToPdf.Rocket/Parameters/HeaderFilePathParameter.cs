using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class HeaderFilePathParameter : SimpleValueParameter<string>
    {
        public HeaderFilePathParameter(string value) : base("--header-html", quoteValue: true)
        {
            this.Enabled = value != null;
            this.Value = value;
        }
    }
}
