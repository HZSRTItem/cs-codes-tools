using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 输入观测值数量n和必要观测数t，重载计算函数，得到间接平差结果
    /// </summary>
    class IndirectAdjustment
    {

        #region 平差基本量

        /// <summary>
        /// 观测值数量
        /// </summary>
        public int n = 0;
        /// <summary>
        /// 必要观测数
        /// </summary>
        public int t = 0;
        /// <summary>
        /// 常数项 观测 - 近似(共线方程)    n*1
        /// </summary>
        public Matrix l = new Matrix();
        /// <summary>
        /// 观测值改正数 v = Bx - l        n*1
        /// </summary>
        public Matrix v = new Matrix();
        /// <summary>
        /// 观测值                         n*1
        /// </summary>
        public Matrix L0 = new Matrix();
        /// <summary>
        /// 观测值的近似值 L = L0 + v;      n*1
        /// </summary>
        public Matrix L = new Matrix();
        /// <summary>
        /// 系数                           n*t
        /// </summary>
        public Matrix B = new Matrix();
        /// <summary>
        /// 观测值的权                      n*n
        /// </summary>
        public Matrix P = new Matrix();
        /// <summary>
        /// 参数改正数                     t*1
        /// </summary>
        public Matrix x = new Matrix();
        /// <summary>
        /// 待求参数                       t*1
        /// </summary>
        public Matrix X = new Matrix();
        /// <summary>
        /// 法方程系数                     t*t
        /// </summary>
        public Matrix N = new Matrix();

        #endregion

        /// <summary>
        /// 间接平差构造函数
        /// </summary>
        /// <param name="n">必要观测数</param>
        /// <param name="t">多于观测数</param>
        public IndirectAdjustment(int n, int t)
        {
            this.n = n;
            this.t = t;

            l = new Matrix(n, 1);
            v = new Matrix(n, 1);
            L0 = new Matrix(n, 1);
            L = new Matrix(n, 1);
            B = new Matrix(n, t);
            P = new Matrix(n, n);
            X = new Matrix(t, 1);
            x = new Matrix(t, 1);
        }

        /// <summary>
        /// 解算
        /// </summary>
        /// <param name="Ei">迭代精度</param>
        public virtual void Fit(double E)
        {
            int i = 0;
            CalX0();                // 定初值
            double e = 10E20;       // 迭代精度
            while (e > E)           // 迭代计算
            {                       // 
                                    // 构建误差方程
                CalL();             // 计算观测值的近似值
                CalP();             // 计算观测值的权，默认为单位阵
                CalB();             // 计算误差方程系数                
                l = L0 - L;         // 计算误差方程常数项，观测 - 近似
                                    // 构建法方程
                N = ~(!B * P * B);  // 法方程系数的逆，即参数的协因数阵
                x = N * !B * P * l; // 参数改正数
                X = X + x;          // 参数改正
                v = B * x - l;      // 计算观测量改正数
                L0 = L0 + v;        // 观测量改正
                                    // 
                e = ei(x);          // 计算迭代精度
                i++;
                if(i>1000)
                {
                    break;
                }
            }                       // 
            double s = 0;
        }

        /// <summary>
        /// 参数的初值
        /// </summary>
        public virtual void CalX0() { }

        /// <summary>
        /// 观测值的近似值
        /// </summary>
        public virtual void CalL() { }

        /// <summary>
        /// 误差方程系数矩阵
        /// </summary>
        public virtual void CalB() { }

        /// <summary>
        /// 观测值的权
        /// </summary>
        public virtual void CalP()
        {
            int i = 0;
            for (i = 0; i < n; i++)
            {
                P.A[i, i] = 1;
            }
        }

        /// <summary>
        /// 求改正数绝对值最大
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private double ei(Matrix a)
        {
            double max = 0;
            for (int i = 0; i < a.m; i++)
            {
                max = Math.Abs(a.A[i, 0]) > max ? Math.Abs(a.A[i, 0]) : max;
            }
            return max;
        }
    }
}
