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

namespace Rocket.PdfGenerator.HelpersLibrary.ProcessManager
{
    public class ProcessHelper
    {
        public Process _process = null;
        public TimeSpan? ExecutionTimeout { get; set; }

        public void WaitWkHtmlProcessForExit()
        {
            if (this.ExecutionTimeout.HasValue)
            {
                if (!this._process.WaitForExit((int)this.ExecutionTimeout.Value.TotalMilliseconds))
                {
                    this.EnsureWkHtmlProcessStopped();
                    //TODO: throw new WkHtmlToPdfException
                    throw new Exception(string.Format("Process exceeded execution timeout ({0}) and was aborted", (object)this.ExecutionTimeout));
                }
            }
            else
            {
                this._process.WaitForExit();
            }
        }

        public void EnsureWkHtmlProcessStopped()
        {
            if (this._process == null)
                return;

            if (!this._process.HasExited)
            {
                try
                {
                    this._process.Kill();
                    this._process.Close();
                    this._process = null;

                    // TODO: improve this! /^\
                }
                catch //(Exception ex)
                {
                }
            }
            else
            {
                this._process.Close();
                this._process = null;
            }
        }
    }
}
