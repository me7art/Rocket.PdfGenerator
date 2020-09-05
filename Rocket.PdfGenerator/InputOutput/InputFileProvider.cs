using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary;
using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator.InputOutput
{
    public class InputFileProvider : IInputVisitor
    {
        private TempFileManager _tempFileManager;

        public InputFileProvider(TempFileManager tempFileManager)
        {
            _tempFileManager = tempFileManager;
        }

        public VisitorResult Visit(InputFile inputFile)
        {
            var result = new VisitorResult() { File = inputFile.File };
            return result;
        }

        public VisitorResult Visit(InputHtmlString inputString)
        {
            var fileName = _tempFileManager.CreateTempFile(inputString.HtmlString);
            return new VisitorResult() { File = fileName };
        }
    }
}
