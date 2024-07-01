/*------------------------------------------------------------------------------
 * File    : Class1
 * Time    : 2022/3/31 21:42:06
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Class1]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string day_dir = DateTime.Now.ToString("yyyyMMdd");

            if (!Directory.Exists(day_dir))
            {
                Directory.CreateDirectory(day_dir);
            }
            Console.WriteLine(Path.GetFullPath(day_dir));
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            Console.ReadLine();
        }




        //private static void NewMethod(string[] args)
        //{
        //    try
        //    {
        //        if (args.Length < 3)
        //        {
        //            Console.WriteLine("Error: number of parameters is not enough.");
        //        }
        //        else
        //        {
        //            string outp = GetShapeInfo(args[1], args[0]);

        //            string[] lines = outp.Split('\n');
        //            StreamWriter sw = new StreamWriter(args[2]);
        //            bool isw = false;
        //            string line = "";

        //            for (int i = 1; i < lines.Length; i++)
        //            {
        //                line = lines[i].Trim();
        //                // |    Value:
        //                if (line.Length > 10)
        //                {
        //                    if (line.Substring(0, 6) == "Value:")
        //                    {
        //                        if (isw)
        //                        {
        //                            sw.Write(",");
        //                        }
        //                        else
        //                        {
        //                            isw = true;
        //                        }
        //                        sw.Write(line.Substring(7));

        //                    }
        //                }

        //                // Report:
        //                if (line == "Report:")
        //                {
        //                    sw.Write("\n");
        //                    isw = false;
        //                }
        //            }
        //            sw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    Console.WriteLine("> srt_sampling [raster file] [coordinte file] [out file]");
        //}

        //private static string GetShapeInfo(string coor_file, string raster_file)
        //{
        //    Process p = new Process();
        //    p.StartInfo.FileName = "cmd.exe";
        //    p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
        //    p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
        //    p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
        //    p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
        //    p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
        //    p.Start(); // 启动程序
        //    string in_str = "type " + coor_file + " | " + "gdallocationinfo " + raster_file;
        //    p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
        //    p.StandardInput.AutoFlush = true;
        //    string output = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
        //    p.WaitForExit(); // 等待程序执行完退出进程
        //    p.Close();
        //    return output;
        //}
    }
}
