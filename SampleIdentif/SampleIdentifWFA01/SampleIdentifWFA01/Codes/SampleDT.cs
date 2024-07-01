/*------------------------------------------------------------------------------
 * File    : SampleDT
 * Time    : 2022/8/6 8:00:30
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[SampleDT]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SampleIdentifWFA01
{
    public class SampleDT
    {
        #region 属性
        /// <summary>
        /// 坐标
        /// </summary>
        public List<double> X, Y;
        /// <summary>
        /// 解译的遥感影像
        /// </summary>
        public string ORemoteImageFile;
        /// <summary>
        /// 信息文件名
        /// </summary>
        public string PrjXmlFileName;
        /// <summary>
        /// GMap的缓存文件夹
        /// </summary>
        public string GMapCacheDir;
        /// <summary>
        /// 当前渲染样本
        /// </summary>
        public int NPic = 0;
        /// <summary>
        /// 类别信息
        /// </summary>
        public CategoryInfo CateInfo = new CategoryInfo();
        /// <summary>
        /// 特殊字段名
        /// </summary>
        public string cateName, srtName, xName, yName;
        /// <summary>
        /// 数据
        /// </summary>
        public DataTable SplDataT = null;
        /// <summary>
        /// 列数
        /// </summary>
        public int CountColumn
        {
            get { return SplDataT.Columns.Count; }
        }
        /// <summary>
        /// 行数
        /// </summary>
        public int CountRows
        {
            get { return SplDataT.Rows.Count; }
        }
        #endregion


        #region 构造函数
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cate_name"></param>
        /// <param name="srt_name"></param>
        /// <param name="x_name"></param>
        /// <param name="y_name"></param>
        public SampleDT(DataTable dt, string cate_name, string srt_name, string x_name, string y_name)
        {
            Gouzao(dt, cate_name, srt_name, x_name, y_name);
        }

        public SampleDT() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cate_name"></param>
        /// <param name="srt_name"></param>
        /// <param name="x_name"></param>
        /// <param name="y_name"></param>
        private void Gouzao(DataTable dt, string cate_name, string srt_name, string x_name, string y_name)
        {
            SplDataT = dt;
            cateName = cate_name;
            SplDataT.Columns[cate_name].SetOrdinal(1);
            srtName = srt_name;
            SplDataT.Columns[srt_name].SetOrdinal(0);
            xName = x_name;
            SplDataT.Columns[x_name].SetOrdinal(2);
            yName = y_name;
            SplDataT.Columns[y_name].SetOrdinal(3);
            X = new List<double>(dt.Rows.Count);
            Y = new List<double>(dt.Rows.Count);

            CateInfo.Add("NOT_KNOWN", 0, Color.FromArgb(0, 0, 0), 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dataRow = dt.Rows[i];
                X.Add(double.Parse(dataRow[x_name].ToString()));
                Y.Add(double.Parse(dataRow[y_name].ToString()));
                CateInfo.Add(dataRow[cate_name].ToString(), 1);
            }
            SplDataT.TableName = "SAMPLE_DT";

        }
        private bool AddSamples(string csv_file, string cate_name, string srt_name, string x_name, string y_name)
        {
            if (SplDataT == null)
            {
                StreamReader sr = new StreamReader(csv_file);
                string line = sr.ReadLine();
                string[] lines = line.Split(',');


                sr.Close();
            }
            return true;
        }
        #endregion

        #region 静态函数
        private static XmlSerializer serializer = new XmlSerializer(typeof(SampleDT));

        /// <summary>
        /// 序列化SampleDT对象，保存到文件中
        /// </summary>
        /// <param name="out_xml_file">输出XML文件</param>
        /// <param name="sampleDT">SampleDT对象</param>
        /// <returns>是否保存成功</returns>
        public static bool XmlSerializerSampleDT(string out_xml_file, SampleDT sampleDT)
        {
            StreamWriter sw = new StreamWriter(out_xml_file);
            serializer.Serialize(sw, sampleDT);
            sw.Close();
            return true;
        }

        /// <summary>
        /// 反序列化，使用XML文件构建SampleDT类
        /// </summary>
        /// <param name="xml_file_name">XML文件</param>
        /// <returns>SampleDT对象</returns>
        public static SampleDT SampleDTSerializerXml(string xml_file_name)
        {
            SampleDT sampleDT = null;
            using (StreamReader sr = new StreamReader(xml_file_name))
            {
                sampleDT = (SampleDT)serializer.Deserialize(sr);
            }
            sampleDT.PrjXmlFileName = Path.GetFullPath(xml_file_name);
            return sampleDT;
        }

        public static void ImportToCSV(DataTable dt, string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            string head = "";
            //拼接列头
            for (int cNum = 0; cNum < dt.Columns.Count; cNum++)
            {
                head += dt.Columns[cNum].ColumnName + ",";
            }
            //csv文件写入列头
            sw.WriteLine(head);
            string data = "";
            //csv写入数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string data2 = string.Empty;
                //拼接行数据
                for (int cNum1 = 0; cNum1 < dt.Columns.Count; cNum1++)
                {
                    data2 = data2 + "\"" + dt.Rows[i][dt.Columns[cNum1].ColumnName].ToString() + "\",";
                }
                bool flag = data != data2;
                if (flag)
                {
                    sw.WriteLine(data2);
                }
                data = data2;
            }
            sw.Close();
        }

        #endregion

        #region this
        /// <summary>
        /// 属性名和行数获得属性内容
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="n">行数</param>
        /// <returns>属性内容</returns>
        public string this[string name, int n]
        {
            set { SplDataT.Rows[n][name] = value; }
            get { return SplDataT.Rows[n][name].ToString(); }
        }

        #endregion

        #region Get 外部索引
        private string GetNullToKong(string ss)
        {
            if (ss == null)
            {
                ss = "";
            }
            else
            {
                ss = ss.Replace('\n', ' ');
            }
            return ss;
        }

        public string GetSingleImageFile(int n)
        {
            if (SplDataT.Columns.IndexOf("_SINGLE_IMAGE") != -1)
            {
                string ff = SplDataT.Rows[n]["_SINGLE_IMAGE"].ToString();
                if (Path.GetExtension(ff) == ".png")
                {
                    if (File.Exists(ff))
                    {
                        return ff;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public string GetColumnName(int n)
        {
            return SplDataT.Columns[n].ColumnName;
        }

        public string GetCategoryIndex(int n)
        {
            return CateInfo.GetIndexByName(SplDataT.Rows[n][cateName].ToString()).ToString();
        }

        public Color GetCategoryColor(int n)
        {
            return CateInfo.GetColorByName(SplDataT.Rows[n][cateName].ToString());
        }

        public bool ChangeClass(int i_cate)
        {
            int yuan_i_cate = CateInfo.GetIndexByName(SplDataT.Rows[NPic][cateName].ToString());
            CateInfo[yuan_i_cate].number--;
            CateInfo[i_cate].number++;
            SplDataT.Rows[NPic][cateName] = CateInfo[i_cate].name;
            return true;
        }
        #endregion

        #region Add 外部

        /// <summary>
        /// 添加单张影像集
        /// </summary>
        /// <param name="dir_name"></param>
        /// <returns></returns>
        public bool AddSingleImages(string dir_name)
        {
            DirectoryInfo root = new DirectoryInfo(dir_name);
            FileInfo[] files = root.GetFiles();
            List<string> arrRate = SplDataT.AsEnumerable().Select(d => d.Field<string>(srtName)).ToList();
            if (SplDataT.Columns.IndexOf("_SINGLE_IMAGE") == -1)
            {
                SplDataT.Columns.Add("_SINGLE_IMAGE");
            }
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".png")
                {
                    string srt = Path.GetFileNameWithoutExtension(files[i].Name);
                    int n = arrRate.IndexOf(srt);
                    if (n != -1)
                    {
                        SplDataT.Rows[n]["_SINGLE_IMAGE"] = files[i].FullName;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Set 外部索引

        public bool SetCateInfoName(int n, string name)
        {
            try
            {
                CateInfo.infos[n].name = name;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetCateInfoColor(int n, Color color)
        {
            try
            {
                CateInfo.infos[n].color = color;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 辅助函数
        public void SaveCSV(string csv_file)
        {
            ImportToCSV(SplDataT, csv_file);
        }
        #endregion

    }

    public class AInfo
    {
        public string name;
        public int number;

        [System.Xml.Serialization.XmlIgnore]
        public Color color
        {
            set { ColorAsArgb = value.ToArgb(); }
            get { return Color.FromArgb(ColorAsArgb); }
        }

        public int ColorAsArgb = 0;
    }

    public class CategoryInfo
    {

        public List<AInfo> infos = new List<AInfo>(256);

        private static Color[] colors =
        {
            Color.FromArgb(255,0,0),
            Color.FromArgb(146,208,80),
            Color.FromArgb(255,192,0),
            Color.FromArgb(0,112,192),
            Color.FromArgb(255,255,0),
            Color.FromArgb(0,176,240),
            Color.FromArgb(0,32,96),
            Color.FromArgb(112,48,160),
        };

        public int Count
        {
            get { return infos.Count; }
        }

        public bool Add(string name, int number, Color color, int n)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                if (name == infos[i].name)
                {
                    infos[i].number += n;
                    return false;
                }
            }

            AInfo cateInfo = new AInfo()
            {
                name = name,
                number = number,
                color = color
            };

            infos.Add(cateInfo);
            return true;
        }

        public bool Add(string name, int n)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                if (name == infos[i].name)
                {
                    infos[i].number += n;
                    return false;
                }
            }

            AInfo cateInfo = new AInfo()
            {
                name = name,
                number = 1,

            };
            cateInfo.color = GetRandomColor();
            infos.Add(cateInfo);

            return true;
        }

        public AInfo this[int i]
        {
            get { return infos[i]; }
        }

        public CategoryInfo()
        {

        }

        public Color GetRandomColor()
        {
            if (colors.Length >= Count)
            {
                return colors[Count - 1];
            }

            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        public void AddNumber(string name, int n)
        {

        }

        public int GetIndexByName(string name)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                if (infos[i].name == name)
                {
                    return i;
                }
            }
            throw new Exception("Not find category by name " + name);
        }

        public Color GetColorByName(string name)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                if (infos[i].name == name)
                {
                    return infos[i].color;
                }
            }
            throw new Exception("Not find category by name " + name);
        }

    }
}
