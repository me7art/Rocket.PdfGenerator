using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator
{
    public class WkHtmlInput
    {
        public string Input { get; set; }

        public string CustomWkHtmlPageArgs { get; set; }

        public string PageHeaderHtml { get; set; }

        public string PageFooterHtml { get; set; }

        public float? Zoom { get; set; } = null;

        public WkHtmlInput(string inputFileOrUrl)
        {
            this.Input = inputFileOrUrl;
        }
    }
}
