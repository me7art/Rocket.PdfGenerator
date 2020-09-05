using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public interface IInputData
    {
        VisitorResult Accept(IInputVisitor visitor);

        string CustomWkHtmlPageArgs { get; }
        string PageHeaderHtml { get; }
        string PageFooterHtml { get; }
        float? Zoom { get; }
    }
}
