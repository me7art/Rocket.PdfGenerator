using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    internal class WkHtmlToPdfParametersInternal
    {
        public OutputFileParameter OutputFileParameter;

        public List<InputFileParameter> InputFileParameters = new List<InputFileParameter>();

        public CoverFilePathParameter CoverFileParameter;
        public HeaderFilePathParameter HeaderFilePathParameter;
        public FooterFilePathParameter FooterFilePathParameter;
    }
}
