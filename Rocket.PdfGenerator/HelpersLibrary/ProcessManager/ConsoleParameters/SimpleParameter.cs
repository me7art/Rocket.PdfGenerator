using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters
{
    public abstract class SimpleParameter : IConsoleParameterMapper
    {
        readonly private string _simpleParameter;

        public bool Enabled = false;

        public bool IsEnabled() => Enabled;
        public IEnumerable<string> ComposeArg() => new List<string>() { $" {_simpleParameter} " };

        public SimpleParameter(string parameter)
        {
            _simpleParameter = parameter;
        }
    }
}
