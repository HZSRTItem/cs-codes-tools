/*------------------------------------------------------------------------------
 * File    : ShapeInfo
 * Time    : 2022/5/3 10:47:31
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ShapeInfo]
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
    /// 矢量类
    /// </summary>
    class ShapeInfo
    {
        // 不会变
        /// <summary>
        /// 输入文件名
        /// </summary>
        public string InFile = "";
        /// <summary>
        /// 矢量的属性名
        /// </summary>
        public string[] DbfHeader = new string[2] { "", "SRT" };
        /// <summary>
        /// 矢量中样本的数量
        /// </summary>
        public int NSample = 0;
        /// <summary>
        /// 空间参考
        /// </summary>
        public string SpatialRef = "";
        // 会变
        /// <summary>
        /// 矢量的几何图形
        /// </summary>
        List<ShapeGeometry> ShapeGeometrys = new List<ShapeGeometry>();
        /// <summary>
        /// 属性值
        /// </summary>
        private KeyValuePair<string, AttrCol> DbfValues = new KeyValuePair<string, AttrCol>();

        public ShapeInfo()
        {

        }

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

        /// <summary>
        /// 保存当前矢量
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (InFile == "")
            {
                throw new Exception("Error: not find current file");
            }
            return SaveToShapeFile(InFile);
        }

        /// <summary>
        /// 转为点矢量
        /// </summary>
        /// <returns></returns>
        public ShapeInfo ToPoint()
        {
            ShapeInfo shapeInfo = new ShapeInfo();
            shapeInfo.SpatialRef = SpatialRef;
            for (int i = 0; i < ShapeGeometrys.Count; i++)
            {
                ShapeGeometry[] to_p = ShapeGeometrys[i].ToPoint();
                shapeInfo.ShapeGeometrys.AddRange(to_p);
                for (int j = 0; j < to_p.Length; j++)
                {
                    shapeInfo.DbfValues.Add(new string[2] { "", "0" });
                }
            }
            shapeInfo.NSample = shapeInfo.ShapeGeometrys.Count;
            return shapeInfo;
        }

        /// <summary>
        /// 样本点空间均匀
        /// </summary>
        /// <param name="density"></param>
        /// <returns></returns>
        public ShapeInfo UniformSpace(double density)
        {
            ShapeInfo shapeInfo = new ShapeInfo();
            shapeInfo.SpatialRef = SpatialRef;

            if (ShapeGeometrys[0].GType == ShapeGeometry.LeiXing.Point)
            {
                List<double> x = new List<double>();
                List<double> y = new List<double>();

                for (int i = 0; i < NSample; i++)
                {
                    x.Add(ShapeGeometrys[i].x[0][0]);
                    y.Add(ShapeGeometrys[i].y[0][0]);
                }

                int n_sample = x.Count > y.Count ? y.Count : x.Count;
                // 计算四个最大值
                double xmin = x.Min();
                double xmax = x.Max();
                double ymin = y.Min();
                double ymax = y.Max();
                // 计算网格宽度
                int m = (int)((xmax - xmin) / density);
                int n = (int)((ymax - ymin) / density);
                Random random = new Random();
                bool[,] grid_is = new bool[m + 1, n + 1];
                while (n_sample > 0)
                {
                    int i = random.Next(x.Count);
                    double x0 = x[i];
                    double y0 = y[i];
                    int m0 = (int)((xmax - x0) / density);
                    int n0 = (int)((ymax - y0) / density);
                    n_sample--;
                    x.RemoveAt(i);
                    y.RemoveAt(i);
                    if (!grid_is[m0, n0])
                    {
                        ShapeGeometry shapeGeometry = new ShapeGeometry(x0, y0);
                        shapeInfo.ShapeGeometrys.Add(shapeGeometry);
                        shapeInfo.DbfValues.Add(new string[2] { "", "0" });
                        grid_is[m0, n0] = true;
                    }
                }
            }
            shapeInfo.NSample = shapeInfo.ShapeGeometrys.Count;
            return shapeInfo;
        }

        /// <summary>
        /// 计算面积，添加一个字段
        /// </summary>
        /// <param name="field_name"></param>
        /// <returns></returns>
        public ShapeInfo CalArea(string field_name)
        {
            // 判断是不是面要素
            if (ShapeGeometrys[0].GType == ShapeGeometry.LeiXing.MultPolygon
                | ShapeGeometrys[0].GType == ShapeGeometry.LeiXing.Polygon)
            {

            }
        }
    }
}

