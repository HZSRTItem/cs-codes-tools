/*------------------------------------------------------------------------------
 * File    : Tfr2Npy
 * Time    : 2022/10/14 15:08:05
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Tfr2Npy]
------------------------------------------------------------------------------*/

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
using System.Data;

namespace Tfr2NpyCSA
{
    class Tfr2Npy
    {
        private List<string> fieldNames = new List<string>();
        private List<string> dataNames = new List<string>();
        private string csvFileName = null;

        private int n_current = 0;

        private int n_oned = 0;
        private int n_bands = 0;
        private DataTable dt = new DataTable();

        public Tfr2Npy(string csv_file)
        {
            csvFileName = csv_file;
        }

        ~Tfr2Npy()
        {

        }

        public void SaveToCsv()
        {
            using (StreamWriter sw = new StreamWriter(csvFileName))
            {
                using (CsvWriter csvWriter = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        csvWriter.WriteField(dt.Columns[i].ColumnName);
                    }
                    csvWriter.NextRecord();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            csvWriter.WriteField(dt.Rows[i][j]);
                        }
                        csvWriter.NextRecord();
                    }
                    csvWriter.NextRecord();
                }
            }
        }

        public int AddTFRecord(string tfr_file_name, string npy_file_name)
        {
            if (fieldNames.Count == 0 & dataNames.Count == 0)
            {
                if (!Init(tfr_file_name))
                {
                    return 0;
                }
            }

            List<double[][]> d = new List<double[][]>(1000);
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfr_file_name), true);
            int n_samples = 0;
            byte[] readbytes = tr.Read();
            Feature feat;
            while (readbytes != null)
            {
                n_current++;
                n_samples++;
                // 写入属性数据
                Example readexample = Example.Parser.ParseFrom(readbytes);
                //csvWriter.WriteField(n_current);
                DataRow dr = dt.NewRow();
                dr["_ID"] = n_current;
                for (int i = 0; i < fieldNames.Count; i++)
                {
                    feat = readexample.Features.Feature[fieldNames[i]];
                    if (feat.FloatList != null)
                    {
                        dr[fieldNames[i]] = feat.FloatList.Value[0];
                        //csvWriter.WriteField(feat.FloatList.Value[0]);
                    }
                    else
                    {
                        dr[fieldNames[i]] = feat.Int64List.Value[0];
                        //csvWriter.WriteField(feat.Int64List.Value[0]);
                    }
                }
                dr["_TFR_FILE_NAME"] = tfr_file_name;
                dr["_NPY_FILE_NAME"] = npy_file_name;
                dt.Rows.Add(dr);
                //csvWriter.WriteField(tfr_file_name);
                //csvWriter.WriteField(npy_file_name);
                //csvWriter.NextRecord();
                // 保存data数据
                double[][] d0 = new double[n_bands][];
                for (int i = 0; i < n_bands; i++)
                {
                    feat = readexample.Features.Feature[dataNames[i]];
                    if (feat.FloatList != null)
                    {
                        d0[i] = Array.ConvertAll(feat.FloatList.Value.ToArray(), Convert.ToDouble);
                    }
                    else
                    {
                        d0[i] = Array.ConvertAll(feat.Int64List.Value.ToArray(), Convert.ToDouble);
                    }
                }
                d.Add(d0);
                readbytes = tr.Read();
            }
            var arr = np.array(d.ToArray());
            np.save(npy_file_name, arr);
            d.Clear();
            return n_samples;
        }

        public int AddTFRecord(string tfr_file_name)
        {
            string npy_file_name = Path.ChangeExtension(tfr_file_name, ".npy");
            return AddTFRecord(tfr_file_name, npy_file_name);
        }

        private bool Init(string tfr_file_name)
        {

            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfr_file_name), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return false;
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 特征的类别数量
            int n = readexample.Features.Feature.Keys.Count;
            // 获得每一个 Feature 名
            string[] feat_names = readexample.Features.Feature.Keys.ToArray();
            // 解析数量
            bool[] is_one = new bool[n];
            int n_data0 = 0;
            for (int i = 0; i < feat_names.Length; i++)
            {
                Feature feat = readexample.Features.Feature[feat_names[i]];

                if (feat.Int64List != null)
                {
                    if (feat.Int64List.Value.Count == 1)
                    {
                        is_one[i] = true;
                    }
                    else
                    {
                        if (n_data0 == 0)
                        {
                            n_data0 = feat.Int64List.Value.Count;
                            dataNames.Add(feat_names[i]);
                        }
                        else
                        {
                            if (n_data0 == feat.Int64List.Value.Count)
                            {
                                dataNames.Add(feat_names[i]);
                            }
                        }
                    }
                    continue;
                }
                if (feat.FloatList != null)
                {
                    if (feat.FloatList.Value.Count == 1)
                    {
                        is_one[i] = true;
                    }
                    else
                    {
                        if (n_data0 == 0)
                        {
                            n_data0 = feat.FloatList.Value.Count;
                            dataNames.Add(feat_names[i]);
                        }
                        else
                        {
                            if (n_data0 == feat.FloatList.Value.Count)
                            {
                                dataNames.Add(feat_names[i]);
                            }
                        }
                    }
                    continue;
                }
                is_one[i] = false;
            }
            n_oned = n_data0;
            n_bands = dataNames.Count;
            Console.WriteLine("Find mark as follows:");
            for (int i = 0; i < dataNames.Count; i++)
            {
                Console.WriteLine("{0, 3} {1}", i + 1, dataNames[i]);
            }
            Console.Write("Do you want to change the order? [y/n]");
            string s = Console.ReadLine();
            if (s[0] == 'y')
            {
                Console.WriteLine("Please enter a new order like \"2,1,...\":");
                s = Console.ReadLine();
                string[] lines = s.Split(",");
                string[] dns = new string[dataNames.Count];
                for (int j = 0; j < lines.Length; j++)
                {
                    dns[j] = dataNames[int.Parse(lines[j])-1];
                    Console.WriteLine("{0, 3} {1}", j + 1, dns[j]);
                }
                dataNames.Clear();
                dataNames = dns.ToList();
            }
            Console.WriteLine("");
            dt.Columns.Add("_ID");
            //csvWriter.WriteField("_ID");
            for (int i = 0; i < feat_names.Length; i++)
            {
                if (is_one[i])
                {
                    fieldNames.Add(feat_names[i]);
                    dt.Columns.Add(feat_names[i]);
                    //csvWriter.WriteField(feat_names[i]);
                }
            }
            //csvWriter.WriteField("_TFR_FILE_NAME");
            //csvWriter.WriteField("_NPY_FILE_NAME");
            //csvWriter.NextRecord();
            dt.Columns.Add("_TFR_FILE_NAME");
            dt.Columns.Add("_NPY_FILE_NAME");
            tr.Dispose();
            return true;
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
        /// tfr中数量为1的参数转为csv
        /// </summary>
        /// <param name="tfr_file">Tfrecord file</param>
        /// <param name="csv_file">Csv file</param>
        /// <returns></returns>
        private static int Tfr2Npy3(string tfr_file, string csv_file)
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
    }
}

