/*------------------------------------------------------------------------------
 * File    : UseExe
 * Time    : 2022/4/28 19:48:38
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[UseExe]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
    /// <summary>
    /// 使用到的命令行程序
    /// </summary>
    class UseExes
    {
        /// <summary>
        /// gdal - ogr2ogr
        /// </summary>
        public static string ogr2ogr = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\ogr2ogr.exe";
        /// <summary>
        /// 带参数的 ogr2ogr 命令行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Ogr2ogr(params string[] args)
        {
            return ogr2ogr + " " + string.Join(" ", args);
        }

        /// <summary>
        /// gdal - gdalinfo
        /// </summary>
        public static string gdalinfo = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\gdalinfo.exe";
    }
}
