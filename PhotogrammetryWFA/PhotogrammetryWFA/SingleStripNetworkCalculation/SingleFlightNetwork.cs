using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 单航带区域网概算
    /// </summary>
    class SingleFlightNetwork
    {
        #region 属性
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath = "";
        /// <summary>
        /// 所有计算报告
        /// </summary>
        public string Report = "";
        /// <summary>
        /// 像片主距
        /// </summary>
        public double f = 0;
        /// <summary>
        /// 基线x分量
        /// </summary>
        public double bx = 0;
        /// <summary>
        /// 像对数据
        /// </summary>
        public List<PointPair> PhotoPair = new List<PointPair>();
        /// <summary>
        /// 像点名
        /// </summary>
        public List<string> PhotoPointName = new List<string>();
        /// <summary>
        /// 地面控制点列表
        /// </summary>
        public List<GroundPoint> ControlPoint = new List<GroundPoint>();
        /// <summary>
        /// 地面检查点列表
        /// </summary>
        public List<GroundPoint> CheckPoint = new List<GroundPoint>();
        /// <summary>
        /// 所有点
        /// </summary>
        public GroundPoint[] AllPoint;

        #endregion  


        public SingleFlightNetwork()
        {

        }

        /// <summary>
        /// 全部计算函数
        /// </summary>
        public void Calculate()
        {
            // 所有坐标点
            AllPoint = new GroundPoint[PhotoPointName.Count];
            for (int i = 0; i < PhotoPointName.Count; i++)
            {
                AllPoint[i] = new GroundPoint();
            }

            // 1 连续法相对定向
            Report1();

            // 2 统一航带网模型建立
            Report2();

            // 3 绝对定向
            Report3();
        }

        /// <summary>
        /// 第一部分：连续法相对定向
        /// </summary>
        private void Report1()
        {
            #region 头
            double xs1 = 0;
            double ys1 = 0;
            double zs1 = 0;

            double xs2 = 0;
            double ys2 = 0;
            double zs2 = 0;

            double phi1 = 0;
            double omega1 = 0;
            double kappa1 = 0;

            double phi2 = 0;
            double omega2 = 0;
            double kappa2 = 0;

            Matrix B = new Matrix();
            Matrix l = new Matrix();
            Matrix x = new Matrix(5, 1);
            Matrix X = new Matrix(5, 1);
            #endregion 

            // 遍历所有像对点，进行连续法定向
            for (int i = 0; i < PhotoPair.Count; i++)
            {
                #region 平差
                PointPair PhotoPoint = PhotoPair[i];
                int n = PhotoPair[i].n;

                // 角换一下
                xs1 = bx;
                ys1 = X.A[0, 0] * bx;
                zs1 = X.A[1, 0] * bx;
                phi1 = X.A[2, 0];
                omega1 = X.A[3, 0];
                kappa1 = X.A[4, 0];

                phi2 = 0;
                omega2 = 0;
                kappa2 = 0;

                // 初始平差参数
                B = new Matrix(n, 5);
                l = new Matrix(n, 1);
                x = new Matrix(5, 1);
                X = new Matrix(5, 1);

                // 像空间直角坐标系坐标
                Matrix xyf = new Matrix(3, 1);
                xyf.A[2, 0] = -f;

                // 旋转矩阵
                Matrix R = new Matrix(3, 3);//

                // 像辅坐标
                Matrix XYZ1 = new Matrix(3, 1);
                Matrix XYZ2 = new Matrix(3, 1);

                // 线元素
                Matrix XYZS1 = new Matrix(3, 1);
                Matrix XYZS2 = new Matrix(3, 1);

                double ei=0;

                while (ei < 1000)
                {
                    ei++;

                    // 转一次，改一次
                    phi2 = X.A[2, 0];
                    omega2 = X.A[3, 0];
                    kappa2 = X.A[4, 0];

                    // 循环所有的像对
                    // 先构造 B 可以 一块构造
                    for (int k = 0; k < PhotoPoint.n; k++)
                    {
                        // 左像片
                        R = CommonMethod.R(phi1, omega1, kappa1);
                        xyf.A[0, 0] = PhotoPoint.x1[k];
                        xyf.A[1, 0] = PhotoPoint.y1[k];
                        XYZ1 = R * xyf;
                        double X1 = XYZ1.A[0, 0];
                        double Y1 = XYZ1.A[1, 0];
                        double Z1 = XYZ1.A[2, 0];

                        // 右像片
                        R = CommonMethod.R(phi2, omega2, kappa2);
                        xyf.A[0, 0] = PhotoPoint.x2[k];
                        xyf.A[1, 0] = PhotoPoint.y2[k];
                        XYZ2 = R * xyf;
                        double X2 = XYZ2.A[0, 0];
                        double Y2 = XYZ2.A[1, 0];
                        double Z2 = XYZ2.A[2, 0];

                        // 基线分量B
                        double bz = X.A[1, 0] * bx;
                        double by = X.A[0, 0] * bx;
                        xs2 = bx;
                        ys2 = by;
                        zs2 = bz;

                        // 计算点投影系数
                        double N1 = (bx * Z2 - bz * X2) / (X1 * Z2 - X2 * Z1);
                        double N2 = (bx * Z1 - bz * X1) / (X1 * Z2 - X2 * Z1);

                        B.A[k, 0] = bx;
                        B.A[k, 1] = -Y2 / Z2 * bx;
                        B.A[k, 2] = -X2 * Y2 / Z2 * N2;
                        B.A[k, 3] = -(Z2 + Y2 * Y2 / Z2) * N2;
                        B.A[k, 4] = X2 * N2;

                        l.A[k, 0] = N1 * Y1 - N2 * Y2 - by;

                    }

                    x = ~(!B * B) * !B * l;
                    X = X + x;


                }

                #endregion

                #region 算一算

                // 左片的外方位元素
                PhotoPoint.ExternalElementsLeft[0] = xs1;
                PhotoPoint.ExternalElementsLeft[1] = ys1;
                PhotoPoint.ExternalElementsLeft[2] = zs1;
                PhotoPoint.ExternalElementsLeft[3] = phi1;
                PhotoPoint.ExternalElementsLeft[4] = omega1;
                PhotoPoint.ExternalElementsLeft[5] = kappa1;

                // 右片的外方位元素
                PhotoPoint.ExternalElementsRight[0] = xs2;
                PhotoPoint.ExternalElementsRight[1] = ys2;
                PhotoPoint.ExternalElementsRight[2] = zs2;
                PhotoPoint.ExternalElementsRight[3] = phi2;
                PhotoPoint.ExternalElementsRight[4] = omega2;
                PhotoPoint.ExternalElementsRight[5] = kappa2;

                // 计算点投影系数和模型点坐标
                for (int k = 0; k < PhotoPoint.n; k++)
                {
                    // 左像片
                    R = CommonMethod.R(phi1, omega1, kappa1);
                    xyf.A[0, 0] = PhotoPoint.x1[k];
                    xyf.A[1, 0] = PhotoPoint.y1[k];
                    XYZ1 = R * xyf;
                    PhotoPoint.X1.Add(XYZ1.A[0, 0]);
                    PhotoPoint.Y1.Add(XYZ1.A[1, 0]);
                    PhotoPoint.Z1.Add(XYZ1.A[2, 0]);

                    // 右像片
                    R = CommonMethod.R(phi2, omega2, kappa2);
                    xyf.A[0, 0] = PhotoPoint.x2[k];
                    xyf.A[1, 0] = PhotoPoint.y2[k];
                    XYZ2 = R * xyf;
                    PhotoPoint.X2.Add(XYZ2.A[0, 0]);
                    PhotoPoint.Y2.Add(XYZ2.A[1, 0]);
                    PhotoPoint.Z2.Add(XYZ2.A[2, 0]);

                    // 基线分量B
                    double bz = X.A[1, 0] * bx;
                    double by = X.A[0, 0] * bx;


                    // 计算点投影系数
                    PhotoPoint.N1.Add((bx * PhotoPoint.Z2[k] - bz * PhotoPoint.X2[k]) / (PhotoPoint.X1[k] * PhotoPoint.Z2[k] - PhotoPoint.X2[k] * PhotoPoint.Z1[k]));
                    PhotoPoint.N2.Add((bx * PhotoPoint.Z1[k] - bz * PhotoPoint.X1[k]) / (PhotoPoint.X1[k] * PhotoPoint.Z2[k] - PhotoPoint.X2[k] * PhotoPoint.Z1[k]));

                    PhotoPoint.X.Add(PhotoPoint.N1[k] * PhotoPoint.X1[k]);
                    PhotoPoint.Y.Add(0.5 * (PhotoPoint.N1[k] * PhotoPoint.Y1[k] + PhotoPoint.N2[k] * PhotoPoint.Y2[k] + by));
                    PhotoPoint.Z.Add(PhotoPoint.N1[k] * PhotoPoint.Z1[k]);

                }
                #endregion
            }

            #region 出来

            Report += "\n# * * * * * * * * * * * * * * * * * * * * * * * * * * 连续法相对定向 * * * * * * *";
            Report += "* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * #\n\n";

            // 像对，点名，像辅坐标，点投影系数，模型点坐标
            for(int i=0;i<PhotoPair.Count;i++)
            {
                Report += string.Format("像对 : {0}         {1}\n", PhotoPair[i].LeftPhoto, PhotoPair[i].RightPhoto);
                Report += string.Format("相对定向元素 : by bz φ ω κ\n");
                Report += string.Format("{0, -10:F6}\t", PhotoPair[i].ExternalElementsRight[0]);
                Report += string.Format("{0, -10:F6}\t", PhotoPair[i].ExternalElementsRight[1]);
                Report += string.Format("{0, -10:F6}\t", PhotoPair[i].ExternalElementsRight[2]);
                Report += string.Format("{0, -10:F6}\t", PhotoPair[i].ExternalElementsRight[3]);
                Report += string.Format("{0, -10:F6}\n", PhotoPair[i].ExternalElementsRight[4]);

                Report += string.Format("{0, -4}\t", "点名");
                Report += string.Format("{0, -10}\t", "X1(m)");
                Report += string.Format("{0, -10}\t", "Y1(m)");
                Report += string.Format("{0, -10}\t", "Z1(m)");
                Report += string.Format("{0, -10}\t", "N1(m)");
                Report += string.Format("{0, -10}\t", "X2(m)");
                Report += string.Format("{0, -10}\t", "Y2(m)");
                Report += string.Format("{0, -10}\t", "Z2(m)");
                Report += string.Format("{0, -10}\t", "N2(m)");
                Report += string.Format("{0, -10}\t", "XA(m)");
                Report += string.Format("{0, -10}\t", "YA(m)");
                Report += string.Format("{0, -10}\n", "ZA(m)");

                for(int j=0;j<PhotoPair[i].n;j++    )
                {
                    Report += string.Format("{0, -6}\t", PhotoPointName[PhotoPair[i].Number[j]]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].X1[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].Y1[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].Z1[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].N1[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].X2[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].Y2[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].Z2[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].N2[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].X[j]);
                    Report += string.Format("{0, -10:F6}\t", PhotoPair[i].Y[j]);
                    Report += string.Format("{0, -10:F6}\n", PhotoPair[i].Z[j]);
                }
                Report += "----------------------------------------------------------------------";
                Report += "----------------------------------------------------------------------";
                Report += "------------\n\n";
            }
            #endregion
        }

        /// <summary>
        /// 第二部分统一航带网建立
        /// 求出航带内各模型点在航带统一坐标系（第一个像片的像空间坐标系）中的坐标
        /// </summary>
        private void Report2()
        {

            #region 归一下坐标
            // 找到连接点，求比例尺归化系数
            for (int i = 0; i < PhotoPair.Count - 1; i++)
            {

                // 比例尺归化系数
                double K = 0;
                double iK = 0;

                // 遍历 i 和 i+1 像对 找到同名像点
                for (int j = 0; j < PhotoPair[i].n; j++)
                {
                    for (int k = 0; k < PhotoPair[i + 1].n; k++)
                    {
                        // 找到了
                        if (PhotoPair[i].Number[j] == PhotoPair[i + 1].Number[k])
                        {
                            // 标一下
                            
                            AllPoint[PhotoPair[i].Number[j]].Style = PhotoPair[i].RightPhoto + "连接点";

                            // 记一下
                            iK++;

                            // 算一下
                            double ZA = PhotoPair[i].Z[j] - PhotoPair[i].ExternalElementsRight[2];
                            double ZB = PhotoPair[i + 1].Z[k];
                            K += ZA / ZB;
                        }
                    }
                }

                // 取平均算出来了
                K /= iK;

                // 使用比例尺归化系数去乘后左像片的模型点坐标

                // 归外方位元素 右归左不归 

                PhotoPair[i + 1].ExternalElementsRight[0] *= K;
                PhotoPair[i + 1].ExternalElementsRight[1] *= K;
                PhotoPair[i + 1].ExternalElementsRight[2] *= K;

                PhotoPair[i + 1].ExternalElementsRight[0] += PhotoPair[i].ExternalElementsRight[0];
                PhotoPair[i + 1].ExternalElementsRight[1] += PhotoPair[i].ExternalElementsRight[1];
                PhotoPair[i + 1].ExternalElementsRight[2] += PhotoPair[i].ExternalElementsRight[2];


                // 缩放 对的
                for (int j = 0; j < PhotoPair[i + 1].n; j++)
                {
                    PhotoPair[i + 1].X[j] = PhotoPair[i + 1].X[j] * K + PhotoPair[i].ExternalElementsRight[0];
                    PhotoPair[i + 1].Y[j] = PhotoPair[i + 1].Y[j] * K + PhotoPair[i].ExternalElementsRight[1];
                    PhotoPair[i + 1].Z[j] = PhotoPair[i + 1].Z[j] * K + PhotoPair[i].ExternalElementsRight[2];
                }


            }

            #endregion

            #region 出来
            // 像对，点名，像辅坐标，点投影系数，模型点坐标
            Report += "\n# * * * * * * * * * * * * * * * * * 比例尺系数归化与平移 建立统一航带网模型 * * * * * * * * * * * * * * * * #\n\n";
           
            for (int i = 0; i < PhotoPair.Count; i++)
            {
                Report += string.Format("像对 : {0}         {1}\n", PhotoPair[i].LeftPhoto, PhotoPair[i].RightPhoto);
                Report += string.Format("{0, -4}\t", "点名");
                Report += string.Format("{0, -20}\t", "XA(m)");
                Report += string.Format("{0, -20}\t", "YA(m)");
                Report += string.Format("{0, -20}\n", "ZA(m)");

                for (int j = 0; j < PhotoPair[i].n; j++)
                {
                    Report += string.Format("{0, -6}\t", PhotoPointName[PhotoPair[i].Number[j]]);
                    Report += string.Format("{0, -20:F6}\t", PhotoPair[i].X[j]);
                    Report += string.Format("{0, -20:F6}\t", PhotoPair[i].Y[j]);
                    Report += string.Format("{0, -20:F6}\n", PhotoPair[i].Z[j]);
                }
                Report += "----------------------------------------------------------------------";
                Report += "------------\n\n";
            }
            #endregion
        }

        /// <summary>
        /// 绝对定向
        /// </summary>
        private void Report3()
        {
            #region 坐标小修几手
            // 找出控制点对应的模型点坐标
            for (int i = 0; i < PhotoPair.Count; i++)
            {
                for (int j = 0; j < PhotoPair[i].n; j++)
                {
                    // 找控制点的模型点坐标
                    for (int k = 0; k < ControlPoint.Count; k++)
                    {
                        if (PhotoPair[i].Number[j] == ControlPoint[k].PointNumber)
                        {
                            // 找到了
                            ControlPoint[k].X_ = PhotoPair[i].X[j];
                            ControlPoint[k].Y_ = PhotoPair[i].Y[j];
                            ControlPoint[k].H_ = PhotoPair[i].Z[j];
                            AllPoint[ControlPoint[k].PointNumber].Style = "控制点";
                        }
                    }

                }
            }

            // 先微调一下
            int NKnow = ControlPoint.Count;
            double[] XOld = new double[NKnow];
            double[] YOld = new double[NKnow];
            double[] ZOld = new double[NKnow];
            double[] XNew = new double[NKnow];
            double[] YNew = new double[NKnow];
            double[] ZNew = new double[NKnow];

            // 平移原点
            double X0 = ControlPoint[0].X;
            double Y0 = ControlPoint[0].Y;
            double H0 = ControlPoint[0].H;

            double x0 = ControlPoint[0].X_;
            double y0 = ControlPoint[0].Y_;
            double h0 = ControlPoint[0].H_;

            for (int i = 0; i < AllPoint.Length; i++)
            {
                AllPoint[i].X_ = AllPoint[i].X_ - x0;
                AllPoint[i].Y_ = AllPoint[i].Y_ - y0;
                AllPoint[i].H_ = AllPoint[i].H_ - h0;
            }

            for (int i = 0; i < NKnow; i++)
            {
                XOld[i] = ControlPoint[i].X_ - x0;
                YOld[i] = ControlPoint[i].Y_ - y0;
                ZOld[i] = ControlPoint[i].H_ - h0;
                XNew[i] = ControlPoint[i].X - X0;
                YNew[i] = ControlPoint[i].Y - Y0;
                ZNew[i] = ControlPoint[i].H - H0;
            }

            // 再小修一手
            double xt1 = XNew[1];
            double yt1 = YNew[1];
            double xp1 = XOld[1];
            double yp1 = YOld[1];

            double a = (yt1 * xp1 + xt1 * yp1) / (xt1 * xt1 + yt1 * yt1);
            double b = (xt1 * xp1 - yt1 * yp1) / (xt1 * xt1 + yt1 * yt1);
            double lamb = Math.Sqrt(a * a + b * b);
            Matrix ab1 = new Matrix(new double[2, 2] { { b, a }, { a, -b } });
            ab1 = ~ab1;

            for (int i = 0; i < NKnow; i++)
            {
                double xx = b * XNew[i] + a * YNew[i];
                double yy = a * XNew[i] - b * YNew[i];
                XNew[i] = xx;
                YNew[i] = yy;
                ZNew[i] = ZNew[i] * lamb;
            }

            #endregion

            #region 绝定
            // 平差参数
            Matrix B = new Matrix(NKnow * 3, 7);    // 误差方程系数
            Matrix l = new Matrix(NKnow * 3, 1);    // 误差方程常数项
            Matrix L0 = new Matrix(NKnow * 3, 1);   // 控原坐标
            Matrix L = new Matrix(NKnow * 3, 1);    // 定了的坐标
            Matrix x = new Matrix(7, 1);            // 那七个参数改正数
            double DelatX = 0;
            double DelatY = 0;
            double DelatZ = 0;
            double phi = 0;
            double omega = 0;
            double kappa = 0;
            double lambda = 1;
            Matrix R = CommonMethod.R(phi, omega, omega);

            // 设初值
            for (int i = 0; i < NKnow; i++)
            {
                // B和L的索引
                int inn = i * 3;

                L0.A[inn + 0, 0] = XNew[i];
                L0.A[inn + 1, 0] = YNew[i];
                L0.A[inn + 2, 0] = ZNew[i];

                L.A[inn + 0, 0] = XOld[i];
                L.A[inn + 1, 0] = YOld[i];
                L.A[inn + 2, 0] = ZOld[i];
            }

            int hahaha = 0;

            while (hahaha < 100)
            {
                hahaha++;

                #region B
                // 构造误差方程系数
                for (int i = 0; i < NKnow; i++)
                {
                    int inn = i * 3; // B和L的索引

                    double XOldi = L.A[inn + 0, 0];
                    double YOldi = L.A[inn + 1, 0];
                    double ZOldi = L.A[inn + 2, 0];

                    // 第一行
                    B.A[inn + 0, 0] = 1;
                    B.A[inn + 0, 3] = XOldi;
                    B.A[inn + 0, 4] = -ZOldi;
                    B.A[inn + 0, 6] = -YOldi;

                    // 第二行
                    B.A[inn + 1, 1] = 1;
                    B.A[inn + 1, 3] = YOldi;
                    B.A[inn + 1, 5] = -ZOldi;
                    B.A[inn + 1, 6] = XOldi;

                    // 第三行
                    B.A[inn + 2, 2] = 1;
                    B.A[inn + 2, 3] = ZOldi;
                    B.A[inn + 2, 4] = XOldi;
                    B.A[inn + 2, 5] = YOldi;

                }
                #endregion

                // 最小二乘
                l = L0 - L;
                x = ~(!B * B) * !B * l;

                // 大修坐标
                DelatX += x.A[0, 0];
                DelatY += x.A[1, 0];
                DelatZ += x.A[2, 0];
                lambda += x.A[3, 0];
                phi += x.A[4, 0];
                omega += x.A[5, 0];
                kappa += x.A[6, 0];

                for (int i = 0; i < NKnow; i++)
                {
                    double xx = XOld[i];
                    double yy = YOld[i];
                    double zz = ZOld[i];

                    R = CommonMethod.R(phi, omega, kappa);
                    L.A[i * 3 + 0, 0] = lambda * (R.A[0, 0] * xx + R.A[0, 1] * yy + R.A[0, 2] * zz) + DelatX;
                    L.A[i * 3 + 1, 0] = lambda * (R.A[1, 0] * xx + R.A[1, 1] * yy + R.A[1, 2] * zz) + DelatY;
                    L.A[i * 3 + 2, 0] = lambda * (R.A[2, 0] * xx + R.A[2, 1] * yy + R.A[2, 2] * zz) + DelatZ;
                }
            }

            #endregion

            #region 修出来

            for (int i = 0; i < AllPoint.Length; i++)
            {
                double xx = AllPoint[i].X_;
                double yy = AllPoint[i].Y_;
                double zz = AllPoint[i].H_;

                R = CommonMethod.R(phi, omega, kappa);
                double xxx = lambda * (R.A[0, 0] * xx + R.A[0, 1] * yy + R.A[0, 2] * zz) + DelatX;
                double yyy = lambda * (R.A[1, 0] * xx + R.A[1, 1] * yy + R.A[1, 2] * zz) + DelatY;
                double zzz = lambda * (R.A[2, 0] * xx + R.A[2, 1] * yy + R.A[2, 2] * zz) + DelatZ;

                Matrix xyxy = ab1 * (new Matrix(new double[2, 1] { { xxx }, { yyy } }));

                AllPoint[i].X = X0 + xyxy.A[0, 0];
                AllPoint[i].Y = Y0 + xyxy.A[1, 0];
                AllPoint[i].H = H0 + zzz / lamb;
            }

            // 找出检查点点对应的模型点坐标
            for (int i = 0; i < PhotoPair.Count; i++)
            {
                for (int j = 0; j < PhotoPair[i].n; j++)
                {
                    // 找检查点的算出来的坐标
                    for (int k = 0; k < CheckPoint.Count; k++)
                    {
                        if (PhotoPair[i].Number[j] == CheckPoint[k].PointNumber)
                        {
                            // 找到了
                            CheckPoint[k].X_ = AllPoint[CheckPoint[k].PointNumber].X;
                            CheckPoint[k].Y_ = AllPoint[CheckPoint[k].PointNumber].Y;
                            CheckPoint[k].H_ = AllPoint[CheckPoint[k].PointNumber].H;
                            AllPoint[CheckPoint[k].PointNumber].Style = "检查点";
                        }
                    }

                }
            }


            Report += "\n# * * * * * * * * * * * * * * * * * 绝对定向 计算出坐标点 * * * * * * * * * * * * * * * * #\n\n";
            Report += string.Format("{0, -6}\t", "点名");
            Report += string.Format("{0, -20:F8}\t", "X");
            Report += string.Format("{0, -20:F8}\t", "Y");
            Report += string.Format("{0, -20:F8}\t", "H");
            Report += string.Format("{0, -20}\n", "点类型");


            for (int i = 0; i < AllPoint.Length; i++)
            {
                Report += string.Format("{0, -8}\t", PhotoPointName[i]);
                Report += string.Format("{0, -20:F3}\t", AllPoint[i].X);
                Report += string.Format("{0, -20:F3}\t", AllPoint[i].Y);
                Report += string.Format("{0, -20:F3}\t", AllPoint[i].H);
                Report += string.Format("{0, -20}\n", AllPoint[i].Style);
            }
            #endregion

            #region 精度评定

            Report += "\n# * * * * * * * * * * * * * * * * * 精度评定 * * * * * * * * * * * * * * * * #\n\n";

            double vv = 0;
            double vx = 0;
            double vy = 0;
            double vh = 0;
            for (int i = 0; i < CheckPoint.Count; i++)
            {
                vx += Math.Abs(CheckPoint[i].X_ - CheckPoint[i].X);
                vy += Math.Abs(CheckPoint[i].Y_ - CheckPoint[i].Y);
                vh += Math.Abs(CheckPoint[i].H_ - CheckPoint[i].H);

                vv += Math.Sqrt((CheckPoint[i].X_ - CheckPoint[i].X) * (CheckPoint[i].X_ - CheckPoint[i].X)
                              + (CheckPoint[i].Y_ - CheckPoint[i].Y) * (CheckPoint[i].Y_ - CheckPoint[i].Y)
                              + (CheckPoint[i].H_ - CheckPoint[i].H) * (CheckPoint[i].H_ - CheckPoint[i].H));
            }

            Report += string.Format("X 方向平均偏差 : {0:F4} m\n", vx / CheckPoint.Count);
            Report += string.Format("Y 方向平均偏差 : {0:F4} m\n", vy / CheckPoint.Count);
            Report += string.Format("Z 方向平均偏差 : {0:F4} m\n", vh / CheckPoint.Count);
            Report += string.Format("全方向平均偏差 : {0:F4} m\n", vv / CheckPoint.Count);

            Report += "\n# * * * * * * * * * * * * * * * * * 程序结束 * * * * * * * * * * * * * * * * #\n\n";
            #endregion

        }


        #region 辅助函数
        /// <summary>
        /// 像点名转像点号
        /// </summary>
        /// <param name="pname">像点名</param>
        /// <returns>像点号</returns>
        public int GetPNumber(string pname)
        {
            int i = 0;
            for (i = 0; i < PhotoPointName.Count; i++)
            {
                if (pname == PhotoPointName[i])
                {
                    return i;
                }
            }
            PhotoPointName.Add(pname);
            return i;
        }




        #endregion

    }
}
