using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary;
using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator.InputOutput
{
    public class OutputFileProvider : IOutputVisitor
    {
        private TempFileManager _tempFileManager;

        public OutputFileProvider(TempFileManager tempFileManager)
        {
            _tempFileManager = tempFileManager;
        }

        public OutputVisitorResult Visit(OutputFile outputFile)
        {
            var result = new OutputVisitorResult() { OutputFile = outputFile.OutputFileName };
            return result;
        }

        public OutputVisitorResult Visit(OutputStream outputStream)
        {
            var fileName = _tempFileManager.CreateTempFile(content: null);
            return new OutputVisitorResult() { OutputFile = fileName };
        }

        public OutputVisitorResult Visit(OutputByteArray outputByteArray)
        {
            var fileName = _tempFileManager.CreateTempFile(content: null);
            return new OutputVisitorResult() { OutputFile = fileName };
        }
    }
}
