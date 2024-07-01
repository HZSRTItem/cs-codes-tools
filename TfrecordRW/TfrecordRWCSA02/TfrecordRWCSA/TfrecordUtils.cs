/*------------------------------------------------------------------------------
 * File    : TfrecordUtils
 * Time    : 2022/4/18 9:52:18
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[TfrecordUtils]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
using System.IO;
using System.Collections;

namespace TfrecordRWCSA
{
    class TfrecordUtils
    {
        public static string GetInfo(string tfrecord_file)
        {
            string out_str = "";
            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfrecord_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return "";
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 获得每一个 Feature 名
            string[] feat_names = new string[readexample.Features.Feature.Keys.Count];
            int i_feat_names = 0;
            foreach (string item in readexample.Features.Feature.Keys) feat_names[i_feat_names++] = item;
            // 解析数据
            readexample = Example.Parser.ParseFrom(readbytes);
            for (int i = 0; i < feat_names.Length; i++)
            {
                Feature feat = readexample.Features.Feature[feat_names[i]];
                out_str += feat_names[i] + ":";
                if (feat.BytesList != null)
                {
                    out_str += " BytesList-" + feat.BytesList.Value.Count.ToString();
                }
                else if (feat.FloatList != null)
                {
                    out_str += " FloatList-" + feat.FloatList.Value.Count.ToString();
                }
                else if (feat.Int64List != null)
                {
                    out_str += " Int64List-" + feat.Int64List.Value.Count.ToString();
                }
                else
                {
                    continue;
                }
                out_str += "\n";
            }
            tr.Dispose();
            return out_str.Trim();
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
