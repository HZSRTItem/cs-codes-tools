using System;
using System.IO;
using Tensorflow;
using System.Collections;
using System.Collections.Generic;

namespace TfrecordRWCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string tfrecord_file = Path.GetFullPath(args[0]); // "gba_spl17_test2_1.tfrecord"
            string out_file = Path.GetFullPath(args[1]);
            //Console.WriteLine(tfrecord_file);
            //Console.WriteLine(out_file);
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
                    else if(feat.FloatList != null)
                    {
                        var tt = feat.FloatList.Value;
                        foreach (var item in tt)
                        {
                            sw.Write(item);
                            sw.Write(",");
                        }
                    }
                    else if(feat.Int64List != null)
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
