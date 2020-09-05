using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.InputOutput.Formats
{
    public class WHHtmlInputBase
    {
        public string CustomWkHtmlPageArgs { get; set; }
        public string PageHeaderHtml { get; set; }
        public string PageFooterHtml { get; set; }
        public float? Zoom { get; set; }
    }
}
