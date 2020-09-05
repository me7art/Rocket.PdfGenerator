using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.ProcessManager.ConsoleParameters;

namespace Rocket.PdfGenerator.WkHtmlToPdf.Rocket.Parameters
{
    public class PageMarginsParameter : IConsoleParameterHolder
    {
        MarginTopParameter _marginTop = new MarginTopParameter();
        MarginBottomParameter _marginBottom = new MarginBottomParameter();
        MarginLeftParameter _marginLeft = new MarginLeftParameter();
        MarginRightParameter _marginRight = new MarginRightParameter();

        public bool IsEnabled() => _pageMargins != null;

        private PageMargins _pageMargins = null;
        public PageMargins PageMargins
        {
            get => _pageMargins;
            set
            {
                _pageMargins = value;

                _marginTop.Enabled = PageMargins.Top.HasValue;
                _marginTop.Value = PageMargins.Top ?? 0;

                _marginBottom.Enabled = PageMargins.Bottom.HasValue;
                _marginBottom.Value = PageMargins.Bottom ?? 0;

                _marginLeft.Enabled = PageMargins.Left.HasValue;
                _marginLeft.Value = PageMargins.Left ?? 0;

                _marginRight.Enabled = PageMargins.Right.HasValue;
                _marginRight.Value = PageMargins.Right ?? 0;
            }
        }

        public List<IConsoleParameterMapper> GetConsoleParameters()
        {
            return new List<IConsoleParameterMapper> { _marginTop, _marginBottom, _marginLeft, _marginRight };
        }

        public class MarginTopParameter : SimpleValueParameter<float>
        {
            public MarginTopParameter() : base("-T")
            {
            }
        }

        public class MarginBottomParameter : SimpleValueParameter<float>
        {
            public MarginBottomParameter() : base("-B")
            {
            }
        }

        public class MarginLeftParameter : SimpleValueParameter<float>
        {
            public MarginLeftParameter() : base("-L")
            {
            }
        }

        public class MarginRightParameter : SimpleValueParameter<float>
        {
            public MarginRightParameter() : base("-R")
            {
            }
        }
    }
}
