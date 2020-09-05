using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator.InputOutput
{
    public class ResultProvider : IOutputVisitor
    {
        private string _resultFileName;

        public ResultProvider(string resultFileName)
        {
            _resultFileName = resultFileName;
        }

        public OutputVisitorResult Visit(OutputFile outputFile)
        {
            return null;
        }

        public OutputVisitorResult Visit(OutputStream outputStream)
        {
            using (FileStream fileStream = new FileStream(_resultFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                this.CopyStream((Stream)fileStream, outputStream.Stream, 65536);

            return null;
        }

        public OutputVisitorResult Visit(OutputByteArray outputByteArray)
        {
            using (var outputStream = new MemoryStream())
            {
                using (FileStream fileStream = new FileStream(_resultFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    this.CopyStream((Stream)fileStream, outputStream, 65536);

                outputByteArray.ByteArr = outputStream.ToArray();
            }

            return null;
        }

        private void CopyStream(Stream inputStream, Stream outputStream, int bufSize)
        {
            byte[] buffer = new byte[bufSize];
            int count;
            while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, count);
            }
        }
    }
}
