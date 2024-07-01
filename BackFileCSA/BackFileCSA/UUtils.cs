/*------------------------------------------------------------------------------
 * File    : UUtils
 * Time    : 2022/10/10 18:58:11
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[UUtils]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackFileCSA
{
    class UUtils
    {
        /// <summary>
        /// 控制每行字符串的长度
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="n">字符串前面加n个空格</param>
        /// <param name="n_line">一行最多多少字符</param>
        /// <returns>格式化后的字符串</returns>
        public static string FmtStr(string s, int n = 0, int n_line = 70)
        {
            char[] in_chars = new char[s.Length * 2];
            int iline = 0;
            int ii = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                in_chars[ii++] = c;
                if (c == '\n')
                {
                    for (int j = 0; j < n; j++)
                    {
                        in_chars[ii++] = ' ';
                    }
                    iline = n;
                }
                iline += c > 127 ? 2 : 1;
                if (iline > n_line)
                {
                    while (true)
                    {
                        if (c == ' ' | c > 127 | c == '\n')
                        {
                            break;
                        }
                        i += 1;
                        if (i >= s.Length)
                        {
                            break;
                        }
                        c = s[i];
                        in_chars[ii++] = c;
                    }
                    if (i == s.Length)
                    {
                        break;
                    }
                    in_chars[ii++] = '\n';
                    for (int j = 0; j < n; j++)
                    {
                        in_chars[ii++] = ' ';
                    }
                    iline = n;
                }
            }

            in_chars = in_chars.Take(ii).ToArray();
            string out_s = new string(in_chars);
            return out_s;
        }

        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="n">字符串前面加n个空格</param>
        /// <param name="n_line">一行最多多少字符</param>
        public static void PrintFormat(int n = 0, int n_line = 70, string s = "")
        {
            if (s == "")
            {
                Console.WriteLine(s);
            }
            else
            {
                s = FmtStr(s, n: n, n_line: n_line);
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// 获得用户输入的YN
        /// </summary>
        /// <param name="tips">前情提示</param>
        /// <returns></returns>
        public static bool IsYN(string tips)
        {
            Console.Write(tips + " [y/n]:");
            while (true)
            {
                char y_n = (char)Console.Read();
                if (y_n == 'y' | y_n == 'n' )
                {
                    Console.WriteLine("\n");
                    return y_n == 'y';
                }
            }
        }

        /// <summary>
        /// 给文件名添加一个后缀
        /// </summary>
        /// <param name="file_name">文件名</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string FileNameAddSuffix(string file_name, string suffix)
        {
            string out_file_name = Path.GetFileNameWithoutExtension(file_name) + suffix + Path.GetExtension(file_name);
            out_file_name = Path.Combine(Path.GetDirectoryName(file_name), out_file_name);
            return out_file_name;
        }
    }
}
