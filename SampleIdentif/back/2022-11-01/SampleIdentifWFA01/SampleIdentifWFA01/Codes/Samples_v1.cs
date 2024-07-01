///*------------------------------------------------------------------------------
// * File    : Samples
// * Time    : 2022/5/16 11:20:28
// * Author  : Zheng Han 
// * Contact : hzsongrentou1580@gmail.com
// * License : (C)Copyright 2022, ZhengHan. All rights reserved.
// * Desc    : class[Samples]
//------------------------------------------------------------------------------*/

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace SampleIdentifWFA01
//{
//    /// <summary>
//    /// 样本点
//    /// </summary>
//    class Samples
//    {
//        #region 静态
//        /// <summary>
//        /// 类别名和数量
//        /// </summary>
//        public static Dictionary<string, int> CateoryNamesCounts = new Dictionary<string, int>();
//        /// <summary>
//        /// 类别名
//        /// </summary>
//        public static string[] CateoryNames = null;
//        /// <summary>
//        /// 每个类别的数量
//        /// </summary>
//        public static int[] CateoryCounts = null;
//        /// <summary>
//        /// 混淆矩阵
//        /// </summary>
//        public static double[,] CM = null;

//        /// <summary>
//        /// 每一个样本
//        /// </summary>
//        public static List<Samples> Spls = new List<Samples>();
//        /// <summary>
//        /// 每一个字段的名字
//        /// </summary>
//        public static string[] FieldNames = null;
//        /// <summary>
//        /// 每一个字段名字的索引
//        /// </summary>
//        public static Dictionary<string, int> KvpFieldNames = new Dictionary<string, int>();

//        /// <summary>
//        /// 样本点的数量
//        /// </summary>
//        public static int Number
//        {
//            get { return Spls.Count; }
//        }

//        /// <summary>
//        /// 使用 csv file 构建样本集合
//        /// </summary>
//        /// <param name="csv_file">csv 文件</param>
//        /// <param name="cateory_field_name">类别的字段名</param>
//        /// <param name="imgfile_field_name">文件路径的字段名</param>
//        /// <returns>样本的数量</returns>
//        public static int BuildByCsv(string csv_file, string cateory_field_name, string imgfile_field_name)
//        {
//            CateoryNamesCounts.Add("Not Know", 0);
//            // 读取头信息
//            StreamReader sr = new StreamReader(csv_file);
//            string line = sr.ReadLine();
//            FieldNames = line.Split(',');
//            if(!FieldNames.Contains(cateory_field_name))
//            {
//                throw new Exception("Can not find cateory field name in csv file: " + csv_file);
//            }
//            if(!FieldNames.Contains(imgfile_field_name))
//            {
//                throw new Exception("Can not find image file field name in csv file: " + csv_file);

//            }
//            try
//            {
//                for (int i = 0; i < FieldNames.Length; i++)
//                {
//                    KvpFieldNames.Add(FieldNames[i], i);
//                }
//            }
//            catch
//            {
//                throw new Exception("repeat field name in csv file: " + csv_file);
//            }
//            // 读取全部信息
//            line = sr.ReadLine();
//            List<string> ks = new List<string>();
//            ks.Add("Not Know");
//            while (line != null)
//            {
//                Samples samples = new Samples();
//                samples.Infos = line.Split(',');
//                samples.Count = KvpFieldNames.Count;
//                Spls.Add(samples);
//                // 获得类别名
//                string leibie = samples[cateory_field_name];
//                if(!CateoryNamesCounts.Keys.Contains(leibie))
//                {
//                    CateoryNamesCounts.Add(leibie, 1);
//                    ks.Add(leibie);
//                }
//                else
//                {
//                    CateoryNamesCounts[leibie]++;
//                }
//                samples.OldCateory = ks.FindIndex(k => k == leibie);
//                samples.NewCateory = samples.OldCateory;
//                // 获得图像名
//                samples.ImFileName = samples[imgfile_field_name];
//                if (!File.Exists(samples.ImFileName))
//                {
//                    throw new Exception("can not fnid file: " + samples.ImFileName);
//                }
//                line = sr.ReadLine();
//            }
//            CateoryNames = CateoryNamesCounts.Keys.ToArray();
//            CateoryCounts = CateoryNamesCounts.Values.ToArray();
//            ks.Clear();
//            ks = null;
//            return Spls.Count;
//        }

