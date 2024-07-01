/*------------------------------------------------------------------------------
 * File    : utils
 * Time    : 2022/4/28 19:50:58
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[utils]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{

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

        public static int SamplesUniformSpace(List<double> x, List<double> y, List<double> u_x, List<double> u_y, double density)
        {
            int n_sample = x.Count > y.Count ? y.Count : x.Count;
            // 计算四个最大值
            double xmin = x.Min();
            double xmax = x.Max();
            double ymin = y.Min();
            double ymax = y.Max();
            // 计算网格宽度
            double grid_w = Math.Sqrt(density);
            int m = (int)((xmax - xmin) / grid_w);
            int n = (int)((ymax - ymin) / grid_w);
            Random random = new Random();
            bool[,] grid_is = new bool[m, n];
            while (n_sample > 0)
            {
                int i = random.Next(x.Count);
                double x0 = x[i];
                double y0 = y[i];
                int m0 = (int)((xmax - x0) / grid_w);
                int n0 = (int)((ymax - y0) / grid_w);
                n_sample--;
                x.RemoveAt(i);
                y.RemoveAt(i);
                if (!grid_is[m0, n0])
                {
                    u_x.Add(x[i]);
                    u_y.Add(y[i]);
                    grid_is[m0, n0] = true;
                }
            }
            return u_x.Count;
        }
    }
}
