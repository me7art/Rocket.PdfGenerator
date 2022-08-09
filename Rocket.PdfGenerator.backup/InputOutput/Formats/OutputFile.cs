using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class OutputFile : IOutputData
    {
        public string OutputFileName;

        public OutputVisitorResult Accept(IOutputVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
