using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NumSharp;
using TFRecordRW;
using CsvHelper;
using System.Globalization;

namespace Tfr2NpyCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "*" };
            if (args.Length == 0)
            {
                Console.WriteLine(Usage());
                return;
            }
            List<string> tfr_file_names = new List<string>(256);
            List<string> npy_file_names = new List<string>(256);
            string csv_file = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-csv" & i < args.Length - 1)
                {
                    csv_file = args[i + 1];
                    i++;
                }
                else if (args[i] == "-tfr" & i < args.Length - 1)
                {
                    if (File.Exists(args[i + 1]))
                    {
                        tfr_file_names.Add(args[i + 1]);
                    }
                    else
                    {
                        Console.WriteLine("Can not find file: " + args[i + 1]);
                    }
                    i++;
                }
                else if (args[i] == "-npy" & i < args.Length - 1)
                {
                    npy_file_names.Add(args[i + 1]);
                    i++;
                }
                else if (args[i] == "*")
                {
                    GetFiles(tfr_file_names, Directory.GetCurrentDirectory());
                }
                else if (args[i] == "-dir" & i < args.Length - 1)
                {
                    GetFiles(tfr_file_names, args[i + 1]);
                    i++;
                }
                else
                {

                }
            }

            if (csv_file == null)
            {
                csv_file = Directory.GetCurrentDirectory();
                csv_file = Path.Combine(csv_file, Path.GetFileName(csv_file) + "_tfr2npy.csv");
            }

            Tfr2Npy tfr2Npy = new Tfr2Npy(csv_file);

            int n_min = tfr_file_names.Count < npy_file_names.Count ? tfr_file_names.Count : npy_file_names.Count;
            int k = 0;
            for (; k < n_min; k++)
            {
                try
                {
                    int n = tfr2Npy.AddTFRecord(tfr_file_names[k], npy_file_names[k]);
                    Console.WriteLine("> TFR " + (k + 1).ToString() + ":" + n.ToString());
                    Console.WriteLine("  * " + tfr_file_names[k]);
                    Console.WriteLine("  ->" + npy_file_names[k]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            for (; k < tfr_file_names.Count; k++)
            {
                try
                {
                    int n = tfr2Npy.AddTFRecord(tfr_file_names[k]);
                    Console.WriteLine("> TFR " + (k + 1).ToString() + ":" + n.ToString());
                    Console.WriteLine("  * " + tfr_file_names[k]);
                    Console.WriteLine("  ->" + Path.ChangeExtension(tfr_file_names[k], ".npy"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + tfr_file_names[k]);
                    Console.WriteLine(ex.Message);
                }
            }

            tfr2Npy.SaveToCsv();
            Console.WriteLine("End");
        }

        static string Usage()
        {
            string usage = "srt_tfr2npy [*] [-csv csv file] [-tfr tfr file] [opt:-npy npy file]\n" +
                           "            [-dir folder tfrs]\n" +
                "    *: input all current folder tfrecord file\n" +
                "    [-tfr tfr file]: input one tfrecord file\n" +
                "    [opt:-csv csv file]: save info csv file default:current_dir.csv\n" +
                "    [opt:-npy npy file]: output one npy file default:tfr_file.npy\n" +
                "    [-dir folder tfrs]: add all *.tfrecord in dir";
            return usage;
        }

        static void GetFiles(List<string> fs, string in_dir)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(in_dir);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Extension == ".tfrecord")
                {
                    fs.Add(fileInfos[i].FullName);
                }
            }
        }

    }



}
