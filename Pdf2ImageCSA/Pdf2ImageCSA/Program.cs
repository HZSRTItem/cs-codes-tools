using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pdf2ImageCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[]
            //{
            //    @"D:\GroupMeeting\20221127\3.韩政-2022年11月27日-两个会议.pdf"
            //};
            if (args.Length == 0)
            {
                Console.WriteLine(Usage());
                return;
            }

            string pdf_file = null;
            string out_dir = null;
            string qianzui = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o_dir" & i < args.Length - 1)
                {
                    out_dir = args[i + 1];
                    i++;
                }
                else if (args[i] == "-q" & i < args.Length - 1)
                {
                    qianzui = args[i + 1];
                    i++;
                }
                else
                {
                    pdf_file = args[i];
                }
            }

            if (!File.Exists(pdf_file))
            {
                Console.WriteLine("Can not find file: " + pdf_file);
                Console.WriteLine(Usage());
                return;
            }

            if(out_dir == null)
            {
                out_dir = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(pdf_file));
            }

            if (!Directory.Exists(out_dir))
            {
                try
                {
                    Directory.CreateDirectory(out_dir);
                }
                catch
                {
                    Console.WriteLine("Can not build directory: " + out_dir);
                    Console.WriteLine(Usage());
                    return;
                }
            }
            if (qianzui == null)
            {
                qianzui = Path.GetFileNameWithoutExtension(pdf_file);
            }
            Console.WriteLine("- PDF File: " + pdf_file);
            Console.WriteLine("- Image Directory: " + out_dir);
            Console.WriteLine("- Qian Zui: " + qianzui);
            // E:\miniconda3\envs\mm\python.exe
            string line = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"pdf2image\pdf2image.exe ") +
                utils.add_yh(pdf_file) + " " + utils.add_yh(out_dir)+ " " + utils.add_yh(qianzui);
            Console.WriteLine(">>> " + line);
            CmdRun.run(line);
            if (CmdRun.ErrorInfo == "")
            {
                Console.WriteLine("Success");
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.\n");
            }
            else
            {
                Console.WriteLine("Error: \n" + CmdRun.ErrorInfo);
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.\n");
            }
        }

        static string Usage()
        {
            string line = "srt_pdf2image pdf_file [opt:-o] [opt:-q]\n" +
                "    [opt:-o]: output dir default:.\\`pdf_file`\n" +
                "    [opt:-q]: front of image file name default:`pdf_file`_ \n" +
                "(C)Copyright 2022, ZhengHan. All rights reserved.";
            return line;
        }


    }

    /// <summary>
    /// 一些函数
    /// </summary>
    class utils
    {
        /// <summary>
        /// 添加引号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string add_yh(string info)
        {
            return "\"" + info + "\"";
        }

        public static string remove_yh(string info)
        {
            if (info == "")
            {
                return "";
            }
            else
            {
                if (info[0] == '"')
                {
                    info = info.Remove(0, 1);
                }

                if (info.Length >= 2)
                {
                    if (info[info.Length - 1] == '"')
                    {
                        info = info.Remove(info.Length - 1);
                    }
                }

                return info;
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

    }
}
