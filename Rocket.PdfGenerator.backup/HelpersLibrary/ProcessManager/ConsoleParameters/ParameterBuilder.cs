using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters
{
    public class ParameterBuilder
    {
        List<IConsoleParameterMapper> _consoleParameters = new List<IConsoleParameterMapper>();

        public ParameterBuilder()
        {

        }

        private void AddParameterMapper(IConsoleParameterMapper parameter)
        {
            _consoleParameters.Add(parameter);
        }

        private void AddParameterHolder(IConsoleParameterHolder parameterHolder)
        {
            foreach(var parameter in parameterHolder.GetConsoleParameters())
            {
                AddParameter(parameter);
            }
        }

        public ParameterBuilder AddParameter(params IConsoleParameter[] parameters)
        {
            foreach (var param in parameters)
            {
                if (param == null || param.IsEnabled() == false)
                {
                    continue;
                }
                
                if (param is IConsoleParameterMapper paramMapper)
                {
                    AddParameterMapper(paramMapper);
                }
                if (param is IConsoleParameterHolder paramHolder)
                {
                    AddParameterHolder(paramHolder);
                }
            }
            return this;
        }

        public string BuildString()
        {
            var args = Build();
            var str = string.Join("", args);
            return str;
        }

        public IEnumerable<string> Build()
        {
            foreach (var param in _consoleParameters)
            {
                if (param.IsEnabled() == true)
                {
                    foreach (var arg in param.ComposeArg())
                    {
                        yield return arg;
                    }
                }
            }
        }
    }
}
