using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PhotogrammetryWFA
{
    /// <summary>
    /// DEM内插计算
    /// </summary>
    class DEMInter:IndirectAdjustment
    {
        public List<double[]> DataList = new List<double[]>();

        /// <summary>
        /// 输入DEM原始坐标数据，进行DEM内插计算
        /// </summary>
        /// <param name="dt"></param>
        public DEMInter(List<double[]> dataList, int n, int t):base(n, t)
        {
            DataList = dataList;
        }

        /// <summary>
        /// 平差计算
        /// </summary>
        /// <param name="dt"></param>
        public double Calculate(double xx, double yy)
        {
            // 构造系数阵 B 和 常数项 l
            for(int i=0;i<n;i++ )
            {
                double X_ = DataList[i][0] - xx;
                double Y_ = DataList[i][1] - yy;

                // 系数阵
                B.A[i, 0] = X_ * X_;
                B.A[i, 1] = X_ * Y_;
                B.A[i, 2] = Y_ * Y_;
                B.A[i, 3] = X_;
                B.A[i, 4] = Y_;
                B.A[i, 5] = 1;

                // 常数项
                l.A[i, 0] = DataList[i][2];

                // 权阵
                P.A[i, i] = 1 / Math.Sqrt(X_ * X_ + Y_ * Y_);
            }

            // 平差
            x = ~(!B * P * B) * !B * P * l;

            return x.A[5, 0];
        }

    }
}
