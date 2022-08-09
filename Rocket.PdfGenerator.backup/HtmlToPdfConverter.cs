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

using Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper;
using Rocket.PdfGenerator.HelpersLibrary.ProcessManager;
using Rocket.PdfGenerator.HelpersLibrary;
using Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters;
using Rocket.PdfGenerator.WkHtmlToPdf.Rocket;

using Rocket.PdfGenerator.InputOutput;
using Rocket.PdfGenerator.InputOutput.Formats;

namespace Rocket.PdfGenerator
{

    public partial class HtmlToPdfConverter : RocketPDFParameterManager
    {
        private static string[] _ignoreWkHtmlToPdfErrLines = new string[]
        {
            "Exit with code 1 due to network error: ContentNotFoundError",
            "QFont::setPixelSize: Pixel size <= 0",
            "Exit with code 1 due to network error: ProtocolUnknownError",
            "Exit with code 1 due to network error: HostNotFoundError",
            "Exit with code 1 due to network error: ContentOperationNotPermittedError",
            "Exit with code 1 due to network error: UnknownContentError"
        };

        private ProcessHelper _processHelper = new ProcessHelper();

        public string PdfToolDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public string WkHtmlToPdfExeName { get; set; } = "wkhtmltopdf.exe";
        public string TempFilesDirectory { get; set; } = null;
        public bool UseStandaloneWkHtmlToPdfTool { get; set; } = false; 

        public event EventHandler<DataReceivedEventArgs> LogReceived;

        private IAssemblyVersion _wkHtmlToPdfVersion;

        private IAssemblyVersion _defaultAssemblyVersion = WKHtmlToPDFVersions.WIN64_0_12_6_mxecross; //.Win32_0_12_4_0;

        public HtmlToPdfConverter()
        {
            _wkHtmlToPdfVersion = _defaultAssemblyVersion;
        }

        public HtmlToPdfConverter(IAssemblyVersion wkHtmlToPdfVerstion = null)
        {
            _wkHtmlToPdfVersion = wkHtmlToPdfVerstion ?? _defaultAssemblyVersion; 
        }

        private string GetToolExePath()
        {
            string path = Path.Combine(PdfToolDirectory, WkHtmlToPdfExeName);
            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException($"Cannot find Pdf Tool: {path}");
            }
            return path;
        }

        private void ExtractWkHtmlToPdfFiles()
        {
            AssemblyFileExtractor assemblyFileExtractor = new AssemblyFileExtractor(name: "Rocket.PdfGenerator.WkHtmlToPdf");
            assemblyFileExtractor.InitFileMap();
            assemblyFileExtractor.ExtractFiles(_wkHtmlToPdfVersion, targetDirectory: PdfToolDirectory);
        }

        private bool _batchMode = false;

        public void BeginBatch()
        {
            if (_batchMode)
            {
                throw new InvalidOperationException("HtmlToPdfConverter is already in the batch mode.");
            }
            _batchMode = true;

            if (UseStandaloneWkHtmlToPdfTool == false)
            {
                ExtractWkHtmlToPdfFiles();
            }
        }

        public void EndBatch()
        {
            if (_batchMode != true)
            {
                throw new InvalidOperationException("HtmlToPdfConverter is not in the batch mode.");
            }

            _batchMode = false;

            if (_processHelper._process == null)
                return;
            if (!_processHelper._process.HasExited)
            {
                this._processHelper._process.StandardInput.Close();
                this._processHelper._process.WaitForExit();
                this._processHelper._process.Close();
            }
            this._processHelper._process = null;
        }

        private void GeneratePdfInternal(IInputData[] inputFiles, IOutputData output, string coverHtml, RocketPDFParameterManager parameterManager)
        {
            TempFileManager tempFileManager = new TempFileManager(tempFilesDirectory: TempFilesDirectory, filePrefix: "pdf-temp-");
            
            OutputFileProvider outputFileProvider = new OutputFileProvider(tempFileManager);
        
            var outputPdfFilePath = output.Accept(outputFileProvider).OutputFile;
            ResultProvider resultProvider = new ResultProvider(outputPdfFilePath);


            var parameters = parameterManager.GetParameters();
            var pdfParametersInternal = GenerateInternalParameters(inputFiles, outputPdfFilePath, coverHtml, parameters, tempFileManager);

            try
            {
                if (_batchMode)
                {
                    InvokeWkHtmlToPdfInInBatch(parameters, pdfParametersInternal);
                }
                else
                {
                    InvokeWkHtmlToPdf(parameters, pdfParametersInternal);
                }

                output.Accept(resultProvider);
            }
            catch (Exception ex)
            {
                if (!_batchMode)
                {
                    _processHelper.EnsureWkHtmlProcessStopped();
                }
                throw new Exception("Can't generate PDF: " + ex.Message, ex);
            }
            finally
            {
                tempFileManager.DeleteTempFiles();
            }

        }

