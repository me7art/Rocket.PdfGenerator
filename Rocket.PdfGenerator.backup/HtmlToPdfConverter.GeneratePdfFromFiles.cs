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
        public void GeneratePdfFromFiles(string[] htmlFiles, string coverHtml, Stream output)
        {
            var inputData = MapInput(htmlFiles).ToArray();
            var outputData = new OutputStream() { Stream = output };
            GeneratePdfInternal( inputData, outputData, coverHtml, this);
        }

        public void GeneratePdfFromFiles(string[] htmlFiles, string coverHtml, string outputPdfFilePath)
        {
            var inputData = MapInput(htmlFiles).ToArray();
            var outputData = new OutputFile() { OutputFileName = outputPdfFilePath };
            GeneratePdfInternal(inputData, outputData, coverHtml, this);
        }

        public void GeneratePdfFromFiles(WkHtmlInput[] inputs, string coverHtml, string outputPdfFilePath)
        {
            var inputData = MapInput(inputs).ToArray();
            var outputData = new OutputFile() { OutputFileName = outputPdfFilePath };
            GeneratePdfInternal(inputData, outputData, coverHtml, this);
        }

        IEnumerable<IInputData> MapInput (string[] htmlFiles)
        {
            foreach (var htmlFile in htmlFiles)
            {
                yield return new InputFile() { File = htmlFile };
            }
        }

        IEnumerable<IInputData> MapInput(WkHtmlInput[] inputs)
        {
            foreach(var input in inputs)
            {
                yield return new InputFile()
                {
                    File = input.Input,
                    CustomWkHtmlPageArgs = input.CustomWkHtmlPageArgs,
                    PageHeaderHtml = input.PageHeaderHtml,
                    PageFooterHtml = input.PageFooterHtml,
                    Zoom = input.Zoom
                };
            }
        }
    }
}
