using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.PdfGenerator;

namespace Rocket.PdfGenerator.Tests
{
    [TestClass]
    public class GeneratePdfFromFilesTests
    {
        readonly string HTML_CONTENT_COVER = $"HTML COVER STRING {DateTime.Now.ToString()}";
        const string HTML_FILE = @"..\..\..\TEST_HTML_FILES\file.html";
        const string HTML_FILE2 = @"..\..\..\TEST_HTML_FILES\file2.html";
        const string HTML_LINK = @"https://lenta.ru";

        const string PDF_DIRECTORY = @"PDF\GeneratePdfFromFiles";

        public GeneratePdfFromFilesTests()
        {
            if (Directory.Exists(PDF_DIRECTORY) == false)
            {
                Directory.CreateDirectory(PDF_DIRECTORY);
            }
        }

        [TestMethod]
        public void GenerateStreamByStringAndCover()
        {
            const string fileName = "GenerateStreamByStringAndCover.pdf";

            using (MemoryStream stream = new MemoryStream())
            {
                HtmlToPdfConverter generator = new HtmlToPdfConverter();
                generator.GeneratePdfFromFiles(new string[] { HTML_FILE, HTML_FILE2 }, HTML_CONTENT_COVER, stream);

                File.WriteAllBytes(Path.Combine(PDF_DIRECTORY, fileName), stream.ToArray());
            }
        }

        [TestMethod]
        public void GenerateFileByStringAndCover()
        {
            const string fileName = "GenerateFileByStringAndCover.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();
            generator.GeneratePdfFromFiles(new string[] { HTML_FILE, HTML_FILE2 }, HTML_CONTENT_COVER, Path.Combine(PDF_DIRECTORY, fileName));
        }

        [TestMethod]
        public void GenerateFileByWkHtmlInputAndCover()
        {
            const string fileName = "GenerateFileByWkHtmlInputAndCover.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();

            WkHtmlInput[] input = new WkHtmlInput[]
            {
                new WkHtmlInput("https://lenta.ru") { PageFooterHtml = "lenta footer", PageHeaderHtml = "lenta header" },
                new WkHtmlInput("https://yandex.ru") { PageFooterHtml = "yandex footer", PageHeaderHtml = "yandex header" }
            };

            generator.GeneratePdfFromFiles(input, HTML_CONTENT_COVER, Path.Combine(PDF_DIRECTORY, fileName));
        }
    }
}
