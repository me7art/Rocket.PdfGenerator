using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    internal class InputFileParameter : SimpleValueParameter<string>, IConsoleParameterHolder
    {
        public InputFileParameter(string value,
            CustomWkHtmlArgsParameter customWkHtmlPageArgs,
            HeaderFilePathParameter headerFilePath,
            FooterFilePathParameter footerFilePath,
            ZoomParameter zoom)
            : base("", quoteValue: true)
        {
            this.Enabled = true;
            this.Value = value;
            this.CustomWkHtmlPageArgs = customWkHtmlPageArgs;
            this.HeaderFilePath = headerFilePath;
            this.FooterFilePath = footerFilePath;
            this.Zoom = zoom;
        }

        public List<IConsoleParameterMapper> GetConsoleParameters()
        {
            return new List<IConsoleParameterMapper> { CustomWkHtmlPageArgs, HeaderFilePath, FooterFilePath, Zoom };
        }

        public CustomWkHtmlArgsParameter CustomWkHtmlPageArgs;

        public HeaderFilePathParameter HeaderFilePath;
        public FooterFilePathParameter FooterFilePath;

        public ZoomParameter Zoom = new ZoomParameter() { Value = 1f };
    }
}
