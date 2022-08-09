using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters
{
    public class SimpleValueParameter<T> : IConsoleParameterMapper
    {
        private bool _quoteValue = false;
        private bool _quoteSlash = false;

        readonly private string _simpleParameter;
        public T Value;

        public bool Enabled;

        public virtual bool IsEnabled() => Enabled;

        public virtual IEnumerable<string> ComposeArg()
        {
            var value = Value.ToString();
            if (_quoteSlash) value = value.Replace("\"", "\\\"");
            if (_quoteValue) value = $"\"{value}\"";

            return new List<string> { $" {_simpleParameter} {value} " };
        }

        public SimpleValueParameter(string parameter, bool quoteValue = false, bool quoteSlash = false)
        {
            _simpleParameter = parameter;
            _quoteValue = quoteValue;
            _quoteSlash = quoteSlash;
        }
    }
}
