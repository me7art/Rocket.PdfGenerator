using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class OutputFileParameter : SimpleValueParameter<string>
    {
        public OutputFileParameter(string value) : base("", quoteValue: true)
        {
            this.Enabled = true;
            this.Value = value;
        }
    }
}
