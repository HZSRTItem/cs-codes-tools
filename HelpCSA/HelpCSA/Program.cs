using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseArgs p = new ParseArgs(
                "srt_help"
                , "Open or get some help files"
                , "(C)Copyright 2022, ZhengHan. All rights reserved."
            );

            p.AddOptional("--help", 0, "help info");
            p.AddOptional("--gdal", 0, "gdal help html");
            p.Fit(args);

            if(p.OptionalParams["--help"].IsStart)
            {
                Console.WriteLine(p.UsageInfo());
            }
            else if (p.OptionalParams["--gdal"].IsStart)
            {
                System.Diagnostics.Process.Start(@"D:\SpecialProjects\ogr2ogrtest\html\index.html", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
            }
            else
            {
                Console.WriteLine(p.UsageInfo());
            }
        }
    }
}
