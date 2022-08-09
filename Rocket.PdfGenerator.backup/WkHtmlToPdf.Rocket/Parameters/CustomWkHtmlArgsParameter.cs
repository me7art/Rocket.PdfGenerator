using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class CustomWkHtmlArgsParameter : IConsoleParameterMapper
    {
        public bool Enabled;
        public string Args;

        public bool IsEnabled() => Enabled;
        public IEnumerable<string> ComposeArg() => new List<string> { $" {Args} " };
    }
}
