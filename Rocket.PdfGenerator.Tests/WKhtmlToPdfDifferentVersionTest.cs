using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper;
using Rocket.PdfGenerator;

namespace Rocket.PdfGenerator.Tests
{
    [TestClass]
    public class WKhtmlToPdfDifferentVersionTest
    {
        const string pdfContent = "Testing pdf generation";

        const string PDF = @"PDF\Versions";
        Dictionary<string, IAssemblyVersion> _directoryAssembly = new Dictionary<string, IAssemblyVersion>
        {
            { "win32_0_12_2_4_msvc2013", WKHtmlToPDFVersions.WIN32_0_12_2_4_msvc2013 },
            { "win64_0_12_2_4_msvc2013", WKHtmlToPDFVersions.WIN64_0_12_2_4_msvc2013 },
            { "win32_0_12_4_mingw-w64", WKHtmlToPDFVersions.WIN32_0_12_4_mingw },
            { "win64_0_12_4_mingw-w64", WKHtmlToPDFVersions.WIN64_0_12_4_mingw },
            { "win32_0_12_6-1_mxe-cross", WKHtmlToPDFVersions.WIN32_0_12_6_mxecross },
            { "win64_0_12_6-1_mxe-cross", WKHtmlToPDFVersions.WIN64_0_12_6_mxecross },
        };

        [TestMethod]
        public void TestVersions()
        {
            if (Directory.Exists(PDF))
            {
                Directory.Delete(PDF, recursive: true);
            }

            foreach (var version in _directoryAssembly)
            {
                var pdfDir = Path.Combine(PDF, version.Key);
                Directory.CreateDirectory(pdfDir);

                var htmlToPdfConverter = new HtmlToPdfConverter(version.Value) { PdfToolDirectory = pdfDir };

                htmlToPdfConverter.GeneratePdf(pdfContent, null, $"{version.Key}.pdf");
            }
        }
    }
}
