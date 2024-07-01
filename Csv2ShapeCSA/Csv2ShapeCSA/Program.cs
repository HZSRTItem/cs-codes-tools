/*------------------------------------------------------------------------------
 * File    : Program.cs
 * Time    : 2022-5-21 20:12:18
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Csv2ShapeCSA
{
    class Program
    {    /// <summary>
         /// 命令行运行
         /// </summary>
        class CmdRun
        {
            /// <summary>
            /// 命令行输出信息
            /// </summary>
            public static string OutInfo = "";
            /// <summary>
            /// 错误信息
            /// </summary>
            public static string ErrorInfo = "";

            /// <summary>
            /// 运行命令行信息
            /// </summary>
            /// <param name="command_line">命令</param>
            /// <returns>是否发生错误</returns>
            public static bool run(string command_line)
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
                p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
                p.Start(); // 启动程序
                string in_str = command_line;
                p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
                p.StandardInput.AutoFlush = true;
                OutInfo = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
                ErrorInfo = p.StandardError.ReadToEnd();
                p.WaitForExit(); // 等待程序执行完退出进程
                p.Close();
                int i = 0;
                int n = 0;
                for (; i < OutInfo.Length; i++)
                {

                    n += OutInfo[i] == '\n' ? 1 : 0;
                    if (n == 4)
                    {
                        break;
                    }
                }
                OutInfo = OutInfo.Substring(i + 1);
                if (ErrorInfo != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            /// <summary>
            /// 运行命令，打印调试信息
            /// </summary>
            /// <param name="run_line">运行命令</param>
            /// <returns>错误代码</returns>
            public static int RunLine(string run_line)
            {
                DebugInfo.WriteLineDubeg("    runline: " + run_line);
                if (run(run_line))
                {
                    DebugInfo.WriteLineDubeg("    outinfo: " + CmdRun.OutInfo);
                    DebugInfo.WriteLineDubeg("  success");
                    return 0;
                }
                else
                {
                    DebugInfo.WriteLineDubeg("    outinfo: " + CmdRun.OutInfo);
                    DebugInfo.WriteLineDubeg("    errorinfo: " + CmdRun.ErrorInfo);
                    DebugInfo.WriteLineDubeg("  not success");
                    return 1;
                }
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        class DebugInfo
        {
            /// <summary>
            /// 是否输出调试信息
            /// </summary>
            public static bool IsDebug = false;

            /// <summary>
            /// 输出调试信息
            /// </summary>
            /// <param name="info"></param>
            public static void WriteLineDubeg(object info)
            {
                if (IsDebug)
                {
                    Console.WriteLine(info);
                }
            }
        }

        static void Main(string[] args)
        {
            Csv2Shp(args);
            // ogr2ogr -f "ESRI Shapefile" lanmei1000_2021.shp lanmei1000_2021.csv -oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES -oo KEEP_GEOM_COLUMNS=NO
            // ogr2ogr -f "ESRI Shapefile" output.shp input.csv -oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES -oo KEEP_GEOM_COLUMNS=NO-oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES -oo KEEP_GEOM_COLUMNS=NO
            // ogr2ogr -oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES -oo KEEP_GEOM_COLUMNS=NO-oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES -oo KEEP_GEOM_COLUMNS=NO -f "ESRI Shapefile" output.shp input.csv 
            // ogr2ogr -f "ESRI Shapefile" output.shp input.csv
            // -oo X_POSSIBLE_NAMES=X
            // -oo Y_POSSIBLE_NAMES=Y
            // -oo AUTODETECT_TYPE=YES
            // -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs"
            // -overwrite 
            //


        }

        static void Csv2ShpHelp()
        {
            Console.WriteLine("srt_csv2shp [csv file] [/shp file] [opt: --kgc Default:KEEP_GEOM_COLUMNS=NO]");
            Console.WriteLine("    [csv file] input csv file name");
            Console.WriteLine("    [opt: shp file] out shape file");
            Console.WriteLine("    [opt: --kgc Default:KEEP_GEOM_COLUMNS=NO] whether to keep XY columns");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        static void Csv2Shp(string[] args)
        {
            string csv_file = null;
            string shp_file = null;
            string kgc = "-oo KEEP_GEOM_COLUMNS=NO";
            string line = "\"C:\\Program Files\\GDAL\\ogr2ogr.exe\" " +
                "-f \"ESRI Shapefile\" output.shp input.csv " +
                "-oo X_POSSIBLE_NAMES=X " +
                "-oo Y_POSSIBLE_NAMES=Y " +
                "-oo AUTODETECT_TYPE=YES " +
                "-a_srs \"+proj=longlat +datum=WGS84 +no_defs +type=crs\" " +
                "-overwrite " + kgc;
            if (args.Length == 0)
            {
                Console.WriteLine("Number of args is not enough");
                Console.WriteLine(">>> " + line);
                Csv2ShpHelp();
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if(args[i] == "--kgc")
                {
                    kgc = "";
                }
                else if(csv_file == null)
                {
                    csv_file = Path.GetFullPath(args[i]);
                }
                else
                {
                    shp_file = args[i];
                }
            }
            if(shp_file == null)
            {
                shp_file = Path.Combine(Path.GetDirectoryName(csv_file), Path.GetFileNameWithoutExtension(csv_file) + ".shp");
            }

            //line = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\ogr2ogr.exe " +
            //    "-f \"ESRI Shapefile\" " + shp_file + " " + csv_file + " " +
            //    "-oo X_POSSIBLE_NAMES=X " +
            //    "-oo Y_POSSIBLE_NAMES=Y " +
            //    "-oo AUTODETECT_TYPE=YES " +
            //    "-a_srs \"+proj=longlat +datum=WGS84 +no_defs +type=crs\" " +
            //    "-overwrite " + kgc;     
            line = "ogr2ogr.exe " +
                "-f \"ESRI Shapefile\" " + shp_file + " " + csv_file + " " +
                "-oo X_POSSIBLE_NAMES=X " +
                "-oo Y_POSSIBLE_NAMES=Y " +
                "-oo AUTODETECT_TYPE=YES " +
                "-a_srs epsg:4326 " +
                "-overwrite " + kgc;
            Console.WriteLine(line);
            if (!CmdRun.run(line))
            {
                Console.WriteLine("    outinfo: " + CmdRun.OutInfo);
                Console.WriteLine("    errorinfo: " + CmdRun.ErrorInfo);
            }
            Csv2ShpHelp();
        }
    }
}

