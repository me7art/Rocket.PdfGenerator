using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper
{
    public class AssembleFileExtractorException : Exception
    {
        public AssembleFileExtractorException() : base()
        {

        }

        public AssembleFileExtractorException(string message) : base(message)
        {

        }
    }

    public class AHFileInfo
    {
        public string FileName;
        public string TagetFileName;
        public string TargetDirectory;
        public string ArchiveExt;
        public string AssemblyName; 
    }

    public class FileMapInfo
    {
        public string Version;
        public string Platform;
        public List<AHFileInfo> FileNames = new List<AHFileInfo>();
    }

    public class FileMapInfoHolder
    {
        public List<FileMapInfo> FileList = new List<FileMapInfo>();
    }

    public class FileMapInfoManager
    {
        FileMapInfoHolder _fileMapInfo;

        public FileMapInfoManager(FileMapInfoHolder fileMapInfo)
        {
            _fileMapInfo = fileMapInfo;
        }

        public void AddFile(string platform, string targetDirectory, string targetFileName, string archiveExt, string fileName, string version, string assemblyName)
        {
            var item = _fileMapInfo.FileList.FirstOrDefault(x => x.Platform == platform && x.Version == version);
            if (item == null)
            {
                item = new FileMapInfo() { Platform = platform, Version = version };
                _fileMapInfo.FileList.Add(item);
            }

            item.FileNames.Add(new AHFileInfo() { ArchiveExt = archiveExt, FileName = fileName, TagetFileName = targetFileName, TargetDirectory = targetDirectory, AssemblyName = assemblyName });
        }

        public bool IsVersionExist(string platform, string version)
        {
            return _fileMapInfo.FileList.Any(x => x.Platform == platform && x.Version == version);
        }

        public FileMapInfo GetFiles(string platform, string version)
        {
            return _fileMapInfo.FileList.FirstOrDefault(x => x.Platform == platform && x.Version == version);
        }
    }


    public class AssemblyNameParser
    {
        const string DIRECTORY_MARKER = "_DIR_";
        const string PLATFORM_WIN32 = "win32";
        const string PLATFORM_WIN64 = "win64";

        readonly string ASSEMBLE_STRING;
        readonly string FILE_HOLDER_ASSEMBLE;

        public bool IsParsed = false;

        // TODO?: raise exception on access to field without "parsing"

        public string SystemPlatform = string.Empty;
        public string TargetDirectory= string.Empty;
        public string TargetFileName = string.Empty;
        public string ArchiveExt     = string.Empty;
        public string FileName       = string.Empty;
        public string Version        = string.Empty;
        public string AssemblyName   = string.Empty;

        public AssemblyNameParser(string assemblyString, string fileHolderAssemble)
        {
            ASSEMBLE_STRING = assemblyString;
            FILE_HOLDER_ASSEMBLE = fileHolderAssemble;
        }

        public void Parse()
        {
            var nameRight = ASSEMBLE_STRING.Substring(FILE_HOLDER_ASSEMBLE.Length + 1); // we must have "." on first char after Assemble name
            var nameRightArray = nameRight.Split('.').ToList();
            if (nameRightArray.Count() < 2)
            {
                throw new AssembleFileExtractorException($"Unknown format for Assembly file storage: \"{nameRight}\". Can't extract Version and Platform information.");
            }
            var version = nameRightArray[0]?.TrimStart('_')?.Replace('_', '.');
            var platform = nameRightArray[1]?.ToLower();
            var archiveExt = nameRightArray.Last();

            var directoriesSrc = nameRightArray
                .Where(x => x.Contains(DIRECTORY_MARKER));                

            var fileName = string.Join(".", nameRightArray.Skip(2).Except(directoriesSrc));
            var TargetFileName = fileName.Substring(0, fileName.Length - archiveExt.Length - 1);

            var directories = directoriesSrc.Select(x => x.Replace(DIRECTORY_MARKER, string.Empty)).ToArray();

            var directoryPath = Path.Combine(directories);

            this.IsParsed       = true;
            this.SystemPlatform = platform;
            this.TargetFileName = TargetFileName;
            this.ArchiveExt     = archiveExt;
            this.TargetDirectory= directoryPath;
            this.FileName       = fileName;
            this.Version        = version;
            this.AssemblyName   = ASSEMBLE_STRING;
        }
    }

    public class AssemblyFileExtractor
    {
        private static object _lockObject = new object();

        readonly string FILE_HOLDER_ASSEMBLE;

        private FileMapInfoHolder  _fileMapInfo;
        private FileMapInfoManager _fileMapInfoManager;

        public AssemblyFileExtractor(string name)
        {
            FILE_HOLDER_ASSEMBLE = name;

            _fileMapInfo = new FileMapInfoHolder();
            _fileMapInfoManager = new FileMapInfoManager(_fileMapInfo);
        }

        public void InitFileMap()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
            string str = FILE_HOLDER_ASSEMBLE; 
            foreach (string recourceName in manifestResourceNames)
            {
                if (recourceName.StartsWith(str))
                {
                    AssemblyNameParser p = new AssemblyNameParser(recourceName, FILE_HOLDER_ASSEMBLE);
                    p.Parse();

                    _fileMapInfoManager
                        .AddFile(
                            platform        : p.SystemPlatform, 
                            targetDirectory : p.TargetDirectory, 
                            targetFileName  : p.TargetFileName, 
                            archiveExt      : p.ArchiveExt, 
                            fileName        : p.FileName, 
                            version         : p.Version,
                            assemblyName    : p.AssemblyName
                            );
                }
            }
        }

        private void ExtractFiles(string platform, string version, string targetDirectory)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            if (!_fileMapInfoManager.IsVersionExist(platform, version))
            {
                throw new AssembleFileExtractorException(message: $"Version '{platform}/{version}' not found in assembly");
            }

            var fileInfo = _fileMapInfoManager.GetFiles(platform, version);

            foreach (var file in fileInfo.FileNames)
            {
                var directory = Path.Combine(targetDirectory, file.TargetDirectory);
                var path = Path.Combine(directory, file.TagetFileName);

                lock (_lockObject)
                {
                    if (File.Exists(path))
                    {
                        var a = File.GetLastWriteTime(path);
                        var b = File.GetLastWriteTime(executingAssembly.Location);
                        if (File.GetLastWriteTime(path) > File.GetLastWriteTime(executingAssembly.Location))
                            continue;
                    }

                    if (Directory.Exists(directory) == false)
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (GZipStream gzipStream = new GZipStream(executingAssembly.GetManifestResourceStream(file.AssemblyName), CompressionMode.Decompress, false))
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            byte[] buffer = new byte[65536];
                            int count;
                            while ((count = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                                fileStream.Write(buffer, 0, count);
                        }
                    }
                }
            }
        }

        public void ExtractFiles(IAssemblyVersion version, string targetDirectory)
        {
            ExtractFiles(platform: version.Platform?.ToLower(), version: version.AssembleVersion, targetDirectory: targetDirectory);
        }
    }
}
