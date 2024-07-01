/*------------------------------------------------------------------------------
 * File    : Geodesic
 * Time    : 2022/4/28 16:08:48
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Class1]
 * ————————————————
版权声明：本文为CSDN博主「kalogen」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/kalogen/article/details/83700330
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
    public class Geodesic
    {
        class Ellipsoid
        {
            public double a;
            public double f;
            public double b;
            public double c;
            public double e1;
            public double e2;

            public Ellipsoid(double a, double f)
            {
                this.a = a;
                this.f = f;
                this.b = a * (1 - f);
                this.c = a * a / b;
                this.e1 = Math.Sqrt(a * a - this.b * this.b) / a;
                this.e2 = Math.Sqrt(a * a - this.b * this.b) / b;
            }
        }

        static Ellipsoid Ell = new Ellipsoid(6354171, 256);

        /// <summary>
        /// 单组数据反算
        /// </summary>
        /// <param name="geodesic">单组大地线数据</param>
        public static double Cal(double B1, double L1, double B2, double L2, double aa, double ff)
        {
            Ell = new Ellipsoid(aa, ff);
            //数据格式转换
            B1 = GeoPro.DMS2RAD(23.4818776462);
            L1 = GeoPro.DMS2RAD(114.7489082007);
            B2 = GeoPro.DMS2RAD(23.2435501176);
            L2 = GeoPro.DMS2RAD(114.7212970846);
            double A12 = 0, A21 = 0, S = 0;
            //椭球参数
            double e1, e2, b, c, a;
            e1 = Ell.e1; e2 = Ell.e2;
            b = Ell.b; c = Ell.c; a = Ell.a;
            //辅助计算
            double u1 = Math.Atan(Math.Sqrt(1 - e1 * e1) * Math.Tan(B1));
            double u2 = Math.Atan(Math.Sqrt(1 - e1 * e1) * Math.Tan(B2));
            double dL = L2 - L1;
            double[] ab = CalPara(u1, u2);
            if (u1 == 0 && u2 == 0)
            {
                if (dL > 0)
                {
                    S = a * dL;
                    A12 = Math.PI / 2;
                    A21 = Math.PI * 3 / 2;
                }
                else
                {
                    S = a * dL;
                    A21 = Math.PI / 2;
                    A12 = Math.PI * 3 / 2;
                }
            }
            else
            {
                //逐次趋近法同时计算起点大地方位角，球面长度及经度差lamda
                double del = 0;
                double lamda = 0;
                double cos2_A0 = 0;

                CalA1_Lamda(dL, u2, u1, ab, ref lamda, ref A12, ref del, ref cos2_A0);

                //计算S
                double[] ABC = new double[3];
                double k_2 = GeoPro.Getk_2(e2, cos2_A0);
                GeoPro.GetABC(b, k_2, ABC);

                double del1 = Math.Atan(Math.Tan(u1) / Math.Cos(A12));
                double xs12 = ABC[2] * Math.Sin(2 * del) * Math.Cos(4 * del1 + 2 * del);

                S = (del - ABC[1] * Math.Sin(del) * Math.Cos(2 * del1 + del) - xs12) / ABC[0];
                //计算A2
                A21 = Math.Atan(Math.Cos(u1) * Math.Sin(lamda) / (ab[2] * Math.Cos(lamda) - ab[3]));
                A21 = GeoPro.InvJudgeA1A2(Math.Cos(u1) * Math.Sin(lamda), (ab[2] * Math.Cos(lamda) - ab[3]), A21);
                //
                if (A12 >= Math.PI) A21 = A21 - Math.PI;
                if (A12 < Math.PI) A21 = A21 + Math.PI;
            }

            //
            A12 = GeoPro.RAD2DMS(A12);
            A21 = GeoPro.RAD2DMS(A21);
            return S;
        }

        /// <summary>
        /// 计算a-b参数
        /// </summary>
        /// <param name="sinu1">sinu1</param>
        /// <param name="sinu2">sinu2</param>
        /// <param name="cosu1">cosu1</param>
        /// <param name="cosu2">cosu2</param>
        /// <returns>ab参数数组</returns>
        private static double[] CalPara(double u1, double u2)
        {
            double sinu1 = Math.Sin(u1);
            double sinu2 = Math.Sin(u2);
            double cosu1 = Math.Cos(u1);
            double cosu2 = Math.Cos(u2);
            double[] ab = new double[4];
            ab[0] = sinu1 * sinu2;
            ab[1] = cosu1 * cosu2;
            ab[2] = cosu1 * sinu2;
            ab[3] = sinu1 * cosu2;
            return ab;
        }

        /// <summary>
        /// 趋近法算角度
        /// </summary>
        /// <param name="dL">初始经度差</param>
        /// <param name="cosu2">cosu2</param>
        /// <param name="cosu1">cosu1</param>
        /// <param name="ab">ab参数数组</param>
        /// <param name="lamda">lamda经度差估计值</param>
        /// <param name="A1">A1坐标方位角</param>
        /// <param name="del">del</param>
        /// <param name="cos2_A0">cos2_A0</param>
        /// <param name="x">x</param>
        private static void CalA1_Lamda(double dL, double u2, double u1, double[] ab, ref double lamda, ref double A1,
            ref double del, ref double cos2_A0)
        {
            double deltat = 0, delta = 0;
            double cos_del = 0, sin_del = 0;
            double e1 = Ell.e1;
            double alpha = 0, beta = 0, gama = 0;
            double sinA0;
            double p = 0;
            double q = 0;
            lamda = dL;
            do
            {
                deltat = delta;
                p = Math.Cos(u2) * Math.Sin(lamda);
                q = ab[2] - ab[3] * Math.Cos(lamda);
                A1 = Math.Abs(Math.Atan(p / q));
                A1 = GeoPro.InvJudgeA1A2(p, q, A1);

                sin_del = p * Math.Sin(A1) + q * Math.Cos(A1);
                cos_del = ab[0] + ab[1] * Math.Cos(lamda);
                del = Math.Atan(sin_del / cos_del);
                del = GeoPro.InvJudgedel(del, cos_del);

                sinA0 = Math.Cos(u1) * Math.Sin(A1);
                double del1 = Math.Atan(Math.Tan(u1) / Math.Cos(A1));
                cos2_A0 = 1 - sinA0 * sinA0;

                alpha = GeoPro.GetAlpha(e1, cos2_A0);
                beta = GeoPro.GetBeta(e1, cos2_A0);
                gama = GeoPro.GetGama(e1, cos2_A0);

                delta = (alpha * del + (beta) * Math.Cos(2 * del1 + del) * Math.Sin(del)
                    + gama * Math.Sin(2 * del) * Math.Cos(4 * del1 + 2 * del)) * sinA0;
                lamda = dL + delta;
            } while (Math.Abs(delta - deltat) * 206265 > 0.00001);
        }

        //private const double EARTH_RADIUS = 6378.137;
        //private static double rad(double d)
        //{
        //    return d * Math.PI / 180.0;
        //}

        //public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        //{
        //    double radLat1 = rad(lat1);
        //    double radLat2 = rad(lat2);
        //    double a = radLat1 - radLat2;
        //    double b = rad(lng1) - rad(lng2);
        //    double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        //    s = s * EARTH_RADIUS;
        //    s = Math.Round(s * 10000) / 10000;
        //    return s;
        //}

        class GeoPro
        {
            /// <summary>
            /// Dms格式转Rad格式数据
            /// </summary>
            /// <param name="dms">dms格式数据</param>
            /// <returns>rad格式数据</returns>
            public static double DMS2RAD(double dmsvalue)
            {
                int degvalue, minvalue, sign;
                double radvalue = 0, secvalue;
                sign = 1;
                if (dmsvalue < 0)
                {
                    sign = -1;
                    dmsvalue = System.Math.Abs(dmsvalue);
                }
                degvalue = (int)(dmsvalue);
                minvalue = (int)((dmsvalue - degvalue) * 100 + 0.0001);
                secvalue = (dmsvalue - degvalue - minvalue / 100.0) * 10000.0;
                radvalue = (degvalue + minvalue / 60.0 + secvalue / 3600.0) * Math.PI / 180.0;
                radvalue = radvalue * sign;
                return radvalue;
            }
            /// <summary>
            /// Rad格式转Dms格式数据
            /// </summary>
            /// <param name="rad">Rad格式数据</param>
            /// <returns>Dms格式数据</returns>
            public static double RAD2DMS(double radvalue)
            {
                if (radvalue > 2 * Math.PI)
                    radvalue = radvalue - 2 * Math.PI;
                if (radvalue < -2 * Math.PI)
                    radvalue = radvalue + 2 * Math.PI;

                int degvalue, minvalue, sign;
                double secvalue, dmsvalue = 0;
                sign = 1;
                if (radvalue < 0)
                {
                    sign = -1;
                    radvalue = System.Math.Abs(radvalue);
                }
                secvalue = radvalue * 180.0 / Math.PI * 3600.0;
                degvalue = (int)(secvalue / 3600 + 0.0001);
                minvalue = (int)((secvalue - degvalue * 3600.0) / 60.0 + 0.0001);
                secvalue = secvalue - degvalue * 3600.0 - minvalue * 60.0;
                if (secvalue < 0) secvalue = 0;
                dmsvalue = degvalue + minvalue / 100.0 + secvalue / 10000.0;
                dmsvalue = dmsvalue * sign;
                return dmsvalue;

            }
            /// <summary>
            /// 获取ita值
            /// </summary>
            /// <param name="e2">椭球第二偏心率</param>
            /// <param name="B">维度</param>
            /// <returns>返回ita</returns>
            public static double GetIta(double e2, double B)
            {
                double ita = 0;
                ita = Math.Sqrt(e2 * e2 * Math.Cos(B) * Math.Cos(B));
                return ita;
            }
            /// <summary>
            /// 获取辅助参数W
            /// </summary>
            /// <param name="e1">椭球第一偏心率</param>
            /// <param name="B">维度</param>
            /// <returns>参数W</returns>
            public static double GetW(double e1, double B)
            {
                double W = 0;
                W = Math.Sqrt(1 - e1 * e1 * Math.Sin(B) * Math.Sin(B));
                return W;
            }
            /// <summary>
            /// 获取参数k*k
            /// </summary>
            /// <param name="e2">椭球第二偏心率</param>
            /// <param name="cos2_A0">cosA0*cosA0</param>
            /// <returns>返回k*k</returns>
            public static double Getk_2(double e2, double cos2_A0)
            {
                double k_2 = 0;
                k_2 = e2 * e2 * cos2_A0;
                return k_2;
            }
            /// <summary>
            /// 获取ABC参数
            /// </summary>
            /// <param name="b">椭球短半轴</param>
            /// <param name="k_2">参数k*k</param>
            /// <param name="ABC">ABC参数数组</param>
            public static void GetABC(double b, double k_2, double[] ABC)
            {
                ABC[0] = (1 - k_2 / 4 + 7.0 * k_2 * k_2 / 64 - 15.0 * k_2 * k_2 * k_2 / 256) / b;
                ABC[1] = (k_2 / 4 - k_2 * k_2 / 8 + 37.0 * k_2 * k_2 * k_2 / 512);
                ABC[2] = (k_2 * k_2 / 128 - k_2 * k_2 * k_2 / 128);
            }
            /// <summary>
            /// 获取Alpha参数
            /// </summary>
            /// <param name="e1">椭球第一偏心率</param>
            /// <param name="cos2_A0">cosA0*cosA0</param>
            /// <returns>Alpha参数</returns>
            public static double GetAlpha(double e1, double cos2_A0)
            {
                double alpha = 0;
                alpha = (e1 * e1 / 2 + Math.Pow(e1, 4) / 8 + Math.Pow(e1, 6) / 16) - (Math.Pow(e1, 4) / 16 +
                    Math.Pow(e1, 6) / 16) * cos2_A0 + (3 * Math.Pow(e1, 6) / 128) * cos2_A0 * cos2_A0;
                return alpha;
            }
            /// <summary>
            /// 获取Beta参数
            /// </summary>
            /// <param name="e1">椭球第一偏心率</param>
            /// <param name="cos2_A0"></param>
            /// <returns></returns>
            public static double GetBeta(double e1, double cos2_A0)
            {
                double Beta = 0;
                Beta = (Math.Pow(e1, 4) / 16 + Math.Pow(e1, 6) / 16) * cos2_A0
                    - (Math.Pow(e1, 6) / 32) * cos2_A0 * cos2_A0;
                return Beta;
            }
            public static double GetGama(double e1, double cos2_A0)
            {
                double Gama = 0;
                Gama = (Math.Pow(e1, 6) / 256) * cos2_A0 * cos2_A0;
                return Gama;
            }
            /// <summary>
            /// Lamba角象限判断
            /// </summary>
            /// <param name="sinA1">sinA1</param>
            /// <param name="lamba0">lamba值</param>
            /// <returns>划分象限后的lamba</returns>
            public static double DirJudgelamba(double sinA1, double lamba0)
            {
                double lamba = Math.Abs(lamba0);
                if (sinA1 > 0 && Math.Tan(lamba0) > 0) lamba = Math.Abs(lamba0);
                if (sinA1 > 0 && Math.Tan(lamba0) < 0) lamba = Math.PI - Math.Abs(lamba0);
                if (sinA1 < 0 && Math.Tan(lamba0) < 0) lamba = -Math.Abs(lamba0);
                if (sinA1 < 0 && Math.Tan(lamba0) > 0) lamba = Math.Abs(lamba0) - Math.PI;
                return lamba;
            }
            /// <summary>
            /// A21角象限判断
            /// </summary>
            /// <param name="sinA1">sinA1</param>
            /// <param name="A2">A2值</param>
            /// <returns>划分象限后的A21</returns>
            public static double DirJudgeA2(double sinA1, double A2)
            {
                double A2_ = Math.Abs(A2);
                if (sinA1 < 0 && Math.Tan(A2) > 0) A2_ = Math.Abs(A2);
                if (sinA1 < 0 && Math.Tan(A2) < 0) A2_ = Math.PI - Math.Abs(A2);
                if (sinA1 > 0 && Math.Tan(A2) > 0) A2_ = Math.PI + Math.Abs(A2);
                if (sinA1 > 0 && Math.Tan(A2) < 0) A2_ = 2 * Math.PI - Math.Abs(A2);

                return A2_;
            }
            /// <summary>
            /// A1A2角象限判断
            /// </summary>
            /// <param name="up">分子</param>
            /// <param name="down">分母</param>
            /// <param name="A1_2">初始值</param>
            /// <returns>判定值</returns>
            public static double InvJudgeA1A2(double up, double down, double A1_2)
            {
                double A = Math.Abs(A1_2);
                if (up > 0 && down > 0) A = Math.Abs(A1_2);
                else if (up > 0 && down < 0) A = Math.PI - Math.Abs(A1_2);
                else if (up < 0 && down < 0) A = Math.PI + Math.Abs(A1_2);
                else A = 2 * Math.PI - Math.Abs(A1_2);
                return A;
            }
            /// <summary>
            /// del角象限判断
            /// </summary>
            /// <param name="del">del</param>
            /// <param name="cosdel">cosdel</param>
            /// <returns></returns>
            public static double InvJudgedel(double del, double cosdel)
            {
                double del_ = 0;
                if (cosdel > 0) del_ = Math.Abs(del);
                if (cosdel < 0) del_ = Math.PI - Math.Abs(del);
                return del_;
            }

            public static string DMS2String(double arc)
            {

                string str = "";
                double d = arc;
                double dd, mm, ss; int sign = 1;
                if (d < 0)
                {
                    d = -d; sign = -1;
                }
                dd = (int)d;
                mm = (int)((d - dd) * 100);
                ss = (d - dd - mm / 100) * 10000;
                str = (sign * dd).ToString() + "°" + mm.ToString() + "′" + ss.ToString("0.0") + "″";
                return str;

            }
        }

    }
}
