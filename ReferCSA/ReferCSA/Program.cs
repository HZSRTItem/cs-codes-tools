using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReferCSA
{
    class Program
    {
        static void Main(string[] args)
        {
        }


        static string DcRisToGB2015(string ris_file)
        {
            string out_s = "";
            string[] lines = File.ReadAllLines(ris_file);
            string title_name = "";
            List<string> ss = new List<string>(50);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line == "ER  -")
                {
                    break;
                }
                if (line == "")
                {
                    continue;
                }

                string line_mark = line.Substring(0, 2);
                line_mark = line_mark.Trim();
                string line_info = line.Substring(6);
                line_info = line_info.Trim();

                if (line_mark == "T1")
                {
                    title_name = line.Substring(6);
                }

               
            }
            return out_s;
        }
    }
}
