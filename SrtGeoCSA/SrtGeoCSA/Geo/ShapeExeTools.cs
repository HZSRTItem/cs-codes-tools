/*------------------------------------------------------------------------------
 * File    : ShapeExeTools
 * Time    : 2022/4/28 19:50:28
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ShapeExeTools]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SrtGeo
{
    /// <summary>
    /// shape file 的梳理工具
    /// </summary>
    class ShapeExeTools
    {
        /// <summary>
        /// 重投影shape文件
        ///   ogr2ogr -overwrite -t_srs [输出的空间坐标系] [输出矢量文件] [输入矢量文件]
        /// </summary>
        /// <param name="in_shp_file">输入shape文件</param>
        /// <param name="out_shp_file">输出shape文件</param>
        /// <param name="t_srs_info">输出空间参考信息，支持gdal的所有形式</param>
        /// <returns>错误代码</returns>
        public static int Reprojection(string in_shp_file, string out_shp_file, string t_srs_info)
        {
            DebugInfo.WriteLineDubeg(">>> Start Reprojection *- ");

            in_shp_file = utils.add_yh(in_shp_file);
            DebugInfo.WriteLineDubeg("    parameter in_shp_file: " + in_shp_file);
            out_shp_file = utils.add_yh(out_shp_file);
            DebugInfo.WriteLineDubeg("    parameter out_shp_file: " + out_shp_file);
            t_srs_info = utils.add_yh(t_srs_info);
            DebugInfo.WriteLineDubeg("    parameter t_srs_info: " + t_srs_info);

            string run_line = UseExes.ogr2ogr + " -overwrite -t_srs " + t_srs_info + " " + out_shp_file + " " + in_shp_file;
            //DebugInfo.WriteLineDubeg("    runline: " + run_line);

            return CmdRun.RunLine(run_line);
        }

        /// <summary>
        /// 使用shape文件裁剪矢量
        ///  ogr2ogr -overwrite -clipsrc[裁剪面状矢量][输出矢量文件][输入矢量文件]
        /// </summary>
        /// <param name="in_shp_file">输入shape文件</param>
        /// <param name="out_shp_file">输出shape文件</param>
        /// <param name="mask_shp_file">掩膜矢量文件</param>
        /// <returns>错误代码</returns>
        public static int WarpByShapeFile(string in_shp_file, string out_shp_file, string mask_shp_file)
        {
            DebugInfo.WriteLineDubeg(">>> Start WarpByShapeFile *- ");

            in_shp_file = utils.add_yh(in_shp_file);
            DebugInfo.WriteLineDubeg("    parameter in_shp_file: " + in_shp_file);
            out_shp_file = utils.add_yh(out_shp_file);
            DebugInfo.WriteLineDubeg("    parameter out_shp_file: " + out_shp_file);
            mask_shp_file = utils.add_yh(mask_shp_file);
            DebugInfo.WriteLineDubeg("    parameter mask_shp_file: " + mask_shp_file);

            string run_line = UseExes.ogr2ogr + " -overwrite -clipsrc " + mask_shp_file + " " + out_shp_file + " " + in_shp_file;
            //DebugInfo.WriteLineDubeg("    runline: " + run_line);

            return CmdRun.RunLine(run_line);
        }

        /// <summary>
        /// 对多个矢量文件进行合并
        ///  ogr2ogr file_merged.shp file1.shp
        ///  ogr2ogr -update -append file_merged.shp file2.shp -nln file_merged
        /// </summary>
        /// <param name="out_shp_file">合并后的矢量文件</param>
        /// <param name="shp_files">矢量文件列表</param>
        /// <returns></returns>
        public static int Merge(string out_shp_file, params string[] shp_files)
        {
            DebugInfo.WriteLineDubeg(">>> Start Merge *- ");

            out_shp_file = utils.add_yh(out_shp_file);
            DebugInfo.WriteLineDubeg("    parameter out_shp_file: " + out_shp_file);
            DebugInfo.WriteLineDubeg("    parameters shp_files: " );
            for (int i = 0; i < shp_files.Length; i++)
            {
                shp_files[i] = utils.add_yh(shp_files[i]);
                DebugInfo.WriteLineDubeg("    " + (i+1).ToString() + ": " + shp_files[i]);
            }

            string run_line = UseExes.Ogr2ogr(out_shp_file, shp_files[0]);
            if (CmdRun.RunLine(run_line) != 0)
            {
                return 1;
            }
            string layer_name = Path.GetFileNameWithoutExtension(out_shp_file);

            for (int i = 1; i < shp_files.Length; i++)
            {
                run_line = UseExes.Ogr2ogr("-update", "-append", "-nln", layer_name, out_shp_file, shp_files[i]);
                if (CmdRun.RunLine(run_line) != 0)
                {
                    return 1;
                }
            }

            return 0;
        }
    }

}
