using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class ZoomParameter : SimpleValueParameter<float>
    {
        public ZoomParameter() : base("--zoom")
        {
        }

        public override bool IsEnabled()
        {
            return base.Value != 1f;
        }
    }
}
