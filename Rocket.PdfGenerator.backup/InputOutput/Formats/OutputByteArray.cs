using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class OutputByteArray : IOutputData
    {
        public byte[] ByteArr;

        public OutputVisitorResult Accept(IOutputVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