        private WkHtmlToPdfParametersInternal GenerateInternalParameters(
            IInputData[] inputFiles,
            string outputPdfFilePath, 
            string coverHtml,
            WKHtmlToPDFParameters parameters, 
            TempFileManager tempFileManager)
        {
            WkHtmlToPdfParametersInternal pdfParametersInternal = new WkHtmlToPdfParametersInternal()
            {
                OutputFileParameter = new OutputFileParameter(value: outputPdfFilePath),
                
                CoverFileParameter = string.IsNullOrWhiteSpace(coverHtml) == false ? new CoverFilePathParameter(value: tempFileManager.CreateTempFile(coverHtml), customWkHtmlCoverArgs: parameters.CustomWkHtmlCoverArgs) : null,
                HeaderFilePathParameter = parameters.PageHeaderHtml?.IsEnabled() == true ? new HeaderFilePathParameter(value: tempFileManager.CreateTempFile(parameters.PageHeaderHtml.GetContent())) : null,
                FooterFilePathParameter = parameters.PageFooterHtml?.IsEnabled() == true ? new FooterFilePathParameter(value: tempFileManager.CreateTempFile(parameters.PageFooterHtml.GetContent())) : null
            };

            InputFileProvider fileProvider = new InputFileProvider(tempFileManager);

            foreach (var inputData in inputFiles)
            {
                var htmlFile = inputData.Accept(fileProvider);

                var headerParam = new PageHeaderHtmlParameter()
                {
                    Enabled = string.IsNullOrWhiteSpace(inputData.PageHeaderHtml) == false,
                    PageHeaderHtml = inputData.PageHeaderHtml
                };
                var footerParam = new PageFooterHtmlParameter()
                {
                    Enabled = string.IsNullOrWhiteSpace(inputData.PageFooterHtml) == false,
                    PageFooterHtml = inputData.PageFooterHtml
                };

                var headerFilePathParameter = headerParam.IsEnabled() == true ? new HeaderFilePathParameter(value: tempFileManager.CreateTempFile(headerParam.GetContent())) : null;
                var footerFilePathParameter = footerParam.IsEnabled() == true ? new FooterFilePathParameter(value: tempFileManager.CreateTempFile(footerParam.GetContent())) : null;
                var zoom = 
                    inputData.Zoom.HasValue 
                    ? new ZoomParameter() { Value = inputData.Zoom.Value } 
                    : parameters.ZoomGlobal;

                pdfParametersInternal.InputFileParameters.Add(
                    new InputFileParameter(
                        value: htmlFile.File, 
                        customWkHtmlPageArgs: parameters.CustomWkHtmlPageArgsGlobal, 
                        headerFilePath: headerFilePathParameter,
                        footerFilePath: footerFilePathParameter,
                        zoom: zoom));
            }

            return pdfParametersInternal;
        }


        private void InvokeWkHtmlToPdfInInBatch(WKHtmlToPDFParameters parameters, WkHtmlToPdfParametersInternal parametersInternal)
        {
            string lastErrorLine = string.Empty;

            WKHtmlToPDFParameterBuilder wkHtmlToPDFParameterBuilder = new WKHtmlToPDFParameterBuilder(parameters, parametersInternal);
            var args = wkHtmlToPDFParameterBuilder.BuildString();

            if (_processHelper._process == null || _processHelper._process.HasExited)
            {
                _processHelper._process = Process.Start(new ProcessStartInfo(GetToolExePath(), "--read-args-from-stdin")
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(this.PdfToolDirectory),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = true
                });

                _processHelper._process.BeginErrorReadLine();
            }

            _processHelper._process.ErrorDataReceived += _wkHtmlToPdfProcess_ErrorDataReceived;

            try
            {
                var outputFile = parametersInternal.OutputFileParameter.Value;

                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                _processHelper._process.StandardInput.WriteLine(args.Replace('\\', '/'));
                bool flag = true;
                while (flag)
                {
                    Thread.Sleep(25);
                    if (_processHelper._process.HasExited)
                        flag = false;
                    if (File.Exists(outputFile))
                    {
                        flag = false;
                        this.WaitForFile(outputFile);
                    }
                }

                if (!_processHelper._process.HasExited) 
                {
                    return;
                }

                this.CheckExitCode(_processHelper._process.ExitCode, lastErrorLine, File.Exists(outputFile));
            }
            finally
            {
                if (_processHelper._process != null && !_processHelper._process.HasExited)
                {
                    _processHelper._process.ErrorDataReceived -= _wkHtmlToPdfProcess_ErrorDataReceived;
                }
                else
                {
                    _processHelper.EnsureWkHtmlProcessStopped();
                }
            }

