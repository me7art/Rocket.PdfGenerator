using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class GenerateTocParameter : SimpleParameter, IConsoleParameterHolder
    {
        public GenerateTocParameter() : base(" toc ")
        {
        }

        public TocHeaderTextParameter TocHeaderText = new TocHeaderTextParameter();
        public CustomWkHtmlArgsParameter CustomWkHtmlTocArgs = new CustomWkHtmlArgsParameter();

        public class TocHeaderTextParameter : SimpleValueParameter<string>
        {
            public TocHeaderTextParameter() : base("--toc-header-text", quoteValue: true, quoteSlash: true)
            { }
        }

        public List<IConsoleParameterMapper> GetConsoleParameters()
        {
            return new List<IConsoleParameterMapper> { TocHeaderText, CustomWkHtmlTocArgs };
        }
    }
}
