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
        public byte[] GeneratePdfFromFile(string htmlFilePath, string coverHtml)
        {
            var inputData = new InputFile() { File = htmlFilePath };
            var outputData = new OutputByteArray();
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml, this);

            return outputData.ByteArr;
        }

        public void GeneratePdfFromFile(string htmlFilePath, string coverHtml, Stream output)
        {
            var inputData = new InputFile() { File = htmlFilePath };
            var outputData = new OutputStream() { Stream = output };
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml, this);
        }

        public void GeneratePdfFromFile(string htmlFilePath, string coverHtml, string outputPdfFilePath)
        {
            var inputData = new InputFile() { File = htmlFilePath };
            var outputData = new OutputFile() { OutputFileName = outputPdfFilePath };
            GeneratePdfInternal(new IInputData[] { inputData }, outputData, coverHtml, this);
        }
    }
}