//        /// <summary>
//        /// 保存为文件
//        /// </summary>
//        /// <param name="save_file_name"></param>
//        /// <param name="n_render"></param>
//        /// <param name="work_name"></param>
//        /// <returns></returns>
//        public static bool Save(string save_file_name, int n_render, string work_name)
//        {
//            if (save_file_name == "")
//            {
//                //save_file_name = Path.Combine(DirName, "SplIndf_" + WorkName + ".txt");
//                return false;
//            }
//            SrtFileFmt srtFileFmt = new SrtFileFmt(save_file_name);
//            srtFileFmt.Open(SrtFileOpenOpts.Write);
//            srtFileFmt.WriteLine("Current Sample", n_render.ToString());
//            srtFileFmt.WriteLine("Work Name", work_name);
//            //srtFileFmt.WriteLine("Current Sample", n_render.ToString());
//            // 写入类别信息
//            for (int i = 0; i < CateoryNamesCounts.Count; i++)
//            {
//                srtFileFmt.WriteLine("Cateory Info", string.Format("{0}\t{1}\t{2}", i, CateoryNames[i], CateoryCounts[i]));
//            }
//            // 写入样本数量
//            srtFileFmt.WriteLine("Sample Count", Spls.Count.ToString());
//            // 写入每个样本的信息
//            string ss = "";
//            for (int i = 0; i < FieldNames.Length - 1; i++)
//            {
//                ss += FieldNames[i] + ",";
//            }
//            ss += FieldNames[FieldNames.Length - 1];
//            srtFileFmt.WriteLine("Every Sample", ss);
//            for (int i = 0; i < Spls.Count; i++)
//            {
//                string line = Spls[i].OldCateory.ToString() + ",";
//                line += Spls[i].NewCateory.ToString() + ",";
//                line += Spls[i].ImFileName;
//                for (int j = 0; j < Spls[i].Count; j++)
//                {
//                    line += "," + Spls[i][j];
//                }
//                srtFileFmt.WriteLine("Every Sample", line);
//            }
//            srtFileFmt.Close();
//            return true;
//        }

//        /// <summary>
//        /// 打开文件
//        /// </summary>
//        /// <param name="prj_file"></param>
//        /// <returns></returns>
//        public static int Open(string prj_file)
//        {
//            int n_render = 0;
//            SrtFileFmt srtFileFmt = new SrtFileFmt(prj_file);
//            srtFileFmt.Open(SrtFileOpenOpts.Read);
//            n_render = int.Parse(srtFileFmt.Get("Current Sample"));
//            string WorkName = srtFileFmt.Get("Work Name");
//            srtFileFmt.SetActivityMark("Cateory Info");
//            string[] lines = srtFileFmt.GetLine().Split('\t');
//            while (lines != null)
//            {
//                CateoryNamesCounts[lines[1]] = int.Parse(lines[2]);
//                lines = srtFileFmt.GetLine()?.Split('\t');
//            }
//            srtFileFmt.Flash();
//            int n = int.Parse(srtFileFmt.Get("Sample Count"));
//            srtFileFmt.SetActivityMark("Every Sample");
//            FieldNames = srtFileFmt.GetLine().Split(',');
//            for (int i = 0; i < n; i++)
//            {
//                lines = srtFileFmt.GetLine().Split(',');
//                Samples samples = new Samples();
//                samples.OldCateory = int.Parse(lines[0]);
//                samples.NewCateory = int.Parse(lines[1]);
//                samples.ImFileName = lines[2];
//                samples.Count = FieldNames.Length;
//                samples.Infos = new string[FieldNames.Length];
//                for (int j = 3; j < lines.Length; j++)
//                {
//                    samples.Infos[j - 3] = lines[j];
//                }
//                Spls.Add(samples);
//            }
//            CateoryNames = CateoryNamesCounts.Keys.ToArray();
//            CateoryCounts = CateoryNamesCounts.Values.ToArray();
//            srtFileFmt.Close();
//            return n_render;
//        }
//        #endregion

//        #region 动态
//        /// <summary>
//        /// 图像文件名
//        /// </summary>
//        public string ImFileName = @"";
//        /// <summary>
//        /// 图像类别
//        /// </summary>
//        public int OldCateory = 0;
//        /// <summary>
//        /// 解译后图像的类别
//        /// </summary>
//        public int NewCateory = 0;

//        /// <summary>
//        /// 每个标签的信息
//        /// </summary>
//        public string[] Infos = null;

//        /// <summary>
//        /// 数量
//        /// </summary>
//        public int Count = 0;

//        /// <summary>
//        /// 获得第i个信息
//        /// </summary>
//        /// <param name="i">索引</param>
//        /// <returns></returns>
//        public string this[int i]
//        {
//            get { return Infos[i]; }
//            set { Infos[i] = value; }
//        }

//        /// <summary>
//        /// 获得该字段的信息
//        /// </summary>
//        /// <param name="field_name">字段名</param>
//        /// <returns></returns>
//        public string this[string field_name]
//        {
//            get { return Infos[KvpFieldNames[field_name]]; }
//            set { Infos[KvpFieldNames[field_name]] = value; }
//        }

//        /// <summary>
//        /// 修改类别
//        /// </summary>
//        /// <param name="n">类别的编号</param>
//        public void ChangeClasses(int n)
//        {
//            if (n != NewCateory)
//            {
//                CateoryNamesCounts[CateoryNames[NewCateory]]--;
//                CateoryNamesCounts[CateoryNames[n]]++;
//                CateoryCounts[NewCateory]--;
//                CateoryCounts[n]++;
//                NewCateory = n;
//            }
//        }
//        #endregion
//    }

//}
