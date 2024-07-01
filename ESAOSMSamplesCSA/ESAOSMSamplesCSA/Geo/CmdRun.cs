/*------------------------------------------------------------------------------
 * File    : CmdRun
 * Time    : 2022/4/28 19:51:33
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[CmdRun]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SrtGeo
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
        ///  这是一个函数，可以运行命令行程序，获得是否运行的时候出现错误
        ///  OutInfo: 静态字符串中保存运行信息
        ///  ErrorInfo: 静态字符串中保存运行信息
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
