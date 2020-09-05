using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.PdfGenerator;



namespace Rocket.PdfGenerator.Tests
{
    [TestClass]
    public class GlobalOptionsTests
    {
        const string SOURCE = "https://lenta.ru";
        const string PDF_DIRECTORY = @"PDF\Options";

        public GlobalOptionsTests()
        {
            if (Directory.Exists(PDF_DIRECTORY) == false)
            {
                Directory.CreateDirectory(PDF_DIRECTORY);
            }
        }

        [TestMethod]
        public void PageSizeTest()
        {
            const string fileName = "PageSize.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();
            generator.Size = PageSize.A1;
            generator.GeneratePdfFromFile(SOURCE, null, Path.Combine(PDF_DIRECTORY, fileName));
        }

        [TestMethod]
        public void CustomArgsTest()
        {
            const string fileName = "CustomsArgs.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();
            generator.CustomWkHtmlArgs = " -g ";
            generator.GeneratePdfFromFile(SOURCE, null, Path.Combine(PDF_DIRECTORY, fileName));
        }

    }
}
