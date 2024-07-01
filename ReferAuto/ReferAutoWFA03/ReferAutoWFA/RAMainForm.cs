using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ReferAutoWFA
{
    public partial class RAMainForm : Form
    {
        /* 文件组织格式
         * 
         * 文献: 编号 \t 文献题目名 \t DOI \t 引用 \t 摘要
         * 引用：编号 \t 引用文章编号 \t 被引用文章编号(-1为自己) \t 引用内容
         * 参考: 文章 \t DOI \t 摘要
         */

        public RAMainForm()
        {
            InitializeComponent();
            InitDir = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 项目文件
        /// </summary>
        public string PrjFile = @"";
        /// <summary>
        /// 文献列表文件
        /// </summary>
        public string RefListFile = @"";
        /// <summary>
        /// 原始路径
        /// </summary>
        public string InitDir = @"";

        public string FileArticles = @"D:\文献\temp\refauto\Articles01.txt";
        public string FileRefers = @"D:\文献\temp\refauto\ReferInfo01.txt";

        public List<string> ArticlesTitle = new List<string>();
        public List<string> ArticlesDOI = new List<string>();
        public List<string> ArticlesAbstract = new List<string>();
        /// <summary>
        /// 引用文章编号为 0 其他的为被引用的
        /// </summary>
        public List<int> ArticleNumRefs = new List<int>();
        public List<string> ArticlesRefs = new List<string>();

        private int RowIndex1 = -1;

        private void TsmiAddArticleList_Click(object sender, EventArgs e)
        {
            BuildForm buildForm = new BuildForm();
            buildForm.ShowDialog();
            if(buildForm.IsSelected)
            {
                PrjFile = buildForm.PrjFile;
                RefListFile = buildForm.RefListFile;
                InitDir = buildForm.InitDir;
                AddRefList();
            }
        }

        private void AddRefList()
        {
            // 读入文献列表数据
            string all_line = File.ReadAllText(RefListFile);
            string[] lines = all_line.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                // 题目 DOI 摘要
                string[] line_infos = lines[i].Split('\t');
                ArticleNumRefs.Add(IsInArticlesFile(line_infos));

                if (line_infos[1].Trim() != "")
                {
                    ArticlesDOI[ArticlesDOI.Count - 1] = line_infos[1].Trim();
                }
                if (line_infos[2].Trim() != "")
                {
                    ArticlesAbstract.Add(line_infos[2].Trim());
                }
                ArticlesRefs.Add(GetReferFromFile(ArticleNumRefs[0], ArticleNumRefs[ArticleNumRefs.Count-1]));

                int irows = DgvArticles.Rows.Add();
                DgvArticles.Rows[irows].Cells[0].Value = irows + 1;
                DgvArticles.Rows[irows].Cells[1].Value = line_infos[0];
                DgvArticles.Rows[irows].Cells[2].Value = line_infos[1];
            }
            TxtFile.Text = PrjFile;
            // 对比原始文献列表，看是不是存在，获得原始层编号，如果不存在，则加入，存在则获得编号
            // 编号 \t 题目名 \t DOI \t 引用文献编号:引用文字编号 \t 摘要
            // 对比的时候，查看DOI摘要是不是存在，如果不存在提醒并添加

        }

        private string GetReferFromFile(int x, int y)
        {
            StreamReader sr = new StreamReader(FileRefers);
            string line = sr.ReadLine();

            while(line != null)
            {
                if(line == "")
                {
                    line = sr.ReadLine();
                    continue;
                }
                string[] lines = line.Split('\t');

                int xx = int.Parse(lines[1]);
                int yy = int.Parse(lines[2]);

                if( x == xx & y == yy)
                {
                    sr.Close();
                    return lines[3];
                }

                line = sr.ReadLine();
            }
            sr.Close();

            return "";
        }

        private int IsInArticlesFile(string[] line_infos)
        {
            StreamReader sr = new StreamReader(FileArticles);
            string line = sr.ReadLine();
            int i = 0;
            bool ist = false;
            string[] lines = null;

            while (line != null)
            {
                if (line == "")
                {
                    line = sr.ReadLine();
                    continue;
                }

                i++;

                lines = line.Split('\t');

                if (line_infos[1].Trim() != "") // 比较DOI
                {
                    if (lines[2].Trim() != "")
                    {
                        if (lines[2].Trim() == line_infos[1].Trim())
                        {
                            ist = true;
                            break;
                        }
                    }
                    else
                    {
                        if (lines[1].Trim() == line_infos[0].Trim())
                        {
                            ist = true;
                            break;
                        }
                    }
                }
                else // 比较题目
                {
                    if (lines[1].Trim() == line_infos[0].Trim())
                    {
                        ist = true;
                        break;
                    }
                }

                line = sr.ReadLine();
            }

            sr.Close();

            if (ist == true)
            {
                ArticlesTitle.Add(lines[1].Trim());
                ArticlesDOI.Add(lines[2].Trim());
                ArticlesAbstract.Add(lines[3].Trim());
            }

            if (ist == false)
            {
                line = string.Format("{0}\t{1}\t{2}\t{3}", i + 1, line_infos[0].Trim(), line_infos[1].Trim(), line_infos[2].Trim());
                File.AppendAllText(FileArticles, "\n" + line);
            }

            return i;
        }
        

        private void DgvArticles_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ArticlesAbstract.Count != 0)
            {
                try
                {
                    if (RowIndex1 != -1)
                    {
                        ArticlesAbstract[RowIndex1] = RtbInfo.Text;
                        ArticlesRefs[RowIndex1] = RtbRefer.Text;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("1: " + ex.Message);
                }

                try
                {
                    RtbInfo.Text = ArticlesAbstract[e.RowIndex];
                    RtbRefer.Text = ArticlesRefs[e.RowIndex];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("2: " + ex.Message);
                }

                try
                {
                    for (int i = 0; i < DgvArticles.Rows.Count - 1; i++)
                    {
                        ArticlesTitle[i] = DgvArticles.Rows[i].Cells[1].Value.ToString();
                        ArticlesDOI[i] = DgvArticles.Rows[i].Cells[2].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("3: " + ex.Message);
                }

                RowIndex1 = e.RowIndex;
            }
        }

        /* 文件组织格式
         * 
         * 文献: 编号 \t 文献题目名 \t DOI \t 摘要
         * 引用：编号 \t 引用文章编号 \t 被引用文章编号(-1为自己) \t 引用内容
         * 参考: 文章 \t DOI \t 摘要
         */

        private void TsbtnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DgvArticles.Rows.Count-1; i++)
            {
                ArticlesTitle[i] = DgvArticles.Rows[i].Cells[1].Value.ToString();
                ArticlesDOI[i] = DgvArticles.Rows[i].Cells[2].Value.ToString();
            }

            // 保存文献列表 ArticlesFile
            SaveArticlesList();

            // 添加 引用
            SaveRefsList();
        }

        private void SaveRefsList()
        {
            // 先遍历所有的参考文献，看看是否存在由 文章 -> 引用 结构
            // 
            string[] all_lines = File.ReadAllLines(FileRefers);
            List<bool> isinline = new List<bool>(ArticleNumRefs.Count);
            for (int i = 0; i < ArticleNumRefs.Count; i++)
            {
                isinline.Add(true);
            }
            int n_lines = 0;

            for (int i = 0; i < all_lines.Length; i++)
            {
                if (all_lines[i] == "")
                {
                    continue;
                }

                string[] lines = all_lines[i].Split('\t');
                n_lines++;

                for (int j = 1; j < ArticleNumRefs.Count; j++)
                {
                    int x = int.Parse(lines[1]);
                    int y = int.Parse(lines[2]);
                    if (x == ArticleNumRefs[0] & y == ArticleNumRefs[j])
                    {
                        lines[3] = ArticlesRefs[j].Trim();
                        isinline[j] = false;
                        all_lines[i] = string.Join("\t", lines);
                        break;
                    }
                }
            }

            File.WriteAllLines(FileRefers, all_lines);

            for (int i = 1; i < ArticleNumRefs.Count; i++)
            {
                if (isinline[i] == true)
                {
                    n_lines++;
                    string line = "\n";
                    line += n_lines.ToString() + "\t";
                    line += ArticleNumRefs[0].ToString()+ "\t";
                    line += ArticleNumRefs[i].ToString() + "\t";
                    line += ArticlesRefs[i].ToString().Trim().Replace("\n", " ");
                    File.AppendAllText(FileRefers, line);
                }
            }
        }

        private void SaveArticlesList()
        {
            string[] art_all = File.ReadAllText(FileArticles).Split('\n');
            int n_line = 0;
            for (int i = 0; i < art_all.Length; i++)
            {
                if (art_all[i] == "")
                {
                    continue;
                }

                string[] lines = art_all[i].Split('\t');
                n_line++;

                for (int j = 0; j < ArticleNumRefs.Count; j++)
                {
                    if (int.Parse(lines[0]) == ArticleNumRefs[j])
                    {
                        // 编号不用改
                        lines[1] = ArticlesTitle[j].Replace("\n", " "); // 标题
                        lines[2] = ArticlesDOI[j].Replace("\n", " "); // DOI
                        lines[3] = ArticlesAbstract[j].Replace("\n", " "); // 摘要
                        art_all[i] = string.Join("\t", lines);
                        break;
                    }
                }
            }
            File.WriteAllText(FileArticles, string.Join("\n", art_all));
        }

        private void TsbtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            ofd.InitialDirectory = InitDir;

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            RefListFile = ofd.FileName;
            InitDir = Path.GetDirectoryName(ofd.FileName);
            PrjFile = Path.Combine(InitDir, "ref.txt");
            AddRefList();
        }
    }
}
