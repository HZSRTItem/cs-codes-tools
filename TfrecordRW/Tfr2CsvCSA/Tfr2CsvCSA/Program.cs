using System;
using Tensorflow;
using System.IO;
using System.Linq;

namespace Tfr2CsvCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Tfr2Csv(args[0], args[1]);
            //string tfr_file = @"C:\Users\ASUS\Downloads\TestSamples_info_2021_S30.tfrecord\TestSamples_info_2021_S30.tfrecord";
            //Tfr2Csv(tfr_file, "t01.csv");
            //Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// tfr中数量为1的参数转为csv
        /// </summary>
        /// <param name="tfr_file">Tfrecord file</param>
        /// <param name="csv_file">Csv file</param>
        /// <returns></returns>
        private static int Tfr2Csv(string tfr_file, string csv_file)
        {
            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfr_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return 0;
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 特征的类别数量
            int n = readexample.Features.Feature.Keys.Count;
            // 获得每一个 Feature 名
            string[] feat_names = readexample.Features.Feature.Keys.ToArray();
            // 解析数量
            bool[] is_one = new bool[n];
            StreamWriter sw = new StreamWriter(csv_file);
            Feature feat;
            for (int i = 0; i < feat_names.Length; i++)
            {
                feat = readexample.Features.Feature[feat_names[i]];
                if (feat.Int64List?.Value.Count == 1)
                {
                    is_one[i] = true;
                }
                else if (feat.FloatList?.Value.Count == 1)
                {
                    is_one[i] = true;
                }
                else
                {
                    is_one[i] = false;
                }
            }
            int n_is_one = 0;
            for (int i = 0; i < feat_names.Length; i++)
            {
                if (is_one[i])
                {
                    feat_names[n_is_one++] = feat_names[i];
                }
            }
            // 写入csv
            // 写入文件头
            for (int i = 0; i < n_is_one - 1; i++)
            {
                sw.Write(feat_names[i]);
                sw.Write(',');
            }
            sw.Write(feat_names[n_is_one - 1]);
            sw.Write('\n');
            // 写入数据
            int n_samples = 0;
            while (readbytes != null)
            {
                n_samples++;
                readexample = Example.Parser.ParseFrom(readbytes);
                for (int i = 0; i < n_is_one - 1; i++)
                {
                    feat = readexample.Features.Feature[feat_names[i]];
                    if (feat.FloatList != null)
                    {
                        sw.Write(feat.FloatList.Value[0]);
                        sw.Write(',');
                    }
                    else
                    {
                        sw.Write(feat.Int64List.Value[0]);
                        sw.Write(',');
                    }
                }
                feat = readexample.Features.Feature[feat_names[n_is_one - 1]];
                if (feat.FloatList != null)
                {
                    sw.Write(feat.FloatList.Value[0]);
                    sw.Write('\n');
                }
                else
                {
                    sw.Write(feat.Int64List.Value[0]);
                    sw.Write('\n');
                }
                readbytes = tr.Read();
            }
            sw.Close();
            return n_samples;
        }


        /// <summary>
        /// tfrecord 转为一个txt文件
        /// </summary>
        /// <param name="tfrecord_file"></param>
        /// <param name="out_file"></param>
        public static void TfrecordTo1Txt(string tfrecord_file, string out_file)
        {
            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfrecord_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return;
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 获得每一个 Feature 名
            string[] feat_names = new string[readexample.Features.Feature.Keys.Count];
            int i_feat_names = 0;
            foreach (string item in readexample.Features.Feature.Keys) feat_names[i_feat_names++] = item;
            // 首先写如特征名
            StreamWriter sw = new StreamWriter(out_file);
            //sw.Write("\n");
            for (int i = 0; i < feat_names.Length - 1; i++)
            {
                sw.Write(feat_names[i]);
                sw.Write(",");
            }
            sw.Write(feat_names[feat_names.Length - 1]);
            sw.Write("\n");
            // 写入数据
            int n_samples = 0;
            while (readbytes != null)
            {
                n_samples++;
                readexample = Example.Parser.ParseFrom(readbytes);
                for (int i = 0; i < feat_names.Length; i++)
                {
                    Feature feat = readexample.Features.Feature[feat_names[i]];
                    if (feat.BytesList != null)
                    {
                        var tt = feat.BytesList.Value;
                        foreach (var item in tt)
                        {
                            sw.Write(item);
                            sw.Write(",");
                        }
                    }
                    else if (feat.FloatList != null)
                    {
                        var tt = feat.FloatList.Value;
                        foreach (var item in tt)
                        {
                            sw.Write(item);
                            sw.Write(",");
                        }
                    }
                    else if (feat.Int64List != null)
                    {
                        var tt = feat.Int64List.Value;
                        foreach (var item in tt)
                        {
                            sw.Write(item);
                            sw.Write(",");
                        }
                    }
                    else
                    {
                        continue;
                    }
                    sw.Write("\n");

                    //var tt = t.FloatList.Value.GetEnumerator();
                }
                readbytes = tr.Read();
            }
            // 写入样本的数量
            //sw.Write(n_samples);
            sw.Close();
            tr.Dispose();
        }
    }
}
