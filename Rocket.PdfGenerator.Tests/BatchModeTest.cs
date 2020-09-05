using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rocket.PdfGenerator.Tests
{
    [TestClass]
    public class BatchModeTest
    {
        const string PDF_DIRECTORY = @"PDF\BatchPdf";
        HtmlToPdfConverter _generator;

        public BatchModeTest()
        {
            if (Directory.Exists(PDF_DIRECTORY) == false)
            {
                Directory.CreateDirectory(PDF_DIRECTORY);
            }

            _generator = new HtmlToPdfConverter();
        }

        [TestMethod]
        public void GenerateInBatchModeTest()
        {
            _generator.BeginBatch();
            _generator.GeneratePdfFromFile("https://yandex.ru", null, Path.Combine(PDF_DIRECTORY, "yandex.pdf"));
            _generator.GeneratePdfFromFile("https://lenta.ru", null, Path.Combine(PDF_DIRECTORY, "lenta.pdf"));
            _generator.GeneratePdfFromFile("https://meduza.io", null, Path.Combine(PDF_DIRECTORY, "meduza.pdf"));
            _generator.EndBatch();

        }
    }
}
