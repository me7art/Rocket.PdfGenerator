using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

using Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket
{
    public class WKHtmlToPDFParameters
    {
        public PageOrientationParameter PageOrientation = new PageOrientationParameter();
        public PageSizeParameter PageSize = new PageSizeParameter();
        public LowQualityParameter LowQuality = new LowQualityParameter();
        public GrayscaleParameter Grayscale = new GrayscaleParameter();
        public ZoomParameter ZoomGlobal = new ZoomParameter() { Value = 1f };
        public PageMarginsParameter PageMargins = new PageMarginsParameter();
        public PageWidthParameter PageWidth = new PageWidthParameter();
        public PageHeightParameter PageHeight = new PageHeightParameter();
        public GenerateTocParameter GenerateTocParameter = new GenerateTocParameter();
        public CustomWkHtmlArgsParameter CustomWkHtmlArgsParameter = new CustomWkHtmlArgsParameter();
        public CustomWkHtmlArgsParameter CustomWkHtmlPageArgsGlobal = new CustomWkHtmlArgsParameter(); // Default parameter for all pages

        public CustomWkHtmlArgsParameter CustomWkHtmlCoverArgs = new CustomWkHtmlArgsParameter();

        public PageHeaderHtmlParameter PageHeaderHtml = new PageHeaderHtmlParameter();
        public PageFooterHtmlParameter PageFooterHtml = new PageFooterHtmlParameter();
        public QuietParameter Quiet = new QuietParameter();
    }
}
