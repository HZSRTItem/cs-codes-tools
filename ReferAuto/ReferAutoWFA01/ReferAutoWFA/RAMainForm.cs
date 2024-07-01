using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ReferAutoWFA
{
    public partial class RAMainForm : Form
    {
        public RAMainForm()
        {
            InitializeComponent();
        }


        List<ReferHZ> Refers = new List<ReferHZ>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NewMethod();
            // NewMethod2();
        }

        private void NewMethod1()
        {
            StreamReader sr = new StreamReader(@"D:\SpecialProjects\ReferAuto\temp\savedrecs.txt");
            List<string> linelist = new List<string>();
            string out_info = "";
            string line = sr.ReadLine();
            out_info += line + "\n";
            line = sr.ReadLine();
            out_info += line + "\n";
            ReferHZ referHZ = new ReferHZ();

            while (line != null)
            {
                if (line == "")
                {
                    continue;
                }

                string biaoshi = line.Substring(0, 2);

                if (biaoshi == "EF")
                {
                    break;
                }

                if (biaoshi == "ER")
                {
                    Refers.Add(referHZ);
                    referHZ = new ReferHZ();
                }
                else
                {
                    if (biaoshi == "  ")
                    {
                        linelist.Add(line.Substring(3));
                    }
                    else
                    {
                        RefAttr refAttr = ReferHZ.RIS2RefAttr(biaoshi);

                        if (refAttr == RefAttr.E_Error)
                        {
                            out_info += $"Error: {biaoshi} not find" + linelist.ToString();
                        }
                        else
                        {
                            referHZ.AddInfo(refAttr, linelist);
                        }

                        linelist.Clear();
                    }
                }

                line = sr.ReadLine();
            }
        }
        private void NewMethod2()
        {
            StreamReader sr = new StreamReader(@"D:\SpecialProjects\ReferAuto\temp\savedrecs.txt");
            List<string> linelist = new List<string>();
            string out_info = "";
            string line = sr.ReadLine();
            out_info += line + "\n";
            line = sr.ReadLine();
            out_info += line + "\n";
            ReferHZ referHZ = new ReferHZ();
            string ss = "";
            string biaoshi = "";
            string qianbiaoshi = "sd";


            while (line != null)
            {
                if (line == "")
                {
                    line = sr.ReadLine();
                    continue;
                }

                biaoshi = line.Substring(0, 2);

                if (biaoshi == "EF")
                {
                    break;
                }

                if (biaoshi == "ER")
                {
                    RefAttr refAttr = ReferHZ.RIS2RefAttr(qianbiaoshi);

                    if (refAttr == RefAttr.E_Error)
                    {
                        out_info += $"Error: {qianbiaoshi} not find" + string.Join(" ", linelist);
                    }
                    else
                    {
                        referHZ.AddInfo(refAttr, linelist);
                    }

                    ss += qianbiaoshi + ": " + string.Join(" | ", linelist) + "\n";

                    linelist.Clear();
                    Refers.Add(referHZ);
                    referHZ = new ReferHZ();
                }
                else
                {
                    if (biaoshi == "  ")
                    {
                        linelist.Add(line.Substring(3));
                    }
                    else
                    {
                        
                        RefAttr refAttr = ReferHZ.RIS2RefAttr(qianbiaoshi);

                        if (refAttr == RefAttr.E_Error)
                        {
                            out_info += $"Error: {qianbiaoshi} not find" + string.Join(" ", linelist) + "\n";
                        }
                        else
                        {
                            referHZ.AddInfo(refAttr, linelist);
                        }

                        ss += qianbiaoshi + ": " + string.Join(" | ", linelist) + "\n";

                        linelist.Clear();
                        qianbiaoshi = biaoshi;
                        linelist.Add(line.Substring(3));
                    }
                }

                line = sr.ReadLine();
            }
        }
        private void NewMethod()
        {
            string[] s = new string[2] { "sdfsd", "sdfd" };
            JArray jArray = JArray.FromObject(s);
            //  标识 英文名 中文名 类型  描述 变量名
            StreamReader sr = new StreamReader(@"D:\SpecialProjects\ReferAuto\temp\dcode.txt");
            string line = sr.ReadLine();
            string[] lines;
            while (line != null)
            {
                lines = line.Split('\t');
                //richTextBox1.Text += string.Format("/// <summary>\n");
                //richTextBox1.Text += string.Format("/// ID: {0}, desc: {1} {2}\n", lines[0], lines[2], lines[4]);
                //richTextBox1.Text += string.Format("/// </summary>\n");
                //richTextBox1.Text += string.Format("public {0} {1} = null;\n", lines[3], lines[5]);
                //richTextBox1.Text += string.Format("case RefAttr.E_{0}:\n", lines[5]);
                //richTextBox1.Text += string.Format("{0}={1}\n", lines[5], lines[6]);
                //richTextBox1.Text += string.Format("break;\n\n");
                //richTextBox1.Text += string.Format("case \"{0}\":\n", lines[0]);
                //richTextBox1.Text += string.Format("return RefAttr.E_{0};\n\n", lines[5]);
                richTextBox1.Text += string.Format("out_job.Add(\"{0}\", {0});\n", lines[5]);

                line = sr.ReadLine();
            }
            sr.Close();
        }
    }
}
