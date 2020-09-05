using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class FooterFilePathParameter : SimpleValueParameter<string>
    {
        public FooterFilePathParameter(string value) : base("--footer-html", quoteValue: true)
        {
            this.Enabled = value != null;
            this.Value = value;
        }
    }
}
