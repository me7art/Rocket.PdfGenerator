using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class OutputStream : IOutputData
    {
        public Stream Stream;

        public OutputVisitorResult Accept(IOutputVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
