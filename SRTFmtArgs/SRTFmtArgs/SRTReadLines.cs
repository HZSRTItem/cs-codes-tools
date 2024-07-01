/*------------------------------------------------------------------------------
 * File    : SRTReadLines
 * Time    : 2023/2/16 10:41:18
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTReadLines]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTFmtArgs
{
    class SRTReadLines
    {

        public static string ReadLines()
        {
            string lines = "";
            int i = 1;
            while (true)
            {
                Console.Write(string.Format("{0, 5}|", i++));
                string line = Console.ReadLine();
                if (line.Contains("--exit"))
                {
                    line = line.Substring(0, line.IndexOf("--exit"));
                    lines += line;
                    break;
                }
                lines += line + "\n";
            }
            return lines;
        }
    }
}
