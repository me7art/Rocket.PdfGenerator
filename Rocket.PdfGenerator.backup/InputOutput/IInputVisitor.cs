using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator.InputOutput
{
    public interface IInputVisitor
    {
        VisitorResult Visit(InputFile inputFile);
        VisitorResult Visit(InputHtmlString inputString);
    }


}
