/*------------------------------------------------------------------------------
 * File    : Program.cs
 * Time    : 2022-4-15 9:32:22
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of main
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace JieXiSD
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"D:\code\bin\JieXiSD.py"))
            {
                if (args.Length != 0)
                {
                    if (File.Exists(args[0]))
                    {
                        if (Path.GetExtension(args[0]) == ".html")
                        {
                            if (args.Length == 1)
                            {
                                string ofile = Path.GetFileNameWithoutExtension(args[0]) + ".txt";
                                Console.WriteLine(GetCmdInfo(args[0], ofile));
                                Console.WriteLine("In file: " + Path.GetFullPath(args[0]));
                                Console.WriteLine("Out file: " + Path.GetFullPath(ofile));
                                Console.WriteLine("success");
                            }
                            else
                            {
                                Console.WriteLine(GetCmdInfo(args[0], args[1]));
                                Console.WriteLine("In file: " + Path.GetFullPath(args[0]));
                                Console.WriteLine("Out file: " + Path.GetFullPath(args[1]));
                                Console.WriteLine("success");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: file type have to *.html -- " + args[0]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: html file not find - " + args[0]);
                    }
                }
                else
                {
                    Console.WriteLine("Warning: number of args is not enough");
                }
            }
            else
            {
                Console.WriteLine("Error: missing dependencies ZhiWangJieXi.py");
            }


            Console.WriteLine("srt_jiexisd [html file] [/out file]");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        public static string GetCmdInfo(string tfile, string ofile)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            p.Start(); // 启动程序
            string in_str = @"python D:\code\bin\JieXiSD.py" + " " + tfile + " " + ofile;
            p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
            int n = 0;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == '\n')
                {
                    n++;
                }
                if (n == 4)
                {
                    output = output.Substring(i + 1);
                }
            }
            output = output.Trim();
            p.WaitForExit(); // 等待程序执行完退出进程
            p.Close();
            return output;
        }
    }
}
