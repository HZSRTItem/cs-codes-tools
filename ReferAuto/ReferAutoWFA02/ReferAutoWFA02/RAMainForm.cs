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

namespace ReferAutoWFA02
{
    public partial class RAMainForm : Form
    {
        public RAMainForm()
        {
            InitializeComponent();
        }
        List<ReferHZ> Refers = new List<ReferHZ>();
        private void TsbtnImportRefs_Click(object sender, EventArgs e)
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
    }
}

