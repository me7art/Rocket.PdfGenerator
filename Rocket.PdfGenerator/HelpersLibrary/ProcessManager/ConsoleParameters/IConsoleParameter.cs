using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters
{
    public interface IConsoleParameter
    {
        bool IsEnabled();
    }

    public interface IConsoleParameterMapper : IConsoleParameter
    {
        IEnumerable<string> ComposeArg();
    }

    public interface IConsoleParameterHolder : IConsoleParameter
    {
        List<IConsoleParameterMapper> GetConsoleParameters();
    }

}
