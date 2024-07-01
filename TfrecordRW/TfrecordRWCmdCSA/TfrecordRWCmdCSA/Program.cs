/*------------------------------------------------------------------------------
 * File    :
 * Time    : 2022-5-15 16:36:23
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

namespace TfrecordRWCmdCSA
{
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

    class Program
    {
        static void Main(string[] args)
        {
            //DcTfrWFA01();
            Tfr2Csv(args);
        }

        static void DcTfrWFA01()
        {
            System.Diagnostics.Process.Start(@"D:\code\lib\srt_dctfr\net5.0-windows\DcTfrWFA01.exe");
        }

        static void Tfr2Csv(string[] args)
        {
            DebugInfo.IsDebug = true;
            // "D:\code\lib\srt_tfr2csv\net5.0\Tfr2CsvCSA.exe"
            string Tfr2CsvCSA_fn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"srt_tfr2csv\net5.0\Tfr2CsvCSA.exe");
            string tfr_file;
            string csv_file;
            if (args.Length == 0)
            {
                Console.WriteLine("srt_tfr2csv [tfrecord file] [/ csv file]");
                Console.WriteLine("    Extract data for features with number 1 in file as csv file");
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                return;
            }
            else if (args.Length == 1)
            {
                tfr_file = args[0];
                if (File.Exists(tfr_file))
                {
                    tfr_file = Path.GetFullPath(tfr_file);
                    csv_file = Path.GetFileNameWithoutExtension(tfr_file) + ".csv";
                    csv_file = Path.Combine(Path.GetDirectoryName(tfr_file), csv_file);
                    if (CmdRun.RunLine(Tfr2CsvCSA_fn + " " + tfr_file + " " + csv_file) == 0)
                    {
                        Console.WriteLine("Success");
                    }
                    Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                }
                else
                {
                    Console.WriteLine("Not find tfrecord file: " + tfr_file);
                }
            }
            else
            {
                tfr_file = args[0];
                csv_file = args[1];
                if (File.Exists(tfr_file))
                {
                    if (CmdRun.RunLine(Tfr2CsvCSA_fn + " " + tfr_file + " " + csv_file) == 0)
                    {
                        Console.WriteLine("Success");
                    }
                    Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                }
                else
                {
                    Console.WriteLine("Not find tfrecord file: " + tfr_file);
                }
            }
        }
    }
}
