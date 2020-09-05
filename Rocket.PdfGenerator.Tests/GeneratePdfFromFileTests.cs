using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.PdfGenerator;


namespace Rocket.PdfGenerator.Tests
{
    [TestClass]
    public class GeneratePdfFromFileTests
    {
        readonly string HTML_CONTENT_COVER = $"HTML COVER STRING {DateTime.Now.ToString()}";
        const string HTML_FILE = @"..\..\..\TEST_HTML_FILES\file.html";
        const string HTML_LINK = @"https://lenta.ru";

        const string PDF_DIRECTORY = @"PDF\GeneratePdfFromFile";

        public GeneratePdfFromFileTests()
        {
            if (Directory.Exists(PDF_DIRECTORY) == false)
            {
                Directory.CreateDirectory(PDF_DIRECTORY);
            }
        }

        [TestMethod]
        public void GenerateByteArrayByString()
        {
            const string fileName = "GenerateByteArrayByString.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();
            byte[] pdfByteArray = generator.GeneratePdfFromFile(HTML_FILE, HTML_CONTENT_COVER);

            File.WriteAllBytes(Path.Combine(PDF_DIRECTORY, fileName), pdfByteArray);
        }

        [TestMethod]
        public void GenerateStreamByStringAndCover()
        {
            const string fileName = "GenerateStreamByStringAndCover.pdf";

            using (MemoryStream stream = new MemoryStream())
            {
                HtmlToPdfConverter generator = new HtmlToPdfConverter();
                generator.GeneratePdfFromFile(HTML_FILE, HTML_CONTENT_COVER, stream);

                File.WriteAllBytes(Path.Combine(PDF_DIRECTORY, fileName), stream.ToArray());
            }
        }

        [TestMethod]
        public void GenerateFileByStringAndCover()
        {
            const string fileName = "GenerateFileByStringAndCover.pdf";

            HtmlToPdfConverter generator = new HtmlToPdfConverter();
            generator.GeneratePdfFromFile(HTML_FILE, HTML_CONTENT_COVER, Path.Combine(PDF_DIRECTORY, fileName));
        }
    }
}
