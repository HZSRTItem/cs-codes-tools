using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ogr = OSGeo.OGR;
using osr = OSGeo.OSR;
using System.IO;

namespace SampleIdentifWFA01
{
    public partial class BuildPrjForm : Form
    {
        public BuildPrjForm()
        {
            InitializeComponent();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        /// <summary>
        /// 投影信息
        /// </summary>

        public SampleDT OSampleDT;
        public string ORemoteImageFile;
        public bool isbuild = false;

        DataTable dataTable;

        string initDir = Directory.GetCurrentDirectory();

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "CSV File|*.csv";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Multiselect = false;

            if (!(openFileDialog.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            textBox1.Text = openFileDialog.FileName;
            initDir = Path.GetDirectoryName(openFileDialog.FileName);

            dataTable = OpenCSV(openFileDialog.FileName);
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                checkedListBox1.Items.Add(dataTable.Columns[i].Caption);
                checkedListBox2.Items.Add(dataTable.Columns[i].Caption);
                checkedListBox3.Items.Add(dataTable.Columns[i].Caption);
                checkedListBox4.Items.Add(dataTable.Columns[i].Caption);
            }
            //if (dataTable.Columns.Count != 0)
            //{
            //    checkedListBox1.SetItemChecked(0, true);
            //    checkedListBox2.SetItemChecked(0, true);
            //    checkedListBox3.SetItemChecked(0, true);
            //    checkedListBox4.SetItemChecked(0, true);
            //}
        }

        string cate_name = "CATE";
        string srt_name = "SRT";
        string x_name = "X";
        string y_name = "Y";

        private void button5_Click(object sender, EventArgs e)
        {
            AttrsForm attrsForm = new AttrsForm(dataTable);
            attrsForm.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (checkedListBox1.CheckedItems.Count > 0)
            {
                cate_name = checkedListBox1.CheckedItems[0].ToString();
            }
            else
            {
                MessageBox.Show("请选择类别列名", "提示");
                return;
            }

            if (checkedListBox2.CheckedItems.Count > 0)
            {
                x_name = checkedListBox2.CheckedItems[0].ToString();
            }
            else
            {
                MessageBox.Show("请选择X坐标列名", "提示");
                return;
            }

            if (checkedListBox3.CheckedItems.Count > 0)
            {
                srt_name = checkedListBox3.CheckedItems[0].ToString();
            }
            else
            {
                MessageBox.Show("请选择唯一标识符列名", "提示");
                return;
            }

            if (checkedListBox4.CheckedItems.Count > 0)
            {
                y_name = checkedListBox4.CheckedItems[0].ToString();
            }
            else
            {
                MessageBox.Show("请选择Y坐标列名", "提示");
                return;
            }

            //OSampleDT = new SampleDT(dataTable, "CATE", "SRT", "X", "Y");

            OSampleDT = new SampleDT(dataTable, cate_name, srt_name, x_name, y_name);
            isbuild = true;
            Close();
        }

        #region MyRegion
        /// <summary>
        /// 读取csv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable OpenCSV(string filePath)//从csv读取数据返回table
        {
            System.Text.Encoding encoding = GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);

            System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);


            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',', '\t');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',', '\t');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
        /// <param name="FILE_NAME">文件路径</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Text.Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        /// 通过给定的文件流，判断文件的编码类型
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(System.IO.FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            System.Text.Encoding reVal = System.Text.Encoding.Default;

            System.IO.BinaryReader r = new System.IO.BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = System.Text.Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = System.Text.Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = System.Text.Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }

        /// 判断是否是不带 BOM 的 UTF8 格式
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;  //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
        #endregion

        string singleImageDir;
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkedListBox3.CheckedItems.Count > 0)
            {
                srt_name = checkedListBox3.CheckedItems[0].ToString();
            }
            else
            {
                MessageBox.Show("请选择唯一标识符列名", "提示");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = initDir;

            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            singleImageDir = fbd.SelectedPath;
            textBox2.Text = singleImageDir;

            DirectoryInfo root = new DirectoryInfo(singleImageDir);
            FileInfo[] files = root.GetFiles();
            List<string> arrRate = dataTable.AsEnumerable().Select(d => d.Field<string>(srt_name)).ToList();
            if(dataTable.Columns.IndexOf("_SINGLE_IMAGE") == -1)
            {
                dataTable.Columns.Add("_SINGLE_IMAGE");
            }
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".png")
                {
                    string srt = Path.GetFileNameWithoutExtension(files[i].Name);
                    int n = arrRate.IndexOf(srt);
                    if (n != -1)
                    {
                        dataTable.Rows[n]["_SINGLE_IMAGE"] = files[i].FullName;
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Tiff Image|*.tif";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ORemoteImageFile = openFileDialog.FileName;
            textBox3.Text = ORemoteImageFile;
        }
    }
}
