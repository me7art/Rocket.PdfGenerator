using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class InputFile : WHHtmlInputBase, IInputData
    {
        public string File;

        public VisitorResult Accept(IInputVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

}
