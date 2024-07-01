using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 常用计算函数
    /// </summary>
    class CommonMethod
    {
        /// <summary>
        /// 计算旋转矩阵
        /// </summary>
        /// <param name="phi">φ</param>
        /// <param name="omega">ω</param>
        /// <param name="kappa">κ</param>
        /// <returns>旋转矩阵</returns>
        public static Matrix R(double phi, double omega, double kappa)
        {
            double[,] r = new double[3, 3];

            r[0, 0] = Math.Cos(phi) * Math.Cos(kappa) - Math.Sin(phi) * Math.Sin(omega) * Math.Sin(kappa);
            r[0, 1] = -Math.Cos(phi) * Math.Sin(kappa) - Math.Sin(phi) * Math.Sin(omega) * Math.Cos(kappa);
            r[0, 2] = -Math.Sin(phi) * Math.Cos(omega);
            r[1, 0] = Math.Cos(omega) * Math.Sin(kappa);
            r[1, 1] = Math.Cos(omega) * Math.Cos(kappa);
            r[1, 2] = -Math.Sin(omega);
            r[2, 0] = Math.Sin(phi) * Math.Cos(kappa) + Math.Cos(phi) * Math.Sin(omega) * Math.Sin(kappa);
            r[2, 1] = -Math.Sin(phi) * Math.Sin(kappa) + Math.Cos(phi) * Math.Sin(omega) * Math.Cos(kappa);
            r[2, 2] = Math.Cos(phi) * Math.Cos(omega);

            return new Matrix(r);
        }

    }
}
