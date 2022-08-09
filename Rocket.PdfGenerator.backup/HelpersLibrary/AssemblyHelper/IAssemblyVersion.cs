using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper
{
    public interface IAssemblyVersion
    {
        string Platform { get; }
        string AssembleVersion { get; }
    }
}
