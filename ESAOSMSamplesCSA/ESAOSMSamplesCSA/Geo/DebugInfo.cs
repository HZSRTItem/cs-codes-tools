/*------------------------------------------------------------------------------
 * File    : DebugInfo
 * Time    : 2022/4/28 19:49:37
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[DebugInfo]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
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
}
