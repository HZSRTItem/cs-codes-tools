using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace QgisProcessCSA
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;
           
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "srt_qrastersampling [input_vector] [input_raster] [opt: -prefix] [opt: -o out shape file]          \n" +
                    "    input_vector: [vector:point] Point vector layer to use for sampling                            \n" +
                    "    input_raster: [raster] Raster layer to sample at the given point locations.                    \n" +
                    "    opt:-prefix: [string default:SAMPLE_] Prefix for the names of the added columns.               \n" +
                    "    opt:-o: [vector:point default:_rspl.shp] Specify the output layer containing the sampled values");
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                return;
            }

            string input_vector = null;
            string input_raster = null;
            string out_shp_file = null;
            string prefix = "SAMPLE_";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o" & i < args.Length - 1)
                {
                    out_shp_file = args[i + 1];
                    i++;
                }
                else if (args[i] == "-prefix" & i < args.Length - 1)
                {
                    prefix = args[i + 1];
                    i++;
                }
                else if (input_vector == null)
                {
                    input_vector = args[i];
                }
                else if (input_raster == null)
                {
                    input_raster = args[i];
                }
            }

            if (input_vector == null)
            {
                Console.WriteLine("Error: can not find input vector file");
                Console.WriteLine(
                    "srt_qrastersampling [input_vector] [input_raster] [opt: -prefix] [opt: -o out shape file]\n" +
                    "    input_vector: [vector:point] Point vector layer to use for sampling                            \n" +
                    "    input_raster: [raster] Raster layer to sample at the given point locations.                    \n" +
                    "    opt:-prefix: [string default:SAMPLE_] Prefix for the names of the added columns.               \n" +
                    "    opt:-o: [vector:point default:_rspl.shp] Specify the output layer containing the sampled values");
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                return;
            }

            if (out_shp_file == null)
            {
                out_shp_file = Path.GetFullPath(CExt(input_vector, "_rspl.shp"));
            }
            else
            {
                out_shp_file = Path.GetFullPath(out_shp_file);
            }

            if (File.Exists(out_shp_file))
            {
                File.Delete(out_shp_file);
            }

            if (input_raster == null)
            {
                Console.WriteLine("Error: can not find input raster file");
                Console.WriteLine(
                    "srt_qrastersampling [input_vector] [input_raster] [opt: -prefix] [opt: -o out shape file]          \n" +
                    "    input_vector: [vector:point] Point vector layer to use for sampling                            \n" +
                    "    input_raster: [raster] Raster layer to sample at the given point locations.                    \n" +
                    "    opt:-prefix: [string default:SAMPLE_] Prefix for the names of the added columns.               \n" +
                    "    opt:-o: [vector:point default:_rspl.shp] Specify the output layer containing the sampled values");
                                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                return;
            }

            string cmd_line = string.Format( "qgis_process-qgis-ltr.bat run native:rastersampling -- \"INPUT={0}\" " +
                "\"RASTERCOPY={1}\" \"COLUMN_PREFIX={2}\" \"OUTPUT={3}\"", input_vector, input_raster, prefix, out_shp_file);
            Console.WriteLine("\n> " + cmd_line);

            Console.WriteLine("\nRun Time: " + (DateTime.Now - dt).ToString());
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            //Console.ReadLine();
        }

        private static void NewMethod(string[] args)
        {
            if (CmdRun.run("qgis_process-qgis-ltr.bat"))
            {
                srt_qrastersampling(args);
            }
            else
            {
                Console.WriteLine("Can not find `qgis_process-qgis-ltr.bat` in your computer, please install QGIS\n");
            }
        }

        /// <summary>
        /// 栅格采样
        ///  Extracts raster values at the point locations. If the raster layer is multiband, each band is sampled.
        ///  The attribute table of the resulting layer will have as many new columns as the raster layer band count.
        /// </summary>
        /// <param name="args"></param>
        static void srt_qrastersampling(string[] args)
        {

            /*
             { 'COLUMN_PREFIX' : 'SAMPLE_', 
            'INPUT' : 'E:/ImageData/Shadow/Samples/sj_spl3_cy1.shp',
            'OUTPUT' : 'E:/ImageData/Shadow/Samples/t1.csv', 
            'RASTERCOPY' : 'E:/ImageData/Shadow/Imd/sj_qd_s21.tif' }
             
            args = new string[8]
            {
                "E:/ImageData/Shadow/Samples/sj_spl3_cy1.shp",
                "-o", "E:/ImageData/Shadow/Samples/t1.csv",
                "-prefix", "t0_",
                "-prefix", "t1_",
                //"--debug",
                "E:/ImageData/Shadow/Imd/sj_qd_s21.tif"
            };*/

            Args args0 = new Args()
            {
                Name = "srt_qrastersampling",
                HelpInfo = "  Extracts raster values at the point locations. If the raster layer is multiband, each band is sampled.	"
                + "\n    The attribute table of the resulting layer will have as many new columns as the raster layer band count.	"
                //+ "\n    	"
                //+ "\n    Parameters	"
                //+ "\n    Label | 名称 | Type | Description	"
                //+ "\n    Input Layer | INPUT | [vector: point] | Point vector layer to use for sampling	"
                //+ "\n    Raster Layer | RASTERCOPY | [raster] | Raster layer to sample at the given point locations.	"
                //+ "\n    Output column prefix | COLUMN_PREFIX | [string] Default: 'SAMPLE_' | Prefix for the names of the added columns.	"
                //+ "\n    Output Layer | OUTPUT | [vector: point]  | Specify the output layer containing the sampled values	"
                //+ "\n    	"
                //+ "\n    Outputs	"
                //+ "\n    Label | 名称 | Type | Description	"
                //+ "\n    Sampled | OUTPUT | [vector: point] | The output layer containing the sampled values.	"
                //+ "\n    	"
                //+ "\n    Python code	"
                //+ "\n    Algorithm ID: native:rastersampling	"
                //+ "\n    import processing	"
                //+ "\n    processing.run(\"algorithm_id\", {parameter_dictionary})	"
                //+ "\n    	"
                //+ "\n    The algorithm id is displayed when you hover over the algorithm in the Processing Toolbox. 	"
                //+ "\n    The parameter dictionary provides the parameter NAMEs and values. See Using processing algorithms	"
                //+ "\n    from the console for details on how to run processing algorithms from the Python console.	"
                + "\n    https://docs.qgis.org/3.22/zh-Hans/docs/user_manual/processing_algs/qgis/rasteranalysis.html#id110"
            };

            args0.AddArg("input_vector", 1, "INPUT", "[vector:point] Point vector layer to use for sampling");
            args0.AddArg("input_raster", 1, "RASTERCOPY", "[raster] Raster layer to sample at the given point locations.");
            args0.AddArg("-prefix", 1, "COLUMN_PREFIX", "SAMPLE_", "[string default:SAMPLE_] Prefix for the names of the added columns.");
            args0.AddArg("-o", 1, "OUTPUT", null, "[vector:point default:_rspl.shp] Specify the output layer containing the sampled values");

            if (args.Length == 0)
            {
                Console.WriteLine(args0.GetHelpInfo());
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--debug")
                {
                    DebugInfo.IsDebug = true;
                }
                else if (args[i] == "--help")
                {
                    Console.WriteLine(args0.GetHelpInfo());
                    return;
                }
                else if (args[i] == "-o" & i < args.Length - 1)
                {
                    args0["-o"] = args[i + 1];
                    i++;
                }
                else if (args[i] == "-prefix" & i < args.Length - 1)
                {
                    args0["-prefix"] = args[i + 1];
                    i++;
                }
                else if (args0["input_vector"] == null)
                {
                    args0["input_vector"] = args[i];
                }
                else
                {
                    args0["input_raster"] = args[i];
                }
            }

            if (args0["input_vector"] == null)
            {
                Console.WriteLine("Error: can not find input vector file");
                Console.WriteLine(args0.GetHelpInfo());
                return;
            }

            if (args0["-o"] == null)
            {
                args0["-o"] = Path.GetFullPath(CExt(args0["input_vector"], "_rspl.shp"));
            }

            if (File.Exists(args0["-o"]))
            {
                File.Delete(args0["-o"]);
            }

            if (args0["input_raster"] == null)
            {
                Console.WriteLine("Error: can not find input raster file");
                return;
            }

            string cmd_line = "qgis_process-qgis-ltr.bat run native:rastersampling -- " + args0.GetQgisArgs();
            Console.WriteLine("> " + cmd_line);
            //CmdRun.RunLine(cmd_line);
        }

        static string CExt(string in_file, string ext)
        {
            return Path.Combine(Path.GetDirectoryName(in_file), Path.GetFileNameWithoutExtension(in_file) + ext);
        }
    }


    /// <summary>
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



}
