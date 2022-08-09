using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Linq;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.HelpersLibrary
{
    public class TempFileManager
    {
        private readonly string _filePrefix;
        private readonly string _tempPath;
        private List<string> _tempFilesList = new List<string>();

        public TempFileManager(string tempFilesDirectory, string filePrefix = "")
        {
            _tempPath = tempFilesDirectory ?? Path.GetTempPath();
            _filePrefix = filePrefix;
        }

        private void CreateDirectory()
        {
            if (Directory.Exists(_tempPath) == false)
            {
                Directory.CreateDirectory(_tempPath);
            }
        }

        public string CreateTempFile(string content)
        {
            CreateDirectory();

            string path = Path.Combine(_tempPath, "pdf-temp-" + Path.GetRandomFileName() + ".html");
            if (content != null)
            {
                File.WriteAllBytes(path, Encoding.UTF8.GetBytes(content));
            }
            _tempFilesList.Add(path);
            return path;
        }

        public void DeleteTempFiles()
        {
            foreach (string filePath in _tempFilesList)
            {
                if (filePath == null)
                    continue;
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch
                {
                    // Log message
                }
            }

            _tempFilesList.Clear();
        }
    }
}
