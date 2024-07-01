using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 输入二维矩阵数据，实现矩阵 + - * ^-1 T 输出计算后的矩阵
    /// </summary>
    class Matrix
    {
        /// <summary>
        /// 矩阵行数
        /// </summary>
        public int m = 0;  
        /// <summary>
        /// 矩阵列数
        /// </summary>
        public int n = 0;  
        /// <summary>
        /// 矩阵数据
        /// </summary>
        public double[,] A;

        /// <summary>
        /// 构造m行n列数据为A的矩阵
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        /// <param name="A">数据</param>
        public Matrix(double[,] A)
        {
            this.m = A.GetLength(0);
            this.n = A.GetLength(1);
            this.A = A;
        }

        /// <summary>
        /// 构造m行n列零矩阵
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            this.A = new double[m, n];
        }

        /// <summary>
        /// 构造零空矩阵
        /// </summary>
        public Matrix()
        {
            m = 0;
            n = 0;
            A = new double[0, 0];
        }

        /// <summary>
        /// A + B
        /// </summary>
        /// <param name="A">矩阵</param>
        /// <param name="B">矩阵</param>
        /// <returns>矩阵</returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            // 不同维度检测
            if (A.m != B.m | A.n != B.n)
            {
                return new Matrix(0, 0);
            }

            double[,] ab = new double[A.m, A.n];

            // 计算
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    ab[i, j] = A.A[i, j] + B.A[i, j];
                }
            }

            return new Matrix(ab);
        }

        /// <summary>
        /// A - B
        /// </summary>
        /// <param name="A">矩阵</param>
        /// <param name="B">矩阵</param>
        /// <returns>矩阵</returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            // 不同维度检测
            if (A.m != B.m | A.n != B.n)
            {
                return new Matrix(0, 0);
            }

            double[,] ab = new double[A.m, A.n];

            // 计算
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    ab[i, j] = A.A[i, j] - B.A[i, j];
                }
            }

            return new Matrix(ab);
        }

        /// <summary>
        /// A * B
        /// </summary>
        /// <param name="A">矩阵</param>
        /// <param name="B">矩阵</param>
        /// <returns>矩阵</returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            // 不同维度检测
            if (A.n != B.m)
            {
                return new Matrix(0, 0);
            }

            double[,] ab = new double[A.m, B.n];

            // 计算
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < B.n; j++)
                {
                    for (int k = 0; k < A.n; k++)
                    {
                        ab[i, j] += A.A[i, k] * B.A[k, j];
                    }
                }
            }

            return new Matrix(ab);
        }

        /// <summary>
        /// 转置
        /// </summary>
        /// <param name="A">矩阵</param>
        /// <returns>转置后矩阵</returns>
        public static Matrix operator !(Matrix A)
        {
            double[,] ab = new double[A.n, A.m];
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    ab[i, j] = A.A[j, i];
                }
            }

            return new Matrix(ab);
        }

        /// <summary>
        /// 求逆
        /// </summary>
        /// <param name="A">方阵</param>
        /// <returns>求逆后矩阵</returns>
        public static Matrix operator ~(Matrix A)
        {
            // 矩阵方阵检测
            if (A.m != A.n)
            {
                return new Matrix(0, 0);
            }

            int n = A.m;                         // 矩阵维度
            double[,] AE = new double[n, 2 * n]; // 变换矩阵
            int i = 0, j = 0, k = 0;             // 循环变量

            // 赋值
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < 2 * n; j++)
                {
                    if (j < n)
                    {
                        AE[i, j] = A.A[i, j];
                    }
                    else
                    {
                        if (i == j - n)
                        {
                            AE[i, j] = 1;
                        }
                    }
                }
            }

            // 消成上三角
            for (i = 0; i < n; i++)
            {
                // 找i到n行第i列不为零的一行
                int jNot0 = -1;
                for (j = i; j < n; j++)
                {
                    if (AE[j, i] != 0)
                    {
                        jNot0 = j;
                        break;
                    }
                }

                if (jNot0 == -1)
                {
                    // 矩阵奇异
                    return new Matrix(0, 0);
                }

                // 所有行加第jNot0行
                for (j = 0; j < n; j++)
                {
                    for (k = 0; k < 2 * n; k++)
                    {
                        AE[j, k] += AE[jNot0, k];
                    }
                }

                // 开始消成下三角
                for (j = i; j < n; j++)
                {
                    for (k = 2 * n - 1; k >= i; k--)
                    {
                        if (j == i)
                        {
                            AE[j, k] = AE[j, k] / AE[j, i];
                        }
                        else
                        {
                            AE[j, k] = AE[j, k] / AE[j, i] - AE[i, k];
                        }
                    }
                    
                }
            }

            // 反消上三角
            for (i = n - 1; i > 0; i--)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    for (k = 2 * n - 1; k >= 0; k--)
                    {
                        AE[j, k] = AE[j, k] - AE[i, k] * AE[j, i];
                    }
                }
            }

            // 取出求逆后的矩阵
            Matrix aa = new Matrix(n, n);
            for (i = 0; i < n; i++)
            {
                for (j = n; j < 2 * n; j++)
                {
                    aa.A[i, j - n] = AE[i, j];
                }
            }

            return aa;
        }

        /// <summary>
        /// 输出矩阵
        /// </summary>
        /// <param name="A">矩阵</param>
        /// <returns>矩阵字符串</returns>
        public static string PrintA(Matrix A, string format)
        {
            string strA = "";
            for (int i = 0; i < A.m; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    strA += string.Format(format, A.A[i, j]);
                }
                strA += "\n";
            }
            strA += "\n";
            return strA;
        }
    }
}
