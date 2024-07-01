/*------------------------------------------------------------------------------
 * File    : OutExe
 * Time    : 2022/4/30 18:37:59
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[OutExe]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SrtGeo;

namespace ESAOSMSamplesCSA
{
    class OutExe
    {
        public static void t()
        {

        }


        public static void ShapeToPoint(string[] args)
        {

            ParseArgs p = new ParseArgs("srt_shptopoint"
                , "Line and surface shape files can be converted to point files. Currently, only the conversion of polyline points to points is supported."
                , "(C)Copyright 2022, ZhengHan. All rights reserved.");

            p.AddRequired("in_shp_file", "input shape file, *.shp");
            p.AddRequired("out_shp_file", "output shape file, *.shp default:_tp[n].shp");
            p.AddOptional("--help", 0, "help info");
            p.AddOptional("--debug", 0, "whether to print debug information");

            if (args.Length == 0)
            {
                Console.WriteLine(p.UsageInfo());
                return;
            }

            p.Fit(args);
            if (p.OptionalParams["--help"].IsStart)
            {
                Console.WriteLine(p.UsageInfo());
                return;
            }

            if (p.OptionalParams["--debug"].IsStart)
            {
                DebugInfo.IsDebug = true;
                Console.WriteLine("Debug Info:");
            }

            string in_shp_file = "";
            if (p.IsFileCorrect("in_shp_file", ".shp", ref in_shp_file))
            {
                string out_shp_file = Path.Combine(Path.GetDirectoryName(in_shp_file), Path.GetFileNameWithoutExtension(in_shp_file) + "_tp.shp");
                p.IsOutFile("out_shp_file", ".shp", ref out_shp_file);
                try
                {
                    ShapeInfo shapeInfo = new ShapeInfo(in_shp_file);
                    ShapeInfo shapeInfo1 = shapeInfo.ToPoint();
                    shapeInfo1.SaveToShapeFile(out_shp_file);
                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.Replace("\n", "    \n"));
                }
            }
            else
            {
                Console.WriteLine("Error: in shape file " + in_shp_file);
                Console.WriteLine(p.UsageInfo());
            }
        }

        public static void ShapeUniformSpace(string[] args)
        {
            ParseArgs p = new ParseArgs("srt_shpufs"
                , "Line and surface shape files can be converted to point files. Currently, only the conversion of polyline points to points is supported."
                , "(C)Copyright 2022, ZhengHan. All rights reserved.");

            p.AddRequired("in_shp_file", "input shape file, *.shp");
            p.AddRequired("out_shp_file", "output shape file, *.shp default:_tp[n].shp");
            p.AddOptional("--help", 0, "help info");
            p.AddOptional("--debug", 0, "whether to print debug information");

            if (args.Length == 0)
            {
                Console.WriteLine(p.UsageInfo());
                return;
            }
        }



    }
}
