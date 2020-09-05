using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rocket.PdfGenerator.HelpersLibrary.AssemblyHelper;

namespace Rocket.PdfGenerator
{
    public class WKHtmlToPdf_WIN32_0_12_2_4_msvc2013 : IAssemblyVersion
    {
        public string Platform { get; } = "Win32";
        public string AssembleVersion { get; } = "0.12.2.4.msvc2013";
    }
    public class WKHtmlToPdf_WIN64_0_12_2_4_msvc2013 : IAssemblyVersion
    {
        public string Platform { get; } = "Win64";
        public string AssembleVersion { get; } = "0.12.2.4.msvc2013";
    }

    public class WKHtmlToPdf_WIN32_0_12_4_mingww64 : IAssemblyVersion
    {
        public string Platform { get; } = "Win32";
        public string AssembleVersion { get; } = "0.12.4.mingww64";
    }
    public class WKHtmlToPdf_WIN64_0_12_4_mingww64 : IAssemblyVersion
    {
        public string Platform { get; } = "Win64";
        public string AssembleVersion { get; } = "0.12.4.mingww64";
    }

    public class WKHtmlToPdf_WIN32_0_12_6_mxecross : IAssemblyVersion
    {
        public string Platform { get; } = "Win32";
        public string AssembleVersion { get; } = "0.12.6.1.mxecross";
    }
    public class WKHtmlToPdf_WIN64_0_12_6_mxecross : IAssemblyVersion
    {
        public string Platform { get; } = "Win64";
        public string AssembleVersion { get; } = "0.12.6.1.mxecross";
    }

    static public class WKHtmlToPDFVersions
    {
        static public WKHtmlToPdf_WIN32_0_12_2_4_msvc2013 WIN32_0_12_2_4_msvc2013 = new WKHtmlToPdf_WIN32_0_12_2_4_msvc2013();
        static public WKHtmlToPdf_WIN64_0_12_2_4_msvc2013 WIN64_0_12_2_4_msvc2013 = new WKHtmlToPdf_WIN64_0_12_2_4_msvc2013();

        static public WKHtmlToPdf_WIN32_0_12_4_mingww64 WIN32_0_12_4_mingw = new WKHtmlToPdf_WIN32_0_12_4_mingww64();
        static public WKHtmlToPdf_WIN64_0_12_4_mingww64 WIN64_0_12_4_mingw = new WKHtmlToPdf_WIN64_0_12_4_mingww64();

        static public WKHtmlToPdf_WIN32_0_12_6_mxecross WIN32_0_12_6_mxecross = new WKHtmlToPdf_WIN32_0_12_6_mxecross();
        static public WKHtmlToPdf_WIN64_0_12_6_mxecross WIN64_0_12_6_mxecross = new WKHtmlToPdf_WIN64_0_12_6_mxecross();
    }
}
