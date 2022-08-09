using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;
using Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket
{
    internal class WKHtmlToPDFParameterBuilder : ParameterBuilder
    {
        public WKHtmlToPDFParameterBuilder(WKHtmlToPDFParameters parameters, WkHtmlToPdfParametersInternal parametersInternal)
        {
            AddParameter(parameters.Quiet, parameters.PageOrientation, parameters.PageSize, parameters.LowQuality, parameters.Grayscale);

            AddParameter(parameters.PageMargins);

            AddParameter(parameters.PageWidth, parameters.PageHeight);

            AddParameter(parametersInternal.HeaderFilePathParameter, parametersInternal.FooterFilePathParameter);

            AddParameter(parameters.CustomWkHtmlArgsParameter);

            AddParameter(parametersInternal.CoverFileParameter);

            AddParameter(parameters.GenerateTocParameter);

            foreach (var p in parametersInternal.InputFileParameters)
            {
                AddParameter(p);
            }

            AddParameter(parametersInternal.OutputFileParameter);
        }
    }
}
