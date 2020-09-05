using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator.InputOutput
{
    public interface IOutputVisitor
    {
        OutputVisitorResult Visit(OutputFile outputFile);
        OutputVisitorResult Visit(OutputStream outputStream);
        OutputVisitorResult Visit(OutputByteArray outputByteArray);
    }
}
