/*------------------------------------------------------------------------------
 * File    : EllipsoidPloyArea
 * Time    : 2022/4/28 19:53:08
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[EllipsoidPloyArea]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
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
}
