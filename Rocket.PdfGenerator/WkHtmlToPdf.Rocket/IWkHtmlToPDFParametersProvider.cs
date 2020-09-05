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

using Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket
{
    public interface IWkHtmlToPDFParametersProvider
    {
        WKHtmlToPDFParameters GetParameters();

    }
}
