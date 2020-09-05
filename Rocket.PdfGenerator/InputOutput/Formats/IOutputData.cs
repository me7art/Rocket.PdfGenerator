using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public interface IOutputData
    {
        OutputVisitorResult Accept(IOutputVisitor visitor);
    }
}
