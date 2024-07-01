/*------------------------------------------------------------------------------
 * File    : ShapeGeometry
 * Time    : 2022/4/28 19:52:35
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ShapeGeometry]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
    class ShapeGeometry
    {
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
        public List<double[]> x = new List<double[]>();
        /// <summary>
        /// Y 坐标
        /// </summary>
        public List<double[]> y = new List<double[]>();




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
        /// 构造一个空的矢量对象
        /// </summary>
        private ShapeGeometry() { }

        /// <summary>
        /// 使用矢量的类型构造一个空的矢量空间对象
        /// </summary>
        /// <param name="gtype"></param>
        public ShapeGeometry(LeiXing gtype)
        {
            GType = gtype;
        }

        public ShapeGeometry(double coor_x, double coor_y)
        {
            GType = LeiXing.Point;
            GNumber = 1;
            x.Add(new double[1] { coor_x });
            y.Add(new double[1] { coor_y });
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
            if (GType == LeiXing.Point)
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

        /// <summary>
        /// 转为点矢量
        /// </summary>
        /// <returns></returns>
        public ShapeGeometry[] ToPoint()
        {
            List<ShapeGeometry> geoms = new List<ShapeGeometry>();
            for (int i = 0; i < GNumber; i++)
            {
                for (int j = 0; j < x[i].Length; j++)
                {
                    ShapeGeometry geom = new ShapeGeometry(LeiXing.Point);
                    geom.AddOne(x[i][j], y[i][j]);
                    geoms.Add(geom);
                }
            }
            ShapeGeometry[] shapeGeometries = geoms.ToArray();
            geoms.Clear();
            return shapeGeometries;
        }

        ///// <summary>
        ///// 转为多点矢量
        ///// </summary>
        ///// <returns></returns>
        //public ShapeGeometry ToMultPoint()
        //{
        //    ShapeGeometry geom = new ShapeGeometry(LeiXing.MultPoint);
        //    geom.GNumber = GNumber;
        //    geom.x = x.ToList();
        //    geom.y = y.ToList();
        //    return geom;
        //}
        // 
        ///// <summary>
        ///// 转为线
        ///// </summary>
        ///// <returns></returns>
        //public ShapeGeometry ToLine()
        //{
        //    if (GType == LeiXing.Point | GType == LeiXing.MultPoint)
        //    {
        //        throw new Exception("Error: not support " + GType.ToString() + " to Multline");
        //    }
        //    else
        //    {
        //        ShapeGeometry geom = new ShapeGeometry(LeiXing.Line);
        //        for (int i = 0; i < GNumber; i++)
        //        {
        //            for (int j = 0; j < x[i].Length; j++)
        //            {
        //                geom.AddOne(x[i][])
        //            }
        //        }
        //        geom.GNumber = GNumber;
        //        geom.x = x.ToList();
        //        geom.y = y.ToList();
        //        return geom;
        //    }
        //}
        // 
        /// <summary>
        /// 转为多线
        /// </summary>
        /// <returns></returns>
        //public ShapeGeometry ToMultLine()
        //{
        //    if(GType == LeiXing.Point | GType == LeiXing.MultPoint)
        //    {
        //        throw new Exception("Error: not support " + GType.ToString() + " to Multline");
        //    }
        //    else
        //    {
        //        ShapeGeometry geom = new ShapeGeometry(LeiXing.MultLine);
        //        geom.GNumber = GNumber;
        //        geom.x = x.ToList();
        //        geom.y = y.ToList();
        //        return geom;
        //    }
        //}

    }

}
