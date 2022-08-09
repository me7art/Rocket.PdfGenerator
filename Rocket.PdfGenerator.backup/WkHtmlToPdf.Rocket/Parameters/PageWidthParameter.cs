using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class PageWidthParameter : SimpleValueParameter<float?>
    {
        public new bool IsEnabled() => base.Value != null;

        public PageWidthParameter() : base("--page-width")
        {
        }
    }
}
