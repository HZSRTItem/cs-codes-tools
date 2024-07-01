using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhotogrammetryWFA
{
    class FileRead2Write
    {

        private OpenFileDialog Ofd = new OpenFileDialog();


        public FileRead2Write()
        {

        }


        #region 单航带网文件读写

        /// <summary>
        /// 打开航带网文件
        /// </summary>
        /// <returns></returns>
        public SingleFlightNetwork ReadFlight(ref string dataInformation, ref DataTable dt)
        {
            // 打开文件对话框
            Ofd.InitialDirectory = Directory.GetCurrentDirectory();
            Ofd.Title = "打开航带网文件";
            Ofd.Multiselect = false;
            Ofd.Filter = "Txt File (*.txt)|*.txt";

            if (!(Ofd.ShowDialog() == DialogResult.OK))
            {
                return null;
            }

            // 读取航带网数据
            SingleFlightNetwork sfn = new SingleFlightNetwork();

            // 打开文件为文件流
            StreamReader streamReader = new StreamReader(Ofd.FileName, Encoding.Default);
            string line = streamReader.ReadLine();

            // 数据信息
            dataInformation = "\n#* * * * * * * * * * * * * * 原始数据信息 * * * * * * * * * * * * * * #\n";

            // 读头文件
            sfn.f = double.Parse(line.Split(' ')[0]) / 1000;
            sfn.bx = double.Parse(line.Split(' ')[1]) / 1000;

            dataInformation += string.Format("像片主距 f : {0:F3}mm\n", sfn.f * 1000);
            dataInformation += string.Format("x方向基线分量 bx : {0:F3}mm\n", sfn.bx * 1000);

            // 读取像对数据
            dataInformation += ReadPointPair(ref sfn, streamReader);

            // 读取控制点数据
            dataInformation += ReadFlightConPoint(ref sfn, streamReader);

            // 读取检查点数据
            dataInformation += ReadFlightCheckPoint(ref sfn, streamReader);

            streamReader.Close();

            ToDataTable(dt, sfn);

            return sfn;
        }

        /// <summary>
        /// 转DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sfn"></param>
        private void ToDataTable(DataTable dt, SingleFlightNetwork sfn)
        {

            dt.Columns.Add("F1");
            dt.Columns.Add("F2");
            dt.Columns.Add("F3");
            dt.Columns.Add("F4");
            dt.Columns.Add("F5");
            DataRow row;

            // 添加像对数据
            for (int i = 0; i < sfn.PhotoPair.Count; i++)
            {
                // 头
                row = dt.Rows.Add();
                row[0] = "左像片 : " + sfn.PhotoPair[i].LeftPhoto;
                row[1] = "左像片 : " + sfn.PhotoPair[i].RightPhoto;

                row = dt.Rows.Add();
                row[0] = "像点名";
                row[1] = "x1(m)";
                row[2] = "y1(m)";
                row[3] = "x2(m)";
                row[4] = "y2(m)";

                // 像点
                for (int j = 0; j < sfn.PhotoPair[i].n; j++)
                {
                    row = dt.Rows.Add();
                    row[0] = sfn.PhotoPointName[sfn.PhotoPair[i].Number[j]];
                    row[1] = sfn.PhotoPair[i].x1[j];
                    row[2] = sfn.PhotoPair[i].y1[j];
                    row[3] = sfn.PhotoPair[i].x2[j];
                    row[4] = sfn.PhotoPair[i].y2[j];
                }
            }

            // 添加控制点数据
            row = dt.Rows.Add();
            row[0] = "像控制点数据";

            row = dt.Rows.Add();
            row[0] = "像点名";
            row[1] = "X(m)";
            row[2] = "Y(m)";
            row[3] = "H(m)";

            for (int i = 0; i < sfn.ControlPoint.Count; i++)
            {
                row = dt.Rows.Add();
                row[0] = sfn.PhotoPointName[sfn.ControlPoint[i].PointNumber];
                row[1] = sfn.ControlPoint[i].X;
                row[2] = sfn.ControlPoint[i].Y;
                row[3] = sfn.ControlPoint[i].H;
            }

            // 添加检查点数据
            row = dt.Rows.Add();
            row[0] = "检查点数据";

            row = dt.Rows.Add();
            row[0] = "像点名";
            row[1] = "X(m)";
            row[2] = "Y(m)";
            row[3] = "H(m)";

            for (int i = 0; i < sfn.CheckPoint.Count; i++)
            {
                row = dt.Rows.Add();
                row[0] = sfn.PhotoPointName[sfn.CheckPoint[i].PointNumber];
                row[1] = sfn.CheckPoint[i].X;
                row[2] = sfn.CheckPoint[i].Y;
                row[3] = sfn.CheckPoint[i].H;
            }
        }


        /// <summary>
        /// 读取单航带像对数据
        /// </summary>
        /// <param name="sfn"></param>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private string ReadPointPair(ref SingleFlightNetwork sfn, StreamReader streamReader)
        {
            // 读取像对数据
            string dataInformation = "";
            string line = streamReader.ReadLine();
            PointPair p = new PointPair();
            List<PointPair> pList = new List<PointPair>();
            while (line != null)
            {
                // 空行或注释则继续
                if (line == "")
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到注释继续
                if (line[0] == '#')
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到end跳出
                if (line.Split(' ')[1] == "end")
                {
                    p.n = p.Number.Count;
                    pList.Add(p);
                    line = streamReader.ReadLine();
                    break;
                }

                // 读取到像对数据头
                if (line[0] == '>')
                {
                    p.n = p.Number.Count;
                    pList.Add(p);
                    p = new PointPair();
                    p.f = sfn.f;
                    p.bx = sfn.bx;
                    p.ExternalElementsRight[0] = sfn.bx;
                    p.LeftPhoto = line.Split(' ')[1];
                    p.RightPhoto = line.Split(' ')[2];
                }
                else
                {
                    List<string> lineList = line.Split(' ').ToList();
                    lineList.RemoveAll(data => data == "");

                    p.Number.Add(sfn.GetPNumber(lineList[0]));
                    p.x1.Add(double.Parse(lineList[1]) / 1000000);
                    p.y1.Add(double.Parse(lineList[2]) / 1000000);
                    p.x2.Add(double.Parse(lineList[3]) / 1000000);
                    p.y2.Add(double.Parse(lineList[4]) / 1000000);

                }

                // 读一行
                line = streamReader.ReadLine();
            }
            pList.RemoveAt(0);
            dataInformation += string.Format("像片数量 : {0}\n", pList.Count);

            // 排序
            sfn.PhotoPair.Add(pList[0]);
            int n = pList.Count;
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < pList.Count; j++)
                {
                    if (pList[j].LeftPhoto == sfn.PhotoPair[i - 1].RightPhoto)
                    {
                        sfn.PhotoPair.Add(pList[j]);
                        pList.RemoveAt(j);
                        break;
                    }
                    else if (pList[j].RightPhoto == sfn.PhotoPair[i - 1].RightPhoto)
                    {
                        string name = pList[j].LeftPhoto;
                        List<double> xl = pList[j].x1.ToList();
                        List<double> yl = pList[j].y1.ToList();

                        pList[j].LeftPhoto = pList[j].RightPhoto;
                        pList[j].x1 = pList[j].x2;
                        pList[j].y1 = pList[j].y2;

                        pList[j].RightPhoto = name;
                        pList[j].x2 = xl;
                        pList[j].y2 = yl;

                        sfn.PhotoPair.Add(pList[j]);
                        pList.RemoveAt(j);
                        break;
                    }
                }

            }

            return dataInformation;
        }


        /// <summary>
        /// 读取控制点数据
        /// </summary>
        /// <param name="sfn"></param>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private static string ReadFlightConPoint(ref SingleFlightNetwork sfn, StreamReader streamReader)
        {
            string line = "";
            string dataInformation = "";

            while (line != null)
            {
                // 空行或注释则继续
                if (line == "")
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到注释继续
                if (line[0] == '#')
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到end跳出
                if (line.Split(' ')[1] == "end")
                {
                    line = streamReader.ReadLine();
                    break;
                }


                // 分割字符串
                List<string> lineList = line.Split(' ').ToList();
                lineList.RemoveAll(data => data == "");

                // 保存点数据
                GroundPoint gp = new GroundPoint();
                gp.PointNumber = sfn.GetPNumber(lineList[0]);
                gp.X = double.Parse(lineList[1]);
                gp.Y = double.Parse(lineList[2]);
                gp.H = double.Parse(lineList[3]);
                sfn.ControlPoint.Add(gp);

                // 读一行
                line = streamReader.ReadLine();
            }
            dataInformation += string.Format("控制点数量 : {0}\n", sfn.ControlPoint.Count);
            return dataInformation;
        }


        /// <summary>
        /// 读取检查点数据
        /// </summary>
        /// <param name="sfn"></param>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private static string ReadFlightCheckPoint(ref SingleFlightNetwork sfn, StreamReader streamReader)
        {
            string line = "";
            string dataInformation = "";
            // 读取检查点数据
            while (line != null)
            {
                // 空行或注释则继续
                if (line == "")
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到注释继续
                if (line[0] == '#')
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到end跳出
                if (line.Split(' ')[1] == "end")
                {
                    line = streamReader.ReadLine();
                    break;
                }

                // 分割字符串
                List<string> lineList = line.Split(' ').ToList();
                lineList.RemoveAll(data => data == "");

                // 保存点数据
                GroundPoint gp = new GroundPoint();
                gp.PointNumber = sfn.GetPNumber(lineList[0]);
                gp.X = double.Parse(lineList[1]);
                gp.Y = double.Parse(lineList[2]);
                gp.H = double.Parse(lineList[3]);
                sfn.CheckPoint.Add(gp);

                // 读一行
                line = streamReader.ReadLine();
            }
            dataInformation += string.Format("检查点数量 : {0}\n", sfn.CheckPoint.Count);
            return dataInformation;
        }


        public static void SaveReport(string Report)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Filter = "Txt File (*.txt)|*.txt";

            if(!(sfd.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("尚未保存文件");
                return;
            }

            StreamWriter sw = new StreamWriter(sfd.FileName);
            sw.Write(Report);
            sw.Close();
            MessageBox.Show("已保存文件");
        }

        #endregion


        #region DEM数据读写
        /// <summary>
        /// 读取DEM原始数据
        /// </summary>
        /// <param name="dt">表格数据</param>
        /// <returns>数据列表</returns>
        public List<double[]> ReadDEMData(DataTable dt)
        {
            // 打开文件对话框
            Ofd.InitialDirectory = Directory.GetCurrentDirectory();
            Ofd.Title = "打开DEM数据";
            Ofd.Multiselect = false;
            Ofd.Filter = "Txt File (*.txt)|*.txt";

            if (!(Ofd.ShowDialog() == DialogResult.OK))
            {
                return null;
            }

            // 打开文件为文件流
            StreamReader streamReader = new StreamReader(Ofd.FileName, Encoding.Default);
            string line = streamReader.ReadLine();
            List<double[]> dataList = new List<double[]>();

            while (line!=null)
            {

                // 读取到空行
                if (line == "")
                {
                    line = streamReader.ReadLine();
                    continue;
                }

                // 读取到注释行
                if(line[0] == '#')
                {
                    line = streamReader.ReadLine();
                    continue;
                }


                // 读取到数据行
                double[] datarow = new double[3];
                DataRow row = dt.Rows.Add();
                row[0] = line.Split(',')[0];
                for(int i=1;i<4;i++)
                {
                    datarow[i-1] =  double.Parse(line.Split(',')[i]);
                    row[i] = line.Split(',')[i];
                }
                dataList.Add(datarow);
                line = streamReader.ReadLine();
            }

            return dataList;
        }




        #endregion


        public static void SaveImage(Chart chart)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Filter = "JEPG File (*.jpeg)|*.jpeg";

            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("尚未保存文件");
                return;
            }

            chart.SaveImage(sfd.FileName, ChartImageFormat.Jpeg);
            MessageBox.Show("保存成功");
            return;
        }

    }
}
