using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Refer_DengToEtal_CSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "test1.txt" };

            if (args.Length == 0)
            {
                Usage();
                return;
            }

            string refer_fn = args[0];
            string to_fn = null;
            StreamWriter sw = null;
            if (args.Length == 2)
            {
                to_fn = args[1];
                sw = new StreamWriter(to_fn);
            }
            StreamReader sr = new StreamReader(refer_fn);
            string line = sr.ReadLine();
            while (line != null)
            {
                line = line.Trim();
                if (line == "")
                {
                    continue;
                }
                string[] lines = line.Split('.');
                if (lines[0].Contains(", 等"))
                {
                    int ii = 0;
                    for (int i = 0; i < lines[0].Length; i++)
                    {
                        char c = lines[0][i];
                        
                        if (c > 'a' & c < 'z')
                        {
                            ii++;
                        }
                        else if(c > 'A' & c < 'Z')
                        {
                            ii++;
                        }
                    }
                    double t = ii * 1.0 / lines[0].Length;
                    if (t > 0.1)
                    {
                        line = line.Replace(" 等. ", " et al. ");
                    }
                }
                Console.WriteLine(line);
                sw?.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
            sw?.Close();
        }

        static void Usage()
        {
            Console.WriteLine("srt_referdengtoetal refer_file [to_file]");
        }
    }
}
