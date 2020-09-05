using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class PageOrientationParameter : IConsoleParameterMapper
    {
        public PageOrientation PageOrientation = PageOrientation.Default;

        public bool IsEnabled() => PageOrientation != PageOrientation.Default;
        public IEnumerable<string> ComposeArg() => new List<string> { $" -O {PageOrientation.ToString()} " };
    }
}