            void _wkHtmlToPdfProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e) // o, args
            {
                if (e.Data == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(e.Data))
                {
                    lastErrorLine = e.Data;
                }
                if (this.LogReceived == null)
                {
                    return;
                }

                this.LogReceived((object)this, e);
            }
        }

        private void WaitForFile(string fullPath)
        {
            const double DEFAULT_BATCH_EXECUTION_TIMEOUT = 60000.0;
            const double DEFAULT_FAST_REQUEST_WINDOW = 1000.0;

            const int THREAD_SLEEP_TIME_FAST_MILLISECONDS = 50;
            const int THREAD_SLLEP_TIME_SLOW_MILLISECONDS = 100;

            double maxExecutionTime = _processHelper.ExecutionTimeout?.TotalMilliseconds ?? DEFAULT_BATCH_EXECUTION_TIMEOUT;
            if (maxExecutionTime == 0)
            {
                maxExecutionTime = DEFAULT_BATCH_EXECUTION_TIMEOUT;
            }

            var startedAt = DateTime.Now.Ticks;
            var endAt = DateTime.Now.AddMilliseconds(maxExecutionTime).Ticks;

            do
            {
                try
                {
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 100))
                    {
                        fileStream.ReadByte();
                        break;
                    }
                }
                catch //(Exception ex)
                {
                    var timeToSleep =
                        DateTime.Now.Ticks - startedAt < DEFAULT_FAST_REQUEST_WINDOW
                        ? THREAD_SLEEP_TIME_FAST_MILLISECONDS
                        : THREAD_SLLEP_TIME_SLOW_MILLISECONDS;

                    Thread.Sleep(timeToSleep);
                }
            } while (endAt - DateTime.Now.Ticks > 0);
           

            if (maxExecutionTime != 0.0 || _processHelper._process == null || _processHelper._process.HasExited)
            {
                return;
            }
            _processHelper._process.StandardInput.Close();
            _processHelper._process.WaitForExit();
        }


        private void InvokeWkHtmlToPdf(WKHtmlToPDFParameters parameters, WkHtmlToPdfParametersInternal parametersInternal)
        {
            if (_processHelper._process != null)
            {
                throw new InvalidOperationException("WkHtmlToPdf process is already started");
            }

            if (UseStandaloneWkHtmlToPdfTool == false)
            {
                ExtractWkHtmlToPdfFiles();
            }

            string lastErrorLine = string.Empty;

            WKHtmlToPDFParameterBuilder wkHtmlToPDFParameterBuilder = new WKHtmlToPDFParameterBuilder(parameters, parametersInternal);
            var args = wkHtmlToPDFParameterBuilder.BuildString();

            try
            {
                _processHelper._process = Process.Start(new ProcessStartInfo(this.GetToolExePath(), args)
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(this.PdfToolDirectory),
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = true
                });

                _processHelper._process.ErrorDataReceived += _wkHtmlToPdfProcess_ErrorDataReceived;
                _processHelper._process.BeginErrorReadLine();
                _processHelper.WaitWkHtmlProcessForExit();

                long num = 0;
                var outputFileName = parametersInternal.OutputFileParameter.Value;
                if (File.Exists(outputFileName))
                {
                    num = new FileInfo(outputFileName).Length;
                }
                this.CheckExitCode(_processHelper._process.ExitCode, lastErrorLine, num > 0L);
            }
            finally
            {
                _processHelper.EnsureWkHtmlProcessStopped();
            }

            void _wkHtmlToPdfProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e) 
            {
                if (e.Data == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(e.Data))
                {
                    lastErrorLine = e.Data;
                }
                if (this.LogReceived == null)
                {
                    return;
                }

                this.LogReceived((object)this, e);
            }


        }

        private void CheckExitCode(int exitCode, string lastErrLine, bool outputNotEmpty)
        {
            if (exitCode == 0)
            {
                return;
            }

            if (exitCode != 1 || Array.IndexOf<string>(_ignoreWkHtmlToPdfErrLines, lastErrLine.Trim()) < 0 || !outputNotEmpty)
            {
                throw new Exception($"{exitCode} {lastErrLine}");

                //TODO:throw new WkHtmlToPdfException
            }
        }

    }
}
