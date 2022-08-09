using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Linq;

using Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper;
using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager;
using Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters;
using Rocket.PdfGenerator.WkHtmlToPdf.Rocket;

namespace Rocket.PdfGenerator
{
    public class RocketPDFParameterManager : IWkHtmlToPDFParametersProvider
    {
        private WKHtmlToPDFParameters _wkHtmlToPdfParameters = new WKHtmlToPDFParameters();

        public WKHtmlToPDFParameters GetParameters() => _wkHtmlToPdfParameters;

        public PageOrientation Orientation
        {
            get => _wkHtmlToPdfParameters.PageOrientation.PageOrientation;
            set => _wkHtmlToPdfParameters.PageOrientation.PageOrientation = value;
        }

        public string Size
        {
            get => _wkHtmlToPdfParameters.PageSize.Size;
            set => _wkHtmlToPdfParameters.PageSize.Size = value;
        }

        public bool LowQuality
        {
            get => _wkHtmlToPdfParameters.LowQuality.IsEnabled();
            set => _wkHtmlToPdfParameters.LowQuality.Enabled = value;
        }

        public bool Grayscale
        {
            get => _wkHtmlToPdfParameters.Grayscale.IsEnabled();
            set => _wkHtmlToPdfParameters.Grayscale.Enabled = value;
        }

        public float Zoom
        {
            get => _wkHtmlToPdfParameters.ZoomGlobal.Value;
            set => _wkHtmlToPdfParameters.ZoomGlobal.Value = value;
        }

        public PageMargins Margins
        {
            get => _wkHtmlToPdfParameters.PageMargins.PageMargins;
            set => _wkHtmlToPdfParameters.PageMargins.PageMargins = value;
        }

        public float? PageWidth
        {
            get => _wkHtmlToPdfParameters.PageWidth.Value;
            set => _wkHtmlToPdfParameters.PageWidth.Value = value;
        }

        public float? PageHeight
        {
            get => _wkHtmlToPdfParameters.PageHeight.Value;
            set => _wkHtmlToPdfParameters.PageHeight.Value = value;
        }

        public bool GenerateToc
        {
            get => _wkHtmlToPdfParameters.GenerateTocParameter.IsEnabled();
            set => _wkHtmlToPdfParameters.GenerateTocParameter.Enabled = true;
        }

        public string TocHeaderText
        {
            get => _wkHtmlToPdfParameters.GenerateTocParameter.TocHeaderText.Value;
            set
            {
                _wkHtmlToPdfParameters.GenerateTocParameter.TocHeaderText.Enabled = value != null;
                _wkHtmlToPdfParameters.GenerateTocParameter.TocHeaderText.Value = value;
            }
        }

        public string CustomWkHtmlArgs
        {
            get => _wkHtmlToPdfParameters.CustomWkHtmlArgsParameter.Args;
            set
            {
                _wkHtmlToPdfParameters.CustomWkHtmlArgsParameter.Enabled = value != null;
                _wkHtmlToPdfParameters.CustomWkHtmlArgsParameter.Args = value;
            }
        }

        public string CustomWkHtmlPageArgs
        {
            get => _wkHtmlToPdfParameters.CustomWkHtmlPageArgsGlobal.Args;
            set
            {
                _wkHtmlToPdfParameters.CustomWkHtmlPageArgsGlobal.Enabled = value != null;
                _wkHtmlToPdfParameters.CustomWkHtmlPageArgsGlobal.Args = value;
            }
        }

        public string CustomWkHtmlCoverArgs
        {
            get => _wkHtmlToPdfParameters.CustomWkHtmlCoverArgs.Args;
            set
            {
                _wkHtmlToPdfParameters.CustomWkHtmlCoverArgs.Enabled = value != null;
                _wkHtmlToPdfParameters.CustomWkHtmlCoverArgs.Args = value;
            }
        }

        public string CustomWkHtmlTocArgs
        {
            get => _wkHtmlToPdfParameters.GenerateTocParameter.CustomWkHtmlTocArgs.Args;
            set
            {
                _wkHtmlToPdfParameters.GenerateTocParameter.CustomWkHtmlTocArgs.Enabled = value != null;
                _wkHtmlToPdfParameters.GenerateTocParameter.CustomWkHtmlTocArgs.Args = value;
            }
        }

        public string PageHeaderHtml
        {
            get => _wkHtmlToPdfParameters.PageHeaderHtml.PageHeaderHtml;
            set
            {
                _wkHtmlToPdfParameters.PageHeaderHtml.Enabled = value != null;
                _wkHtmlToPdfParameters.PageHeaderHtml.PageHeaderHtml = value;
            }
        }

        public string PageFooterHtml
        {
            get => _wkHtmlToPdfParameters.PageFooterHtml.PageFooterHtml;
            set
            {
                _wkHtmlToPdfParameters.PageFooterHtml.Enabled = value != null;
                _wkHtmlToPdfParameters.PageFooterHtml.PageFooterHtml = value;
            }
        }

        public bool Quiet
        {
            get => _wkHtmlToPdfParameters.Quiet.IsEnabled();
            set => _wkHtmlToPdfParameters.Quiet.Enabled = value;
        }
    }
}
