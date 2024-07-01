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

namespace SampleIdentifWFA01
{
    public class SampleDT
    {
        /// <summary>
        /// 坐标
        /// </summary>
        public List<double> X, Y;
        /// <summary>
        /// 项目文件夹名
        /// </summary>
        public string PrjDirName;
        /// <summary>
        /// 项目名
        /// </summary>
        public string PrjName;
        /// <summary>
        /// 解译的遥感影像
        /// </summary>
        public string ORemoteImageFile;
        /// <summary>
        /// 信息文件名
        /// </summary>
        public string PrjTxtName;
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
        /// 数据文件夹
        /// </summary>
        public string DataDir;
        /// <summary>
        /// 特殊字段名
        /// </summary>
        private string cateName, srtName, xName, yName;
        /// <summary>
        /// 数据
        /// </summary>
        private DataTable dataTable;

        /// <summary>
        /// 列数
        /// </summary>
        public int CountColumn
        {
            get { return dataTable.Columns.Count; }
        }
        /// <summary>
        /// 行数
        /// </summary>
        public int CountRows
        {
            get { return dataTable.Rows.Count; }
        }

        public SampleDT(DataTable dt, string cate_name, string srt_name, string x_name, string y_name)
        {
            NewMethod(dt, cate_name, srt_name, x_name, y_name);
        }

        private void NewMethod(DataTable dt, string cate_name, string srt_name, string x_name, string y_name)
        {
            dataTable = dt;
            cateName = cate_name;
            dataTable.Columns[cate_name].SetOrdinal(1);
            srtName = srt_name;
            dataTable.Columns[srt_name].SetOrdinal(0);
            xName = x_name;
            dataTable.Columns[x_name].SetOrdinal(2);
            yName = y_name;
            dataTable.Columns[y_name].SetOrdinal(3);
            X = new List<double>(dt.Rows.Count);
            Y = new List<double>(dt.Rows.Count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dataRow = dt.Rows[i];
                X.Add(double.Parse(dataRow[x_name].ToString()));
                Y.Add(double.Parse(dataRow[y_name].ToString()));
                CateInfo.Add(dataRow[cate_name].ToString(), 1);
            }
            dataTable.TableName = "SAMPLE_DT";
        }

        public bool is_f_build = false;
        public SampleDT(string in_file)
        {
            string[] lines = File.ReadAllLines(in_file);
            lines = lines.Where(x => x != "").ToArray();
            if (lines[0] != "SAMPLE_IDF")
            {
                is_f_build = false;
                return;
            }
            char[] t = new char[] { ':' };

            for (int i = 1; i < lines.Length; i++)
            {
                string[] info2 = lines[i].Split(t, 2);
                info2[0] = info2[0].Trim();
                info2[1] = info2[1].Trim();
                switch (info2[0])
                {
                    case "Directory":
                        PrjDirName = info2[1];
                        break;

                    case "Name":
                        PrjName = info2[1];
                        break;

                    case "Remote Image File":
                        ORemoteImageFile = info2[1];
                        break;

                    case "TXT File":
                        PrjTxtName = info2[1];
                        break;

                    case "GMap Cache Directory":
                        GMapCacheDir = info2[1];
                        break;

                    case "Category Name":
                        cateName = info2[1];
                        break;

                    case "Unique Identifier Name":
                        srtName = info2[1];
                        break;

                    case "X Name":
                        xName = info2[1];
                        break;

                    case "Y Name":
                        yName = info2[1];
                        break;

                    case "Data Directory":
                        DataDir = info2[1];
                        break;

                    case "Category Info":
                        int n = int.Parse(info2[1]);
                        i += n;
                        break;

                    case "Current Sample":
                        NPic = int.Parse(info2[1]);
                        break;

                    default:
                        break;
                }

            }

            string ss = Path.Combine(DataDir, "table.xml");
            string xmlstr = File.ReadAllText(ss);
            DataTable dt = XmlToDataTable(xmlstr);
            NewMethod(dt, cateName, srtName, xName, yName);
            is_f_build = true;
        }

        /// <summary>
        /// 将xml转为Datable
        /// </summary>
        public static DataTable XmlToDataTable(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息  
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据  
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                 
                    ds.ReadXml(Xmlrdr);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    //释放资源  
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 属性名和行数获得属性内容
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="n">行数</param>
        /// <returns>属性内容</returns>
        public string this[string name, int n]
        {
            set { dataTable.Rows[n][name] = value; }
            get { return dataTable.Rows[n][name].ToString(); }
        }

        public bool Save()
        {
            Save(PrjTxtName);
            return true;
        }

        public bool Save(string out_file)
        {
            StreamWriter sw = new StreamWriter(out_file);
            sw.WriteLine("SAMPLE_IDF");
            sw.WriteLine("Directory: " + GetNullToKong(PrjDirName));
            sw.WriteLine("Name: " + PrjName);
            sw.WriteLine("Remote Image File: " + ORemoteImageFile);
            sw.WriteLine("TXT File: " + PrjTxtName);
            sw.WriteLine("GMap Cache Directory: " + GMapCacheDir);
            sw.WriteLine("Category Name: " + cateName);
            sw.WriteLine("Unique Identifier Name: " + srtName);
            sw.WriteLine("X Name: " + xName);
            sw.WriteLine("Y Name: " + yName);
            sw.WriteLine("Data Directory: " + DataDir);
            sw.WriteLine("Current Sample: " + NPic);
            sw.WriteLine("Category Info: " + CateInfo.Count);
            sw.Write(CateInfo.Write());
            dataTable.WriteXml(Path.Combine(DataDir, "table.xml"));
            sw.Close();
            return true;
        }

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
            if (dataTable.Columns.IndexOf("_SINGLE_IMAGE") != -1)
            {
                return dataTable.Rows[n]["_SINGLE_IMAGE"].ToString();
            }
            else
            {
                return null;
            }
        }

        public string GetColumnName(int n)
        {
            return dataTable.Columns[n].ColumnName;
        }

        public string GetCategoryIndex(int n)
        {
            return CateInfo.GetIndexByName(dataTable.Rows[n][cateName].ToString()).ToString();
        }

        public Color GetCategoryColor(int n)
        {
            return CateInfo.GetColorByName(dataTable.Rows[n][cateName].ToString());
        }

        public bool ChangeClass(int i_cate)
        {
            int yuan_i_cate = CateInfo.GetIndexByName(dataTable.Rows[NPic][cateName].ToString());
            CateInfo[yuan_i_cate].number--;
            CateInfo[i_cate].number++;
            dataTable.Rows[NPic][cateName] = CateInfo[i_cate].name;
            return true;
        }
    }

    public class CategoryInfo
    {
        public class AInfo
        {
            public string name;
            public int number;
            public Color color;
        }

        List<AInfo> infos = new List<AInfo>(256);

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
                color = GetRandomColor()
            };

            infos.Add(cateInfo);
            return true;
        }

        public AInfo this[int i]
        {
            get { return infos[i]; }
        }

        public CategoryInfo()
        {
            Add("NOT_KNOWN", 0, Color.FromArgb(0, 0, 0), 0);
        }

        public static Color GetRandomColor()
        {

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

        public string Write()
        {
            string ss = "";
            for (int i = 0; i < infos.Count; i++)
            {
                ss += "  ";
                ss += infos[i].name + "\t";
                ss += infos[i].number + "\t";
                ss += infos[i].color + "\n";
            }
            return ss;
        }
    }
}
