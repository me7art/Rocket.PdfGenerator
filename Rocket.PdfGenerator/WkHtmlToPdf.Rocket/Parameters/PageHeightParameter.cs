using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class PageHeightParameter : SimpleValueParameter<float?>
    {
        public new bool IsEnabled() => base.Value != null;

        public PageHeightParameter() : base("--page-height")
        {
        }
    }
}
