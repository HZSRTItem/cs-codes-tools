/*------------------------------------------------------------------------------
 * File    : GeoUtils
 * Time    : 2022/4/26 10:52:31
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[GeoUtils] 地理处理的库
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoUtils
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
        /// gdal - gdalinfo
        /// </summary>
        public static string gdalinfo = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\gdalinfo.exe";
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
    /// 矢量类
    /// </summary>
    class ShapeInfo
    {
        public string InFile = "";
        /// <summary>
        /// 矢量中样本的数量
        /// </summary>
        public int NSample = 0;
        /// <summary>
        /// 空间参考
        /// </summary>
        public string SpatialRef = "";
        /// <summary>
        /// 矢量的几何图形
        /// </summary>
        List<ShapeGeometry> ShapeGeometrys = new List<ShapeGeometry>();
        /// <summary>
        /// 矢量的属性名
        /// </summary>
        public string[] DbfHeader = null;
        /// <summary>
        /// 属性值
        /// </summary>
        private List<string[]> DbfValues = new List<string[]>();

        /// <summary>
        /// 使用 shape file 文件构建矢量对象
        /// </summary>
        /// <param name="shape_file">矢量文件</param>
        public ShapeInfo(string shape_file)
        {

            // ogr2ogr -f CSV output.csv input.shp -lco geometry=as_wkt
            //  -lco \"GEOMETRY=AS_WKT\" -lco \"SEPARATOR=TAB\" -f CSV 
            // shp转为csv
            // 读取csv 
            InFile = shape_file;
            DebugInfo.WriteLineDubeg(">>> Class ShapeInfo -f " + shape_file);
            string csv_file = Path.Combine(Path.GetDirectoryName(shape_file), Path.GetFileNameWithoutExtension(shape_file) + ".csv");
            if (File.Exists(csv_file))
            {
                File.Delete(csv_file);
            }

            string cmd_line = UseExes.ogr2ogr + " -overwrite -lco GEOMETRY=AS_WKT -lco SEPARATOR=TAB -f CSV " + csv_file + " " + shape_file;
            if (CmdRun.RunLine(cmd_line) == 0)
            {
                StreamReader sr = new StreamReader(csv_file);
                string line = sr.ReadLine();
                string[] lines = line.Split('\t');
                line = sr.ReadLine();
                DbfHeader = lines;

                while (line != null)
                {
                    lines = line.Split('\t');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = utils.remove_yh(lines[i]);
                    }
                    ShapeGeometry geom = ShapeGeometry.FormatWKT(lines[0]);
                    ShapeGeometrys.Add(geom);
                    lines[0] = "";
                    DbfValues.Add(lines);
                    line = sr.ReadLine();
                }
                sr.Close();

                File.Delete(csv_file);
            }
            else
            {
                throw new Exception("Error: cmd line can not run\n    " + cmd_line);
            }


            NSample = DbfValues.Count;

            // 计算空间参考
            string prj_file = Path.Combine(Path.GetDirectoryName(shape_file), Path.GetFileNameWithoutExtension(shape_file) + ".prj");
            SpatialRef = File.ReadAllText(prj_file).Trim();
        }

        /// <summary>
        /// 保存矢量文件
        /// </summary>
        /// <param name="out_shp_file"></param>
        /// <returns></returns>
        public int SaveToShapeFile(string out_shp_file)
        {
            DebugInfo.WriteLineDubeg(">>> Run ShapeInfo.SaveToShapeFile -f " + out_shp_file);

            // GEOM_POSSIBLE_NAMES=WKT
            string csv_file = Path.Combine(Path.GetDirectoryName(out_shp_file), Path.GetFileNameWithoutExtension(out_shp_file) + ".csv");
            if (File.Exists(csv_file))
            {
                File.Delete(csv_file);
            }
            StreamWriter sw = new StreamWriter(csv_file);
            sw.Write("WKT");
            for (int i = 1; i < DbfHeader.Length; i++)
            {
                sw.Write("," + DbfHeader[i]);
            }
            sw.Write("\n");
            for (int i = 0; i < NSample; i++)
            {
                string wkt = utils.add_yh(ShapeGeometrys[i].ToWKT());
                sw.Write(wkt);
                for (int j = 1; j < DbfHeader.Length; j++)
                {
                    sw.Write("," + utils.add_yh(DbfValues[i][j]));
                }
                sw.Write("\n");
            }
            sw.Close();
            // SpatialRef
            // ogr2ogr -f "ESRI Shapefile" output.shp input.csv -oo X_POSSIBLE_NAMES=X -oo Y_POSSIBLE_NAMES=Y -a_srs "+proj=longlat +datum=WGS84 +no_defs +type=crs" -overwrite -oo AUTODETECT_TYPE=YES
            string cmdline = UseExes.ogr2ogr + " -f \"ESRI Shapefile\" -overwrite -oo AUTODETECT_TYPE=YES -oo GEOM_POSSIBLE_NAMES=WKT -oo KEEP_GEOM_COLUMNS=NO -a_srs "
                + utils.add_yh(SpatialRef) + " " 
                + utils.add_yh(out_shp_file) + " " 
                + utils.add_yh(csv_file);
            int n = CmdRun.RunLine(cmdline);
            File.Delete(csv_file);
            return n;
        }

        public int Save0()
        {
            return SaveToShapeFile(InFile);
        }

    }

    class ShapeGeometry
    {
        /// <summary>
        /// 矢量的类型枚举
        /// </summary>
        public enum LeiXing
        {
            Point,
            MultPoint,
            Line,
            MultLine,
            Polygon,
            MultPolygon,
            None
        }

        /// <summary>
        /// 类型
        /// </summary>
        public LeiXing GType;
        /// <summary>
        /// 数量
        /// </summary>
        public int GNumber = 0;
        /// <summary>
        /// X 坐标
        /// </summary>
        private List<double[]> x = new List<double[]>();
        /// <summary>
        /// Y 坐标
        /// </summary>
        private List<double[]> y = new List<double[]>();

        /// <summary>
        /// 构造一个空的矢量对象
        /// </summary>
        private ShapeGeometry() { }

        /// <summary>
        /// 使用矢量的类型构造一个空的矢量空间对象
        /// </summary>
        /// <param name="gtype"></param>
        public ShapeGeometry(string gtype)
        {
            GType = LeiXing.None;
        }

        /// <summary>
        /// 通过WKT返回一个矢量空间对象
        /// </summary>
        /// <param name="wkt_string"></param>
        /// <returns></returns>
        public static ShapeGeometry FormatWKT(string wkt_string)
        {
            ShapeGeometry geom = new ShapeGeometry();
            wkt_string = wkt_string.Trim();
            if (wkt_string == "")
            {
                return geom;
            }
            wkt_string = wkt_string.Replace("\"", "");
            if (wkt_string[0] == '\"')
            {
                wkt_string.Remove(0, 1);
            }
            if (wkt_string[wkt_string.Length - 1] == '\"')
            {
                wkt_string.Remove(wkt_string.Length - 2);
            }
            wkt_string = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(wkt_string, " ");
            wkt_string = wkt_string.Replace("\n", "");
            wkt_string = wkt_string.Replace(" (", "\n");
            wkt_string = wkt_string.Replace("),", "\n");
            wkt_string = wkt_string.Replace("(", "");
            wkt_string = wkt_string.Replace("))", "");
            wkt_string = wkt_string.Replace(")", "");
            string[] wkt_lines = wkt_string.Split('\n');
            geom.SetGType(wkt_lines[0].Trim());
            int n = wkt_lines.Length;
            if (wkt_lines[wkt_lines.Length - 1].Trim() == "")
            {
                n = n - 1;
            }
            for (int i = 1; i < n; i++)
            {
                string[] lines = wkt_lines[i].Split(',');
                double[] x = new double[lines.Length];
                double[] y = new double[lines.Length];
                for (int j = 0; j < lines.Length; j++)
                {
                    string[] xy = lines[j].Split(' ');
                    x[j] = double.Parse(xy[0]);
                    y[j] = double.Parse(xy[1]);
                }
                geom.AddOne(x, y);
            }
            return geom;
        }

        /// <summary>
        /// 设置矢量空间对象的类型
        /// </summary>
        /// <param name="gtype"></param>
        /// <returns></returns>
        private bool SetGType(string gtype)
        {
            // temp.IndexOf(key,StringComparison.OrdinalIgnoreCase)>=0
            // MULTILINESTRING
            // MULTIPOINT
            // MULTILINESTRING
            if (gtype.ToLower() == "point")
            {
                GType = LeiXing.Point;
            }
            else if (gtype.ToLower() == "multipoint")
            {
                GType = LeiXing.MultPoint;
            }
            else if (gtype.ToLower() == "linestring")
            {
                GType = LeiXing.Line;
            }
            else if (gtype.ToLower() == "multilinestring")
            {
                GType = LeiXing.MultLine;
            }
            else if (gtype.ToLower() == "polygon")
            {
                GType = LeiXing.Polygon;
            }
            else if (gtype.ToLower() == "multipolygon")
            {
                GType = LeiXing.MultPolygon;
            }
            else
            {
                GType = LeiXing.None;
            }
            //DebugInfo.WriteLineDubeg("\ngtype: " + gtype);
            return true;
        }

        /// <summary>
        /// 添加一个矢量类型
        /// </summary>
        /// <param name="coor_x"></param>
        /// <param name="coor_y"></param>
        /// <returns></returns>
        public int AddOne(double coor_x, double coor_y)
        {
            if(GType == LeiXing.Point)
            {
                x.Add(new double[1] { coor_x });
                y.Add(new double[1] { coor_y });
                GNumber++;
                return GNumber;
            }
            else
            { 
                throw new Exception("Error: GType have to point not " + GType.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coor_x"></param>
        /// <param name="coor_y"></param>
        /// <returns></returns>
        public int AddOne(double[] coor_x, double[] coor_y)
        {
            //if (coor_x.Length != coor_y.Length)
            //{
            //    throw new Exception("Error: input double[] length have to equal -- coor_x-" + coor_x.Length.ToString() + " != coor_y-" + coor_y.Length.ToString());
            //}
            //if (GType == LeiXing.Point)
            //{
            //    if(coor_x.Length !=1)
            //    {
            //        throw new Exception("Error: Point can not add to Gtype -- " + GType.ToString());
            //    }
            //}
            //else if (GType == LeiXing.Line)
            //{
            //    if (coor_x.Length < 2)
            //    {
            //        throw new Exception("Error: Line input double[] length have to more than 2 -- in-" + coor_x.Length.ToString());
            //    }
            //}
            //else if (GType == LeiXing.Polygon)
            //{
            //    if (coor_x.Length < 3)
            //    {
            //        throw new Exception("Error: Polygon input double[] length have to more than 3 -- in-" + coor_x.Length.ToString());
            //    }
            //    else
            //    {
            //        double xmm = Math.Abs(coor_x[0] - coor_x[coor_x.Length - 1]);
            //        double ymm = Math.Abs(coor_y[0] - coor_y[coor_y.Length - 1]);
            //        if (!(xmm < 10e-8 & ymm < 1e-8))
            //        {
            //            throw new Exception("Error: Polygon input not closure");
            //        }
            //    }
            //}

            x.Add(coor_x);
            y.Add(coor_y);
            GNumber++;
            return GNumber;
        }

        /// <summary>
        /// 转为WKT
        /// </summary>
        /// <returns></returns>
        public string ToWKT()
        {
            string wkt = "";
            if (GType == LeiXing.Point)
            {
                wkt = "POINT ";
                wkt += "(";
                wkt += x[0][0].ToString("F10") + " ";
                wkt += y[0][0].ToString("F10");
                wkt += ")";
            }
            else if (GType == LeiXing.MultPoint)
            {
                wkt = "MULTIPOINT ";
                wkt = CoorToWKT(wkt);
            }
            else if (GType == LeiXing.Line)
            {
                wkt = "LINESTRING ";
                wkt += "(";
                int j = 0;
                for (; j < x[0].Length - 1; j++)
                {
                    wkt += x[0][j].ToString("F10") + " ";
                    wkt += y[0][j].ToString("F10") + ",";
                }
                wkt += x[0][j].ToString("F10") + " ";
                wkt += y[0][j].ToString("F10");
                wkt += ")";
            }
            else if (GType == LeiXing.MultLine)
            {
                wkt = "MULTILINESTRING ";
                wkt = CoorToWKT(wkt);
            }
            else if (GType == LeiXing.Polygon)
            {
                wkt = "POLYGON ";
                wkt = CoorToWKT(wkt);
            }
            else if (GType == LeiXing.MultPolygon)
            {
                wkt = "MULTIPOLYGON ";
                wkt = CoorToWKT(wkt);
            }
            else
            {
                wkt = "None ";
                wkt = CoorToWKT(wkt);
            }
            return wkt;
        }

        /// <summary>
        /// 坐标转为WKT字符串
        /// </summary>
        /// <param name="wkt"></param>
        /// <returns></returns>
        private string CoorToWKT(string wkt)
        {
            wkt += "(";
            int i = 0;
            int j = 0;
            for (; i < GNumber - 1; i++) // 遍历线
            {
                wkt += "(";
                j = 0;
                for (; j < x[i].Length - 1; j++)
                {
                    wkt += x[i][j].ToString("F10") + " ";
                    wkt += y[i][j].ToString("F10") + ",";
                }
                wkt += x[i][j].ToString("F10") + " ";
                wkt += y[i][j].ToString("F10");
                wkt += ")";
                wkt += ", ";
            }
            wkt += "(";
            j = 0;
            for (; j < x[i].Length - 1; j++)
            {
                wkt += x[i][j].ToString("F10") + " ";
                wkt += y[i][j].ToString("F10") + ",";
            }
            wkt += x[i][j].ToString("F10") + " ";
            wkt += y[i][j].ToString("F10");
            wkt += ")";
            wkt += ")";
            return wkt;
        }
    }

    class EllipsoidPloyArea
    {
        /// <summary>
        /// 计算面积对象
        /// </summary>
        /// <param name="a">长半轴</param>
        /// <param name="b">短半轴</param>
        public EllipsoidPloyArea(double a, double b)
        {
            ComputeAreaInit(a, b);
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="padX">经度</param>
        /// <param name="padY">纬度</param>
        /// <returns></returns>
        public double Cal(double[] padX, double[] padY)
        {
            return CalArea(padX, padY);
        }

        #region 面积计算
        private double mSemiMajor, mSemiMinor, mInvFlattening;
        private double m_QA, m_QB, m_QC;
        private double m_QbarA, m_QbarB, m_QbarC, m_QbarD;
        private double m_AE; /* a^2(1-e^2) */
        private double m_Qp;
        private double m_E;
        private double m_TwoPI;
        private double M_PI = 3.141592653589793115997963468544185161590576171875;
        private double GetQ(double x)
        {
            double sinx, sinx2;
            sinx = Math.Sin(x);
            sinx2 = sinx * sinx;
            return sinx * (1 + sinx2 * (m_QA + sinx2 * (m_QB + sinx2 * m_QC)));
        }
        private double GetQbar(double x)
        {
            double cosx, cosx2;
            cosx = Math.Cos(x);
            cosx2 = cosx * cosx;
            return cosx * (m_QbarA + cosx2 * (m_QbarB + cosx2 * (m_QbarC + cosx2 * m_QbarD)));
        }
        private void ComputeAreaInit(double a, double b)
        {
            mSemiMajor = a;
            mSemiMinor = b;
            // mInvFlattening = mSemiMajor

            double a2 = (mSemiMajor * mSemiMajor);
            double e2 = 1 - (a2 / (mSemiMinor * mSemiMinor));
            double e4, e6;

            m_TwoPI = M_PI + M_PI;

            e4 = e2 * e2;
            e6 = e4 * e2;

            m_AE = a2 * (1 - e2);

            m_QA = (2.0 / 3.0) * e2;
            m_QB = (3.0 / 5.0) * e4;
            m_QC = (4.0 / 7.0) * e6;

            m_QbarA = -1.0 - (2.0 / 3.0) * e2 - (3.0 / 5.0) * e4 - (4.0 / 7.0) * e6;
            m_QbarB = (2.0 / 9.0) * e2 + (2.0 / 5.0) * e4 + (4.0 / 7.0) * e6;
            m_QbarC = -(3.0 / 25.0) * e4 - (12.0 / 35.0) * e6;
            m_QbarD = (4.0 / 49.0) * e6;

            m_Qp = GetQ(M_PI / 2);
            m_E = 4 * M_PI * m_Qp * m_AE;
            if (m_E < 0.0)
                m_E = -m_E;
        }
        double deg2rad(double x)
        {
            return x * M_PI / 180.0;
        }
        private double CalArea(double[] padX, double[] padY)
        {
            double area = 0.0;

            int nCount = padX.Length < padY.Length ? padX.Length : padY.Length;
            double x1, y1, dx, dy;
            double Qbar1, Qbar2;

            if (padX.Length == 0 || 0 == padY.Length)
            {
                return 0;
            }

            if (nCount < 3)
            {
                return 0;
            }

            double x2 = deg2rad(padX[nCount - 1]);
            double y2 = deg2rad(padY[nCount - 1]);
            Qbar2 = GetQbar(y2);



            for (int i = 0; i < nCount; i++)
            {
                x1 = x2;
                y1 = y2;
                Qbar1 = Qbar2;

                x2 = deg2rad(padX[i]);
                y2 = deg2rad(padY[i]);
                Qbar2 = GetQbar(y2);

                if (x1 > x2)
                    while (x1 - x2 > M_PI)
                        x2 += m_TwoPI;
                else if (x2 > x1)
                    while (x2 - x1 > M_PI)
                        x1 += m_TwoPI;

                dx = x2 - x1;
                area += dx * (m_Qp - GetQ(y2));

                if ((dy = y2 - y1) != 0.0)
                    area += dx * GetQ(y2) - (dx / dy) * (Qbar2 - Qbar1);
            }
            if ((area *= m_AE) < 0.0)
                area = -area;

            if (area > m_E)
                area = m_E;
            if (area > m_E / 2)
                area = m_E - area;

            return area;
        }
        #endregion
    }

    class RasterSample
    {
        public RasterSample(string raster_file, int n_sample)
        {
            // 检查栅格的数据类型
            // gdallocationinfo 采样
            string line = UseExes.gdalinfo + " -json" + raster_file;
            bool check_raster = false;
            if(CmdRun.RunLine(line) == 0)
            {
                string info_json = CmdRun.OutInfo;
                JObject jo = (JObject)JsonConvert.DeserializeObject(info_json);
                JToken jToken_bands = jo["bands"];
                if(jToken_bands == null)
                {
                    throw new Exception("Raster Format Error: not find info bands");
                }
                else
                {
                    if (jToken_bands.Count() == 1)
                    {
                        JToken jToken_band0 = jToken_bands[0]["type"];
                        if(jToken_band0 == null)
                        {
                            throw new Exception("Raster Format Error: not find info type");
                        }
                        else
                        {
                            string isint = jToken_band0.ToString();
                            // "Byte" 
                            if(isint == "Byte")
                            {
                                check_raster = true;
                            }
                            else
                            {
                                throw new Exception("Error: raster data type have to byte");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Error: number of raster have to one 1");
                    }
                }
            }
            else
            {
                throw new Exception("CMD RUN ERROR: " + CmdRun.ErrorInfo);
            }

        }
    }
}
