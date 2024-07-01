using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 左右像片像对点
    /// </summary>
    class PointPair
    {
        #region 像片属性
        /// <summary>
        /// 像片x基线分量
        /// </summary>
        public double bx = 0;
        /// <summary>
        /// 像片主距
        /// </summary>
        public double f = 0;
        /// <summary>
        /// 像片像对点号
        /// </summary>
        public List<int> Number = new List<int>();
        /// <summary>
        /// 像点个数
        /// </summary>
        public int n = 0;
        #endregion

        #region 左像片属性
        /// <summary>
        /// 左像片像片名
        /// </summary>
        public string LeftPhoto = "";
        /// <summary>
        /// 左像片x坐标
        /// </summary>
        public List<double> x1 = new List<double>();
        /// <summary>
        /// 左像片y坐标
        /// </summary>
        public List<double> y1 = new List<double>();
        /// <summary>
        /// 左像片外方位元素
        /// </summary>
        public double[] ExternalElementsLeft = new double[6];
        /// <summary>
        /// 左像片像空间辅助X坐标
        /// </summary>
        public List<double> X1 = new List<double>();
        /// <summary>
        /// 左像片像空间辅助Y坐标
        /// </summary>
        public List<double> Y1 = new List<double>();
        /// <summary>
        /// 左像片像空间辅助Z坐标
        /// </summary>
        public List<double> Z1 = new List<double>();
        #endregion

        #region 右像片属性
        /// <summary>
        /// 右像片编号
        /// </summary>
        public string RightPhoto;
        /// <summary>
        /// 右像片x坐标
        /// </summary>
        public List<double> x2 = new List<double>();
        /// <summary>
        /// 右像片y坐标
        /// </summary>
        public List<double> y2 = new List<double>();
        /// <summary>
        /// 右像片外方位元素
        /// </summary>
        public double[] ExternalElementsRight = new double[6];
        /// <summary>
        /// 右像片像空间辅助X坐标
        /// </summary>
        public List<double> X2 = new List<double>();
        /// <summary>
        /// 右像片像空间辅助Y坐标
        /// </summary>
        public List<double> Y2 = new List<double>();
        /// <summary>
        /// 右像片像空间辅助Z坐标
        /// </summary>
        public List<double> Z2 = new List<double>();
        #endregion

        #region 模型点
        /// <summary>
        /// 左像片点投影系数
        /// </summary>
        public List<double> N1 = new List<double>();
        /// <summary>
        /// 右像片点投影系数
        /// </summary>
        public List<double> N2 = new List<double>();
        /// <summary>
        /// X坐标
        /// </summary>
        public List<double> X = new List<double>();
        /// <summary>
        /// Y坐标
        /// </summary>
        public List<double> Y = new List<double>();
        /// <summary>
        /// Z坐标
        /// </summary>
        public List<double> Z = new List<double>();
        #endregion

        /// <summary>
        /// 计算像空间辅助坐标
        /// </summary>
        public void CalXYZ()
        {
            N2 = new List<double>();
            X1 = new List<double>();
            Y1 = new List<double>();
            Z1 = new List<double>();
            X2 = new List<double>();
            Y2 = new List<double>();
            Z2 = new List<double>();
            X = new List<double>();
            Y = new List<double>();
            Z = new List<double>();

            // 像空间直角坐标系坐标
            Matrix xyf = new Matrix(3, 1);
            xyf.A[2,0] = -f;

            // 旋转矩阵
            Matrix R = new Matrix(3,3);//

            // 像辅坐标
            Matrix XYZ1 = new Matrix(3,1);
            Matrix XYZ2 = new Matrix(3,1);

            // 线元素
            Matrix XYZS1 = new Matrix(3, 1);
            Matrix XYZS2 = new Matrix(3, 1);

            string s1 = "";
            string s2 = "";

            // 遍历计算
            for (int i = 0; i < n; i++)
            {
                // 左像片
                R = CommonMethod.R(ExternalElementsLeft[3], ExternalElementsLeft[4],ExternalElementsLeft[5]);
                xyf.A[0, 0] = x1[i];
                xyf.A[1, 0] = y1[i];
                XYZ1 = R * xyf;
                X1.Add(XYZ1.A[0, 0]);
                Y1.Add(XYZ1.A[1, 0]);
                Z1.Add(XYZ1.A[2, 0]);
                s1 += Matrix.PrintA(!XYZ1, "{0:F6}\t");

                // 右像片
                R = CommonMethod.R(ExternalElementsRight[3], ExternalElementsRight[4], ExternalElementsRight[5]);
                xyf.A[0, 0] = x2[i];
                xyf.A[1, 0] = y2[i];
                XYZ2 = R * xyf;
                X2.Add(XYZ2.A[0, 0]);
                Y2.Add(XYZ2.A[1, 0]);
                Z2.Add(XYZ2.A[2, 0]);
                s2 += Matrix.PrintA(!XYZ2, "{0:F6}\t");

                // 基线分量B
                double bz = ExternalElementsRight[2];

                // 计算点投影系数
                N1.Add((bx * Z2[i] - bz * X2[i]) / (X1[i] * Z2[i] - X2[i] * Z1[i]));
                N2.Add((bx * Z1[i] - bz * X1[i]) / (X1[i] * Z2[i] - X2[i] * Z1[i]));

                // 计算模型点坐标
                X.Add(N1[i] * X1[i]);
                Y.Add(0.5 * (N1[i] * Y1[i] + N2[i] * Y2[i] + ExternalElementsRight[1]));
                Z.Add(N1[i] * Z1[i]);

            }
            string ss = "";
        }

        /// <summary>
        /// 计算模型点坐标
        /// </summary>
        public void SpaceFowardIntersection()
        {

        }


    }
}
