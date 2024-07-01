/*------------------------------------------------------------------------------
 * File    : IndfImg
 * Time    : 2022/3/13 14:54:26
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[IndfImg]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SampleIdentificationWFA01
{
    /// <summary>
    /// 解译图像类
    /// </summary>
    class IndfImg
    {
        /// <summary>
        /// 总类别的数量
        /// </summary>
        public static List<string> NameClasses = new List<string>();
        /// <summary>
        /// 每个类别的数量
        /// </summary>
        public static List<int> NumClasses = new List<int>();
        /// <summary>
        /// 所有的图片
        /// </summary>
        public static List<IndfImg> Imgs = new List<IndfImg>();
        /// <summary>
        /// 文件夹
        /// </summary>
        public static string DirName = "";
        /// <summary>
        /// 工程名
        /// </summary>
        public static string WorkName = "";

        /// <summary>
        /// 图像文件名
        /// </summary>
        public string ImFileName = @"";
        /// <summary>
        /// 图像类别
        /// </summary>
        public int Classes = 0;
        /// <summary>
        /// 解译后图像的类别
        /// </summary>
        public int NewClasses = 0;
        /// <summary>
        /// 每个标签的信息
        /// </summary>
        public List<string> SplInfo = new List<string>();

        /// <summary>
        /// 每张图片
        /// </summary>
        /// <param name="im_file"></param>
        public IndfImg(string im_file)
        {
            string f0 = Path.GetFileNameWithoutExtension(im_file);
            string[] f1 = f0.Split('_');
            Classes = AddNClasses(f1[f1.Length - 1]);
            NewClasses = Classes;
            ImFileName = Path.GetFullPath(im_file);
        }

        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="n"></param>
        public string ChangeClasses(int n)
        {
            if(n == NewClasses)
            {
                return "category not modified\n";
            }
            else
            {
                NumClasses[NewClasses]--;
                NumClasses[n]++;
                string outs = string.Format("category {0} -> {1} \n", NameClasses[NewClasses], NameClasses[n]);
                NewClasses = n;
                return outs;
            }
        }

        /// <summary>
        /// 构建工程
        /// </summary>
        /// <param name="dir_path"></param>
        public static void BuildIndfImg(string dir_path, string prj_file, string work_name)
        {

            if (Path.GetExtension(dir_path) == ".txt")
            {
                StreamReader sr = new StreamReader(dir_path);
                WorkName = sr.ReadLine();
                DirName = sr.ReadLine();

                string[] lines = sr.ReadLine().Split(',');

                while (lines.Length != 1)
                {
                    NameClasses.Add(lines[1]);
                    NumClasses.Add(int.Parse(lines[2]));
                    lines = sr.ReadLine().Split(',');
                }

                int n = int.Parse(lines[0]);
                for (int i = 0; i < n; i++)
                {
                    lines = sr.ReadLine().Split(',');
                    IndfImg indfImg = new IndfImg();
                    indfImg.Classes = int.Parse(lines[0]);
                    indfImg.NewClasses = int.Parse(lines[1]);
                    indfImg.ImFileName = lines[2];
                    for (int j = 3; j < lines.Length; j++)
                    {
                        indfImg.SplInfo.Add(lines[j]);
                    }
                    Imgs.Add(indfImg);
                }

                sr.Close();
            }
            else
            {
                if (work_name == "")
                {
                    WorkName = Path.GetFileNameWithoutExtension(dir_path);
                }
                else
                {
                    WorkName = work_name;
                }
                DirName = dir_path;
                NameClasses.Add("Not Know");
                NumClasses.Add(0);
                DirectoryInfo root = new DirectoryInfo(dir_path);
                FileInfo[] files = root.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    string fext = Path.GetExtension(files[i].Name);
                    if(fext == ".png" | fext == ".jpg" | fext == ".tif" | fext == ".tiff")
                    {
                        Imgs.Add(new IndfImg(files[i].FullName));
                    }
                }
                SaveAll(prj_file);
            }
            
        }

        public IndfImg()
        {

        }

        /// <summary>
        /// 添加一张图片
        /// </summary>
        /// <param name="classes_name"></param>
        /// <returns></returns>
        private static int AddNClasses(string classes_name)
        {
            for (int i = 0; i < NameClasses.Count; i++)
            {
                if (NameClasses[i] == classes_name)
                {
                    NumClasses[i]++;
                    return i;
                }
            }
            NameClasses.Add(classes_name);
            NumClasses.Add(1);
            return NameClasses.Count - 1;
        }

        public static void SaveAll(string save_file_name)
        {
            if(save_file_name == "")
            {
                save_file_name = Path.Combine(DirName, "SplIndf_" + WorkName + ".txt");
            }

            StreamWriter sw = new StreamWriter(save_file_name);

            sw.WriteLine(WorkName);

            // 写图片文件路径
            sw.WriteLine(DirName);

            // 写入类别信息
            for (int i = 0; i < NameClasses.Count; i++)
            {
                sw.WriteLine(string.Format("{0},{1},{2}", i, NameClasses[i], NumClasses[i]));
            }

            sw.WriteLine(Imgs.Count);

            // 写入每个样本的信息
            for (int i = 0; i < Imgs.Count; i++)
            {
                sw.Write(string.Format("{0},{1},{2}", Imgs[i].Classes, Imgs[i].NewClasses, Imgs[i].ImFileName));
                for (int j = 0; j < Imgs[i].SplInfo.Count; j++)
                {
                    sw.Write(",");
                    sw.Write(Imgs[i].SplInfo[j]);
                }
                sw.Write("\n");
            }

            sw.Close();
            
        }
    }
}
