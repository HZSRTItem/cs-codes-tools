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

namespace DataNormalizationWFA
{
    public partial class DNForm : Form
    {
        public DNForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            label7.Text = "";
            textBox1.Text = Directory.GetCurrentDirectory() + "\\" + "out.txt";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{

                NewMethod();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void NewMethod()
        {
            StreamWriter sw = new StreamWriter(textBox1.Text);
            if (textBox2.Text.Trim() == "")
            {
                throw new Exception("没有原始数据");
            }

            string[] guiyihuafanwei = textBox1_01.Text.Split(',');
            double xia = double.Parse(guiyihuafanwei[0]);
            double shang = double.Parse(guiyihuafanwei[1]);
            char shujufenge = ' ';
            if (comboBox1.Text == "\\t")
            {
                shujufenge = '\t';
            }
            else if (comboBox1.Text == "space")
            {
                shujufenge = ' ';
            }
            else if (comboBox1.Text == ",")
            {
                shujufenge = ',';
            }
            else
            {
                shujufenge = comboBox1.Text[0];
            }

            // 裁切范围
            if (richTextBox3_cq.Text.Trim() == "")
            {
                List<double[]> alld = new List<double[]>();
                string[] in_datas = textBox2.Text.Split('\n');
                int n_rows = in_datas.Length - 1;
                string[] lines = in_datas[0].Split(shujufenge);
                int n_colunms = lines.Length;
                for (int i = 0; i < n_colunms; i++)
                {
                    alld.Add(new double[n_rows]);
                }

                for (int i = 0; i < in_datas.Length; i++)
                {
                    lines = in_datas[i].Split(shujufenge);
                    if (lines[0] == "")
                    {
                        break;
                    }
                    if (lines.Length < n_colunms)
                    {
                        throw new Exception("原始数据列数不够 " + i.ToString() + " 行");
                    }
                    else
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            alld[j][i] = double.Parse(lines[j]);
                        }
                    }

                }

                string out_str0 = "";
                double[] fanwei0 = new double[n_colunms];
                double[] fanwei1 = new double[n_colunms];
                for (int i = 0; i < n_colunms; i++)
                {
                    fanwei0[i] = alld[i].Min();
                    fanwei1[i] = alld[i].Max();
                    out_str0 += (i + 1).ToString() + shujufenge;
                    out_str0 += fanwei0[i].ToString() + shujufenge;
                    out_str0 += fanwei1[i].ToString() + "\n";
                }
                richTextBox3_cq.Text = out_str0;

                progressForm progressForm0 = new progressForm();
                progressForm0.Show();
                double d = 0;
                for (int i = 0; i < n_rows; i++)
                {
                    for (int j = 0; j < n_colunms - 1; j++)
                    {
                        d = (alld[j][i] - fanwei0[j]) / (fanwei1[j] - fanwei0[j]) * (shang - xia) + xia;
                        sw.Write(d);
                        sw.Write(shujufenge);
                    }
                    d = (alld[n_colunms - 1][i] - fanwei0[n_colunms - 1]) / (fanwei1[n_colunms - 1] - fanwei0[n_colunms - 1]) * (shang - xia) + xia;
                    sw.Write(d);
                    sw.Write("\n");
                    progressForm0.AddProgress((int)(i * 1.0 / n_rows * 100));
                }
                progressForm0.Close();
                label7.Text = n_rows.ToString();
            }
            else
            {
                // 读取裁切范围
                string[] lines = null;
                string[] in_fanwei = richTextBox3_cq.Text.Split('\n');
                int n_colunms = in_fanwei.Length - 1;
                bool[] iscal = new bool[n_colunms];
                string[] ming = new string[n_colunms];
                for (int i = 0; i < n_colunms; i++)
                {
                    ming[i] = (i + 1).ToString();
                }
                double[] fanwei0 = new double[n_colunms];
                double[] fanwei1 = new double[n_colunms];
                for (int i = 0; i < n_colunms; i++)
                {
                    lines = in_fanwei[i].Split(shujufenge);
                    if (lines.Length == 1)
                    {
                        ming[i] = lines[0];
                        iscal[i] = false;
                    }
                    else if (lines.Length == 3)
                    {
                        ming[i] = lines[0];
                        iscal[i] = false;
                        fanwei0[i] = double.Parse(lines[1]);
                        fanwei1[i] = double.Parse(lines[2]);
                        if (fanwei1[i] < fanwei0[i])
                        {
                            throw new Exception("数据裁切范围应左边大于右边 " + i.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception("数据的裁切范围格式错误 " + i.ToString());
                    }
                }

                // 读取原始数据
                List<double[]> alld = new List<double[]>();
                string[] in_datas = textBox2.Text.Split('\n');
                int n_rows = in_datas.Length - 1;
                lines = in_datas[0].Split(shujufenge);
                for (int i = 0; i < n_colunms; i++)
                {
                    alld.Add(new double[n_rows]);
                }
                for (int i = 0; i < n_rows; i++)
                {
                    lines = in_datas[i].Split(shujufenge);
                    if (lines.Length < n_colunms)
                    {
                        throw new Exception("原始数据列数不够 " + i.ToString() + " 行");
                    }
                    else
                    {
                        for (int j = 0; j < lines.Length; j++)
                        {
                            alld[j][i] = double.Parse(lines[j]);
                        }
                    }
                }

                // 计算未定的裁切范围
                string out_str0 = "";
                for (int i = 0; i < n_colunms; i++)
                {
                    if (!iscal[i])
                    {
                        fanwei0[i] = alld[i].Min();
                        fanwei1[i] = alld[i].Max();
                    }

                    out_str0 += ming[i] + shujufenge;
                    out_str0 += fanwei0[i].ToString() + shujufenge;
                    out_str0 += fanwei1[i].ToString() + "\n";
                }
                richTextBox3_cq.Text = out_str0;

                // 计算结果
                progressForm progressForm0 = new progressForm();
                progressForm0.Show();
                double d = 0;
                for (int i = 0; i < n_rows; i++)
                {
                    for (int j = 0; j < n_colunms - 1; j++)
                    {
                        d = (alld[j][i] - fanwei0[j]) / (fanwei1[j] - fanwei0[j]) * (shang - xia) + xia;
                        d = d < xia ? xia : d;
                        d = d > shang ? shang : d;
                        sw.Write(d);
                        sw.Write(shujufenge);
                    }
                    d = (alld[n_colunms - 1][i] - fanwei0[n_colunms - 1]) / (fanwei1[n_colunms - 1] - fanwei0[n_colunms - 1]) * (shang - xia) + xia;
                    d = d < xia ? xia : d;
                    d = d > shang ? shang : d;
                    sw.Write(d);
                    sw.Write("\n");
                    progressForm0.AddProgress((int)(i * 1.0 / n_rows * 100));
                }
                label7.Text = n_rows.ToString();
                progressForm0.Close();
            }
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "txt file|*.txt";
            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            textBox1.Text = ofd.FileName;
        }

        private void DNForm_Load(object sender, EventArgs e)
        {

        }
    }
}
