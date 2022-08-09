using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class CoverHtmlParameter : IContentParameter
    {
        public bool Enabled;
        public bool IsEnabled() => Enabled;

        public string CoverHtml;
        public string GetContent() => CoverHtml;
    }
}
