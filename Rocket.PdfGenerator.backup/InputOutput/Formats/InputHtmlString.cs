using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class InputHtmlString : WHHtmlInputBase, IInputData
    {
        public string HtmlString;

        public VisitorResult Accept(IInputVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
