/*------------------------------------------------------------------------------
 * File    : SrtArt
 * Time    : 2022/9/29 10:27:05
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[SrtArt]
 * 
 * private void button2_Click(object sender, EventArgs e)
{
    //创建XML对象
    XDocument xDoc = new XDocument();
    //创建一个根节点
    XElement root = new XElement("Root");
    xDoc.Add(root); //将根节点加入到XML对象中
    //创建一个子节点
    XElement xele = new XElement("User");
    root.Add(xele);
    //添加属性
    XAttribute attr = new XAttribute("ID", 1);
    xele.Add(attr);
    xele.SetElementValue("Name", "张三");
    xele.SetElementValue("Age", "18");

    //保存xml文件
    xDoc.Save("User.xml");
}
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using JiebaNet.Segmenter;
using System.Diagnostics;

namespace SrtArticleCSA
{
    class SrtArt
    {
        /// <summary>
        /// 所有的文章
        /// </summary>
        public List<OneArt> Arts = new List<OneArt>(1000);
        /// <summary>
        /// 序列化XML
        /// </summary>
        private readonly XmlSerializer XmlSer = new XmlSerializer(typeof(OneArt[]));
        /// <summary>
        /// IntList
        /// </summary>
        private List<int> IntList = new List<int>(1000);
        /// <summary>
        /// 分词器
        /// </summary>
        private readonly JiebaSegmenter segmenter = new JiebaSegmenter();

        /// <summary>
        /// 构造
        /// </summary>
        public SrtArt()
        {
            // 需要找到所有的文件名，文件名编号
            ReadAll(StaticInfo.InfoFileName);
            for (int i = 0; i < Arts.Count; i++)
            {
                IntList.Add(0);
            }
        }

        /// <summary>
        /// 保存所有
        /// </summary>
        /// <param name="info_file_name">信息文件 .xml</param>
        /// <returns></returns>
        public bool SaveAll(string info_file_name)
        {
            if (File.Exists(info_file_name))
            {
                File.Delete(info_file_name);
            }

            using (FileStream fs = File.OpenWrite(info_file_name))
            {
                XmlSer.Serialize(fs, Arts.ToArray());
            }

            return true;
        }

        /// <summary>
        /// 读取所有信息
        /// </summary>
        /// <param name="info_file_name">信息文件 .xml</param>
        /// <returns></returns>
        public bool ReadAll(string info_file_name)
        {
            if (File.Exists(info_file_name))
            {
                using (FileStream fs = File.OpenRead(info_file_name))
                {
                    Arts = new List<OneArt>((OneArt[])XmlSer.Deserialize(fs));
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加一个文章
        /// </summary>
        /// <param name="art_name"></param>
        /// <param name="link_files"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Add(string art_name, string[] link_files = null, string info = "")
        {

            string[] art_names = FenCi(art_name);
            if (!ContainsFind(art_names))
            {
                Console.WriteLine("No similar article title.");
            }
            else
            {
                Console.WriteLine("Find similar articles as follows:");
                PrintFindInfo1(20);
            }
            Console.WriteLine("\n");

            int n_add = Arts.Count + 1;
            OneArt oneArt = new OneArt { Number = n_add, Title = art_name, Info = info };
            Console.WriteLine("The following information will be added:");
            Console.WriteLine("    Number of Article: {0}", n_add);
            Console.WriteLine("    Title of Article: {0}", art_name);
            if (link_files != null)
            {
                Console.WriteLine("    Link Files:", art_name);
                List<string> list = new List<string>(20);
                for (int i = 0; i < link_files.Length; i++)
                {
                    Console.WriteLine("        {0}", link_files[i]);
                }
                oneArt.LinkFiles = link_files;
            }
            if (info != "")
            {
                string[] tt = info.Split('\n');
                string ttt = "";
                for (int i = 0; i < tt.Length; i++)
                {
                    ttt += tt[i];
                    ttt += "\n        ";
                }
                Console.WriteLine("    Info of Article: \n        {0}", ttt);
            }

            // 询问是不是要添加
            Console.Write("Whether to add articles to the library? [y/n]: ");
            char y_n = (char)Console.Read();
            if (y_n == 'y') // 要添加
            {
                Arts.Add(oneArt);
                string dir1 = Path.Combine(StaticInfo.ArticleDirName, n_add.ToString());
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                    Console.WriteLine("Create Dir: {0}", dir1);
                }
                else
                {
                    Console.WriteLine("Exist Dir: {0}", dir1);
                }

                string f1 = Path.Combine(dir1, n_add.ToString() + ".md");
                if (!File.Exists(f1))
                {
                    File.Delete(f1);
                }

                Console.WriteLine("Record File: {0}", f1);
                string ws = "Title: " + art_name
                    + "\n\n# Question\n\n\n" + "# Method\n\n\n" + "# Conclusion\n\n\n";
                File.WriteAllText(f1, ws);

                SaveAll(StaticInfo.InfoFileName);
                Process.Start(dir1);
                Console.WriteLine("Success!");
            }
            Console.WriteLine("");

            return true;
        }

        /// <summary>
        /// 寻找文章名中的存在的信息
        /// </summary>
        /// <param name="find_s">文章名</param>
        /// <param name="infos">信息</param>
        /// <returns></returns>
        private bool ContainsFind(string[] infos)
        {
            bool b = false;
            for (int i = 0; i < Arts.Count; i++)
            {
                foreach (string item in infos)
                {
                    if (Arts[i].Title.Contains(item))
                    {
                        IntList[Arts[i].Number - 1]++;
                        b = true;
                    }
                }
            }
            return b;
        }

        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string[] FenCi(string s)
        {
            string[] segments = segmenter.Cut(s, cutAll: true).ToArray();
            segments = segments.Where(ss => !string.IsNullOrEmpty(ss)).ToArray();
            return segments;
        }

        /// <summary>
        /// 打印信息
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool PrintFindInfo1(int n = -1)
        {
            if (n > IntList.Count)
            {
                n = IntList.Count;
            }
            if (n == -1)
            {
                n = IntList.Count;
            }

            Console.WriteLine("    --------------------------------");
            Console.WriteLine("    {0} \t{1}", "No.", "Title");
            Console.WriteLine("    --------------------------------");
            for (int i = 0; i < n; i++)
            {
                int n_max = IntList.Max();
                if (n_max == 0)
                {
                    break;
                }
                int n_select = IntList.IndexOf(n_max);
                Console.WriteLine("    {0:D3} \t{1}", Arts[n_select].Number, Arts[n_select].Title);
                IntList[n_select] = 0;
            }
            Console.WriteLine("    end table...");
            return true;
        }

        /// <summary>
        /// 打印信息
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool PrintFindNInfo(int n = -1)
        {
            if (n > IntList.Count | n <= 0)
            {
                Console.WriteLine("Not find number:{0}", n);
                return false;
            }
            n = n - 1;
            Console.WriteLine("Number: {0}", n);
            Console.WriteLine("    Title: {0}", Arts[n].Title);
            //Console.WriteLine("    Number: {0}", Arts[n].Number);
            Console.WriteLine("    Link Files:");
            for (int i = 0; i < Arts[n].LinkFiles.Length; i++)
            {
                Console.WriteLine("        {0}", Arts[n].LinkFiles[i]);
            }
            string[] tt = Arts[n].Info.Split('\n');
            string ttt = "";
            for (int i = 0; i < tt.Length; i++)
            {
                ttt += tt[i];
                ttt += "\n        ";
            }
            Console.WriteLine("    Info of Article: \n        {0}", ttt);

            return true;
        }

        /// <summary>
        /// 输出所有
        /// </summary>
        /// <returns></returns>
        public bool PrintAll()
        {
            int n = IntList.Count;
            Console.WriteLine("    --------------------------------");
            Console.WriteLine("    {0} \t{1}", "No.", "Title");
            Console.WriteLine("    --------------------------------");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("    {0:D3} \t{1}", Arts[i].Number, Arts[i].Title);
            }
            Console.WriteLine("    end table...");
            return true;
        }

        public bool AddLinkFile(string link_file, int n)
        {
            if (n > IntList.Count | n <= 0)
            {
                Console.WriteLine("Not find number:{0}", n);
                return false;
            }
            return false;
        }
    }

    public class OneArt
    {
        /// <summary>
        /// 论文名
        /// </summary>
        public string Title = "";
        /// <summary>
        /// 论文编号
        /// </summary>
        public int Number = 0;
        /// <summary>
        /// 连接的文件
        /// </summary>
        public string[] LinkFiles = null;
        /// <summary>
        /// 添加的一些说明信息
        /// </summary>
        public string Info = "";
    }

    public class UUtils
    {
        /// <summary>
        /// 计算两个字符串的相似程度
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static double GetSimilarityWith(string s1, string s2)
        {
            int Kq = 2;
            int Kr = 1;
            int Ks = 1;

            char[] ss = s1.ToCharArray();
            char[] st = s2.ToCharArray();

            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;

            return Kq * q * 1.0 / (Kq * q + Kr * r + Ks * s);
        }

        public static string FmtStr(string s, int n = 0, int n_line = 70)
        {
            char[] in_chars = new char[s.Length * 2];
            int iline = 0;
            int ii = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                in_chars[ii++] = c;
                iline += c > 127 ? 2 : 1;
                if (iline > n_line)
                {
                    while (true)
                    {
                        if (c == ' ' | c > 127)
                        {
                            break;
                        }
                        c = s[++i];
                        in_chars[ii++] = c;
                    }
                    in_chars[ii++] = '\n';
                    for (int j = 0; j < n; j++)
                    {
                        in_chars[ii++] = ' ';
                    }
                    iline = n;
                }
            }
            string out_s = new string(in_chars);
            return out_s;
        }

        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="n"></param>
        /// <param name="n_line"></param>
        /// <param name="s"></param>
        public static void PrintFormat(int n = 0, int n_line = 70, string s = "")
        {
            if (s == "")
            {
                Console.WriteLine(s);
            }
            else
            {
                s = FmtStr(s, n: n, n_line: n_line);
                Console.WriteLine(s);
            }
        }
    }

}
