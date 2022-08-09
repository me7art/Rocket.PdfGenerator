using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator
{
    public partial class HtmlToPdfConverter
    {
        public byte[] GeneratePdf(string htmlContent)
        {
            return GeneratePdf(htmlContent, coverHtml: null);
        }

        public byte[] GeneratePdf(string htmlContent, string coverHtml)
        {
            var inputData = new InputHtmlString() { HtmlString = htmlContent };
            var outputData = new OutputByteArray();
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml: coverHtml, parameterManager: this);

            return outputData.ByteArr;
        }

        public void GeneratePdf(string htmlContent, string coverHtml, Stream output)
        {
            var inputData = new InputHtmlString() { HtmlString = htmlContent };
            var outputData = new OutputStream() { Stream = output };
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml, parameterManager: this);
        }

        public void GeneratePdf(string htmlContent, string coverHtml, string outputPdfFilePath)
        {
            var inputData = new InputHtmlString() { HtmlString = htmlContent };
            var outputData = new OutputFile() { OutputFileName = outputPdfFilePath };
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml, this);
        }

    }
}
