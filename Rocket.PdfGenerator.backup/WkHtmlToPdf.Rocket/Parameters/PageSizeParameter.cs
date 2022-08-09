using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class PageSizeParameter : IConsoleParameterMapper
    {
        public string Size = PageSize.Default;

        public bool IsEnabled() => Size != PageSize.Default;
        public IEnumerable<string> ComposeArg() => new List<string> { $" -s {Size} " };
    }
}
