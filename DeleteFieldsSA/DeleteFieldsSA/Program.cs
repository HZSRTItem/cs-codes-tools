/*------------------------------------------------------------------------------
 * File    : Program.cs
 * Time    : 2022-5-8 19:56:51
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of Program
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DeleteFieldsSA
{
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

    class Program
    {
        static void Usage()
        {
            Console.WriteLine("srt_deletefields [in data source] [fields ...]");
            Console.WriteLine("    Use this tool to delete fields of a data source supported by gdal");
            Console.WriteLine("    in_data_source: input data source");
            Console.WriteLine("    fields: a lots of fields");
            Console.WriteLine("    --help: get help");
            Console.WriteLine("    --debug: debug");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        static void Main(string[] args)
        {
            //args = new string[3] { "test01.shp", "srt1", "--debug" };
            //args = new string[3] { "test01.csv", "srt0", "--debug" };
            int argc = args.Length;
            if (argc < 2)
            {
                Console.WriteLine("Number of in params not enough");
                Usage();
                return;
            }
            // "D:\code\lib\geosqlitself.exe"
            string in_data_source = "";
            List<string> fields = new List<string>();
            for (int i = 0; i < argc; i++)
            {
                if (args[i] == "--help")
                {
                    Usage();
                    return;
                }
                else if(args[i] == "--debug")
                {
                    DebugInfo.IsDebug = true;
                }
                else
                {
                    if (in_data_source == "")
                    {
                        in_data_source = args[i];
                    }
                    else
                    {
                        fields.Add(args[i]);
                    }
                }
            }

            // 检查文件
            string full_file_name = Path.GetFullPath(in_data_source);
            string table_name = Path.GetFileNameWithoutExtension(full_file_name);

            for (int i = 0; i < fields.Count; i++)
            {
                string sql_line = "ALTER TABLE " + table_name + " DROP COLUMN " + fields[i];
                if (CmdRun.RunLine(@"D:\code\lib\geosqlitself.exe " + full_file_name + " \"" + sql_line + "\" null") == 0)
                {
                    Console.WriteLine("success: " + fields[i]);
                }
                else
                {
                    string info = Regex.Replace(CmdRun.ErrorInfo, @"[\n\r]", " ");
                    Console.WriteLine("fail   : " + fields[i] + " ** " + info);
                }
            }
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            //Console.ReadLine();
        }
    }
}
