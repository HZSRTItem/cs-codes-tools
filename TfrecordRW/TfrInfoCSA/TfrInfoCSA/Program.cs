using System;
using System.Linq;
using Tensorflow;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TfrInfoCSA
{
    class Program
    {
        static void Usage()
        {
            Console.WriteLine(
                "srt_tfr2png [tfrecord file] [-r:name|number] [-g:name|number] [-b:name|number] [-size:int,int]\n" +
                "            [opt: -dir:dir] [opt: -rdmin:double] [opt: -rdmax:double] [opt: -gdmin:double]\n" +
                "            [opt: -gmax:double] [opt: -bdmin:double] [opt: -bdmax:double] [opt: -srt:string]\n" +
                "            [opt: -c:name] [opt: --info]\n" +
                "    tfrecord file: Input tfrecord file \n" +
                "    -r `name|number`: Red feature name\n" +
                "    -g `name|number`: Green feature name\n" +
                "    -b `name|number`: Blue feature name\n" +
                "    -size `int,int`: width,height\n" +
                "    opt: -dir `dir`: Output png dir\n" +
                "    opt: -rdmin `double`: Red stretch minimum\n" +
                "    opt: -gdmin `double`: Green stretch minimum\n" +
                "    opt: -bdmin `double`: Blue stretch minimum\n" +
                "    opt: -rdmax `double`: Red stretch maximum\n" +
                "    opt: -gdmax `double`: Green stretch maximum\n" +
                "    opt: -bdmax `double`: Blue stretch maximum\n" +
                "    opt: -dmin `double`: Stretch minimum\n" +
                "    opt: -dmax `double`: Stretch maximum\n" +
                "    opt: -srt `name|number`: Unique identifier. Defaults to read order\n" +
                "    opt: --info: Get tfrecord file information\n" +
                "(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        static void Main(string[] args)
        {
            //BmpRW bmpRW = new(@"D:\SpecialProjects\TfrecordRW\TfrInfoCSA\TfrInfoCSA\bin\Debug\net5.0\t05\1.bmp");

            //args = new string[]
            //{
            //    @"lm_scence4_3.tfrecord"
            //    //@"D:\SpecialProjects\WXLanBeiSample\tfr\2021\d_tspl2021_30_4.tfrecord"
            //    , "-r", "4"
            //    , "-g", "2"
            //    , "-b", "1"
            //    , "-size", "64,64"
            //    , "-dir", @"t06"
            //    , "-rdmax", "3000"
            //    , "-bdmax", "3000"
            //    , "-gdmax", "3000"
            //    , "srt", "SRT"
            //    //, "--info"
            //};

            #region main

            DateTime dateTime = DateTime.Now;

            if (args.Length == 0)
            {
                Usage();
                return;
            }


            string tfr_file = null;
            bool is_info = false;
            double r_dmin = 0;
            double r_dmax = 0;
            double g_dmin = 0;
            double g_dmax = 0;
            double b_dmin = 0;
            double b_dmax = 0;
            double dmin = 0;
            double dmax = 0;
            string srt = null;
            string cate_name = null;
            Arr<string> rgb_names = new Arr<string>(3);
            int w = 0;
            int h = 0;
            string png_dir = Directory.GetCurrentDirectory();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--info")
                {
                    is_info = true;
                }
                else if (args[i] == "-r" & i < args.Length - 1)
                {
                    rgb_names.Add(args[++i]);
                }
                else if (args[i] == "-g" & i < args.Length - 1)
                {
                    rgb_names.Add(args[++i]);
                }
                else if (args[i] == "-b" & i < args.Length - 1)
                {
                    rgb_names.Add(args[++i]);
                }
                else if (args[i] == "-dmin" & i < args.Length - 1)
                {
                    dmin = double.Parse(args[++i]);
                }
                else if (args[i] == "-dmax" & i < args.Length - 1)
                {
                    dmax = double.Parse(args[++i]);
                }
                else if (args[i] == "-rdmin" & i < args.Length - 1)
                {
                    r_dmin = double.Parse(args[++i]);
                }
                else if (args[i] == "-gdmin" & i < args.Length - 1)
                {
                    g_dmin = double.Parse(args[++i]);
                }
                else if (args[i] == "-bdmin" & i < args.Length - 1)
                {
                    b_dmin = double.Parse(args[++i]);
                }
                else if (args[i] == "-rdmax" & i < args.Length - 1)
                {
                    r_dmax = double.Parse(args[++i]);
                }
                else if (args[i] == "-gdmax" & i < args.Length - 1)
                {
                    g_dmax = double.Parse(args[++i]);
                }
                else if (args[i] == "-bdmax" & i < args.Length - 1)
                {
                    b_dmax = double.Parse(args[++i]);
                }
                else if (args[i] == "-srt" & i < args.Length - 1)
                {
                    srt = args[++i];
                }
                else if (args[i] == "-size" & i < args.Length - 1)
                {
                    string[] ss = args[++i].Split(',');
                    w = int.Parse(ss[0]);
                    h = int.Parse(ss[1]);
                }
                else if (args[i] == "-dir" & i < args.Length - 1)
                {
                    png_dir = args[++i];
                    if (!Directory.Exists(png_dir))
                    {
                        Directory.CreateDirectory(png_dir);
                    }
                }
                else if (args[i] == "-c" & i < args.Length - 1)
                {
                    cate_name = args[++i];
                }
                else if (tfr_file == null)
                {
                    tfr_file = args[i];
                }
            }

            if (tfr_file == null)
            {
                Console.WriteLine("Error: Not find tfrecord file");
                Usage();
                return;
            }

            int n_samples = 0;
            TfrFeatInfo[] tfrFeatInfos = GetTfrInfo(tfr_file, ref n_samples);

            // 输出 tfrecord file 的信息
            if (is_info)
            {
                LookTfr(tfr_file, tfrFeatInfos, n_samples);
                return;
            }

            int[] i_rgb_names = new int[3] { 0, 0, 0 };
            for (int i = 0; i < rgb_names.Count; i++)
            {
                i_rgb_names[i] = FindFeatName(tfrFeatInfos, rgb_names[i]);
                if (i_rgb_names[i] == -1) return;
            }

            if (!(dmin == 0 & dmax == 0))
            {
                r_dmin = dmin;
                g_dmin = dmin;
                b_dmin = dmin;
                r_dmax = dmax;
                g_dmax = dmax;
                b_dmax = dmax;
            }

            // 测试 srt
            int i_srt = srt != null ? FindFeatName(tfrFeatInfos, srt) : -1;

            // 测试类别名
            int i_cate = cate_name != null ? FindFeatName(tfrFeatInfos, cate_name) : -1;

            byte[] d = new byte[w * h * 3];

            string temp_f = DateTime.Now.ToString() + ".bmp";

            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfr_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            Example readexample;
            // 写入数据
            n_samples = 0;
            BmpRW bmpRW1 = new BmpRW(w, h);
            while (readbytes != null)
            {
                n_samples++;
                readexample = Example.Parser.ParseFrom(readbytes);
                // 获得每一个数据
                double[] R = GetFeatData(readexample, tfrFeatInfos[i_rgb_names[0]].Name);
                double[] G = GetFeatData(readexample, tfrFeatInfos[i_rgb_names[1]].Name);
                double[] B = GetFeatData(readexample, tfrFeatInfos[i_rgb_names[2]].Name);
                int srt_info = i_srt == -1 ? n_samples : (int)(GetFeatData(readexample, tfrFeatInfos[i_srt].Name)[0]);
                //int cate_info = i_cate == -1 ? 0 : (int)(GetFeatData(readexample, tfrFeatInfos[i_cate].Name)[0]);
                if (R.Length == w * h & G.Length == w * h & B.Length == w * h)
                {
                    int j = 0;
                    if (r_dmin == 0 & r_dmax == 0 & g_dmin == 0 & g_dmax == 0 & b_dmin == 0 & b_dmax == 0)
                    {
                        for (int i = 0; i < w * h; i++)
                        {
                            d[j++] = (byte)B[i];
                            d[j++] = (byte)G[i];
                            d[j++] = (byte)R[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < w * h; i++)
                        {
                            d[j++] = ReSize(ref B[i], ref b_dmin, ref b_dmax);
                            d[j++] = ReSize(ref G[i], ref g_dmin, ref g_dmax);
                            d[j++] = ReSize(ref R[i], ref r_dmin, ref r_dmax);
                        }
                    }

                    // 写入文件中
                    string out_png_file = Path.Combine(png_dir, srt_info.ToString() + ".png");
                    bmpRW1.Save("t0.bmp", d);
                    Bitmap bitmap1 = new Bitmap("t0.bmp");
                    bitmap1.Save(out_png_file, ImageFormat.Png);
                    bitmap1.Dispose();

                    //string out_png_file = Path.Combine(png_dir, srt_info.ToString() + ".bmp");
                    //bmpRW1.Save(out_png_file, d);
                }
                else
                {
                    Console.WriteLine($"Warning: {srt_info} data size can not resize to" +
                        $" [{w},{h}]: R[{R.Length}], G[{G.Length}], B[{B.Length}]");
                }
                readbytes = tr.Read();
            }
            // 写入样本的数量
            //sw.Write(n_samples);
            tr.Dispose();
            #endregion

            Console.WriteLine("Run time: " + (DateTime.Now - dateTime).ToString() + "s");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        private static double[] GetFeatData(Example readexample, string feat_name)
        {
            double[] tt = null;
            Feature feat = readexample.Features.Feature[feat_name];
            if (feat.FloatList != null)
            {
                tt = feat.FloatList.Value.ToArray().Select(x => (double)x).ToArray();
            }
            else if (feat.Int64List != null)
            {
                tt = feat.Int64List.Value.ToArray().Select(x => (double)x).ToArray();
            }
            return tt;
        }

        private static void LookTfr(string tfr_file, TfrFeatInfo[] tfrFeatInfos, int n_samples)
        {
            Console.WriteLine("> Look File:" + tfr_file + "\n");
            for (int i = 0; i < tfrFeatInfos.Length; i++)
            {
                Console.WriteLine(tfrFeatInfos[i].Name + ": " + (i + 1).ToString());
                Console.WriteLine("  Number: " + tfrFeatInfos[i].NEle.ToString());
                Console.WriteLine("  DType: " + tfrFeatInfos[i].Dtype.ToString());
                Console.WriteLine("  Sample[0]: " + tfrFeatInfos[i].AInfos.ToString() + "\n");
            }
            Console.WriteLine("Sample number: " + n_samples.ToString());
        }

        private static int FindFeatName(TfrFeatInfo[] tfrFeatInfos, string name)
        {
            int k = 0;
            int j = 0;
            for (; j < tfrFeatInfos.Length; j++)
            {
                if (name == tfrFeatInfos[j].Name)
                {
                    k = j;
                    break;
                }
            }
            if (j == tfrFeatInfos.Length)
            {
                try
                {
                    k = int.Parse(name) - 1;
                    if (k < 0 | k > tfrFeatInfos.Length - 1)
                    {
                        Console.WriteLine("Error: Input names can not find: " + k);
                        Usage();
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: names: " + k + " -- " + ex.Message);
                    Usage();
                    return -1;
                }
            }
            return k;
        }

        private static byte ReSize(ref double x, ref double dmin, ref double dmax)
        {
            if (x < dmin) return 0;
            if (x > dmax) return 255;
            return (byte)((x - dmin) / (dmax - dmin) * 255);
        }

        /// <summary>
        /// 获得一个文件的所有信息
        /// </summary>
        /// <param name="tfrecord_file">tfrecord文件</param>
        /// <returns></returns>
        private static TfrFeatInfo[] GetTfrInfo(string tfrecord_file, ref int n_samples)
        {
            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfrecord_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return null;
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 获得每一个 Feature 名
            string[] feat_names = new string[readexample.Features.Feature.Keys.Count];
            TfrFeatInfo[] tfrInfos = new TfrFeatInfo[readexample.Features.Feature.Keys.Count];
            int i_feat_names = 0;
            foreach (string item in readexample.Features.Feature.Keys)
            {
                tfrInfos[i_feat_names] = new TfrFeatInfo();
                tfrInfos[i_feat_names].Name = item;
                feat_names[i_feat_names++] = item;
            }
            // 解析数据
            readexample = Example.Parser.ParseFrom(readbytes);
            for (int i = 0; i < feat_names.Length; i++)
            {
                Feature feat = readexample.Features.Feature[feat_names[i]];
                if (feat.BytesList != null)
                {
                    tfrInfos[i].Dtype = "byte_list";
                    tfrInfos[i].NEle = feat.BytesList.Value.Count;
                    tfrInfos[i].AInfos = feat.BytesList.Value.ToString();
                }
                else if (feat.FloatList != null)
                {
                    tfrInfos[i].Dtype = "float_list";
                    tfrInfos[i].NEle = feat.FloatList.Value.Count;
                    NewMethod1(ref tfrInfos, i, feat);
                }
                else if (feat.Int64List != null)
                {
                    tfrInfos[i].Dtype = "int_list";
                    tfrInfos[i].NEle = feat.Int64List.Value.Count;
                    NewMethod1(ref tfrInfos, i, feat);
                }
                else
                {
                    continue;
                }
            }

            n_samples = 0;
            while (readbytes != null)
            {
                readbytes = tr.Read();
                n_samples++;
            }

            tr.Dispose();
            return tfrInfos;

            static void NewMethod1(ref TfrFeatInfo[] tfrInfos, int i, Feature feat)
            {
                var tt = feat.FloatList.Value.ToArray();
                string ss = "[";
                if (tfrInfos[i].NEle < 6)
                {
                    int j = 0;
                    for (; j < tt.Length - 1; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += tt[j].ToString() + "]";
                }
                else
                {
                    int j = 0;
                    for (; j < 3; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += "... ";
                    for (j = tt.Length - 4; j < tt.Length - 1; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += tt[j].ToString() + "]";
                }
                tfrInfos[i].AInfos = ss;
            }
        }

    }


    /// <summary>
    /// tfr 文件的每一个feature的信息
    /// </summary>
    class TfrFeatInfo
    {
        /// <summary>
        /// 特征名
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 特征的数据类型
        /// </summary>
        public string Dtype = "";
        /// <summary>
        /// 特征的数据的数量
        /// </summary>
        public int NEle = 0;
        /// <summary>
        /// 简要信息
        /// </summary>
        public string AInfos = "";
    }

    class Arr<T>
    {
        int N = 0;
        T[] arr;

        /// <summary>
        /// 元素的个数
        /// </summary>
        public int Count
        {
            get { return arr.Length; }
        }

        /// <summary>
        /// 构建一个有n个元素的容器
        /// </summary>
        /// <param name="n"></param>
        public Arr(int n)
        {
            arr = new T[n];
        }

        /// <summary>
        /// 添加一个元素
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(T t)
        {
            if (N < arr.Length)
            {
                arr[N++] = t;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="n">索引</param>
        /// <returns>数据</returns>
        public T this[int n]
        {
            set { if (n < arr.Length) arr[n] = value; }
            get { return n < N ? arr[n] : default; }
        }
    }


    class BmpRW
    {
        private uint FidSize;              // 4 Bytes 整个BMP文件的大小
        private ushort FidReserved1;        // 2 Bytes 保留，为0
        private ushort FidReserved2;        // 2 Bytes 保留，为0
        private uint FidOffBits;           // 4 Bytes 文件起始位置到图像像素数据的字节偏移量
        private uint Size;              // 4 Bytes INFOHEADER结构体大小，存在其他版本I NFOHEADE
        private int Width;              // 4 Bytes 图像宽度（以像素为单位）
        private int Height;             // 4 Bytes 图像高度，+：图像存储顺序为Bottom2Top，-：Top
        private ushort Planes;           // 2 Bytes 图像数据平面，BMP存储RGB数据，因此总为1
        private ushort BitCount;         // 2 Bytes 图像像素位数
        private uint Compression;       // 4 Bytes 0：不压缩，1：RLE8，2：RLE4
        private uint SizeImage;         // 4 Bytes 4字节对齐的图像数据大小
        private int XPelsPerMeter;      // 4 Bytes 用象素/米表示的水平分辨率
        private int YPelsPerMeter;      // 4 Bytes 用象素/米表示的垂直分辨率
        private uint ClrUsed;           // 4 Bytes 实际使用的调色板索引数，0：使用所有的调色板索引
        private uint ClrImportant;      // 4 Bytes 重要的调色板索引数，0：所有的调色板索引都重要
        private byte[] ClRgbQuad;
        private int Channels;
        private byte[] ImData = null;

        /// <summary>
        /// 使用图像文件构建对象
        /// </summary>
        /// <param name="bmp_file"></param>
        public BmpRW(string bmp_file)
        {
            ReadBmp(bmp_file);
        }

        /// <summary>
        /// 构建一个 RGB 空图像
        /// </summary>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        public BmpRW(int width, int height)
        {
            Channels = 3;
            Width = width;
            Height = height;



            int step = Channels * Width;
            int offset = step % 4;
            //step += offset == 4 ? 0 : 4 - offset;
            if (offset != 4)
            {
                step += 4 - offset;
            }

            FidSize = (uint)(Height * step + 54);
            FidReserved1 = 0;
            FidReserved2 = 0;
            FidOffBits = 54;
            Size = 40;
            Planes = 1;
            BitCount = 24;
            Compression = 0;
            SizeImage = 0;
            //SizeImage = (uint)(Height * step);
            XPelsPerMeter = 0;
            YPelsPerMeter = 0;
            ClrUsed = 0;
            ClrImportant = 0;
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="save_file">保存的影像</param>
        /// <param name="d">数据</param>
        /// <returns>是否保存成功</returns>
        public bool Save(string save_file, byte[] d)
        {
            FileStream fs = new(save_file, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter nary_writer = new(fs);
            nary_writer.Write('B');
            nary_writer.Write('M');
            int offset = Channels * Width % 4;
            WriteAttrs(nary_writer);
            byte t = 0;
            for (int i = Height - 1; i > -1; i--)
            {
                for (int j = 0; j < Width; j++)
                {
                    nary_writer.Write(d[i * Width * 3 + j * 3]);
                    nary_writer.Write(d[i * Width * 3 + j * 3 + 1]);
                    nary_writer.Write(d[i * Width * 3 + j * 3 + 2]);
                }
                if (offset != 0)
                {
                    for (int j = 0; j < 4 - offset; j++)
                    {
                        nary_writer.Write(t);
                    }
                }
            }
            fs.Close();
            return true;
        }

        /// <summary>
        /// 读取bmp影像
        /// </summary>
        /// <param name="bmp_file"></param>
        private void ReadBmp(string bmp_file)
        {
            // 读取数据
            FileStream fs = new FileStream(bmp_file, FileMode.Open, FileAccess.Read);
            BinaryReader nary_reader = new BinaryReader(fs);
            //byte[] byteArray = new byte[fs.Length];
            //fs.Read(byteArray, 0, byteArray.Length);

            // 读取头信息
            char mark1 = nary_reader.ReadChar();
            char mark2 = nary_reader.ReadChar();
            if (!(mark1 == 'B' & mark2 == 'M'))
            {
                throw new Exception("Not a bmp file: " + bmp_file);
            }

            FidSize = nary_reader.ReadUInt32();
            FidReserved1 = nary_reader.ReadUInt16();
            FidReserved2 = nary_reader.ReadUInt16();
            FidOffBits = nary_reader.ReadUInt32();
            Size = nary_reader.ReadUInt32();
            Width = nary_reader.ReadInt32();
            Height = nary_reader.ReadInt32();
            Planes = nary_reader.ReadUInt16();
            BitCount = nary_reader.ReadUInt16();
            Compression = nary_reader.ReadUInt32();
            SizeImage = nary_reader.ReadUInt32();
            XPelsPerMeter = nary_reader.ReadInt32();
            YPelsPerMeter = nary_reader.ReadInt32();
            ClrUsed = nary_reader.ReadUInt32();
            ClrImportant = nary_reader.ReadUInt32();

            //if (BitCount == 8)
            //{
            //    // 非真彩色
            //    Channels = 1;
            //    int offset = (int)((Channels * Width) % 4);
            //    if (offset != 0)
            //    {
            //        offset = 4 - offset;
            //    }
            //    ImData = new byte[Width * Height];
            //    int step = Channels * Width;
            //    ClRgbQuad = nary_reader.ReadBytes(256 * 4);
            //    // 数据读取
            //    for (int i = 0; i < Height; i++)
            //    {
            //        for (int j = 0; j < Width; j++)
            //        {
            //            ImData[(Height - 1 - i) * step + j] = nary_reader.ReadByte();
            //        }
            //        nary_reader.ReadBytes(offset);
            //    }
            //}
            //else if (BitCount == 24)
            //{
            //    // 真彩色
            //    Channels = 3;
            //    ImData = new byte[Width * Height * 3];
            //    int step = Channels * Width;
            //    int offset = (int)((Channels * Width) % 4);
            //    if (offset != 0)
            //    {
            //        offset = 4 - offset;
            //    }
            //    uint offset1 = FidOffBits - 4 * 13;
            //    nary_reader.ReadBytes((int)offset1);
            //    // 数据读取
            //    for (int i = 0; i < Height; i++)
            //    {
            //        for (int j = 0; j < Width; j++)
            //        {
            //            for (int k = 0; k < 3; k++)
            //            {
            //                ImData[(Height - 1 - i) * step + j * 3 + k] = nary_reader.ReadByte();
            //            }
            //        }
            //        nary_reader.ReadBytes(offset);
            //    }
            //}
            fs.Close();
        }

        /// <summary>
        /// 保存bmp图像
        /// </summary>
        /// <param name="save_bmp_file"></param>
        /// <returns></returns>
        public bool SaveBmp(string save_bmp_file)
        {
            // 读取数据
            FileStream fs = new FileStream(save_bmp_file, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter nary_writer = new BinaryWriter(fs);
            nary_writer.Write("BM");

            if (Channels == 3) // 24位，通道，彩图
            {
                int step = Channels * Width;
                int offset = (int)(step % 4);
                if (offset != 4)
                {
                    step += 4 - offset;
                }
                FidSize = (uint)(Height * step + 54);
                FidReserved1 = 0;
                FidReserved2 = 0;
                FidOffBits = 54;

                Size = 40;
                Width = Width;
                Height = Height;
                Planes = 1;
                BitCount = 24;
                Compression = 0;
                SizeImage = (uint)(Height * step);
                XPelsPerMeter = 0;
                YPelsPerMeter = 0;
                ClrUsed = 0;
                ClrImportant = 0;
                WriteAttrs(nary_writer);

                for (int i = (int)(Height - 1); i > -1; i--)
                {
                    for (int j = 0; j < Width; j--)
                    {
                        nary_writer.Write(ImData[i * Width * 3 + j * 3]);
                        nary_writer.Write(ImData[i * Width * 3 + j * 3 + 1]);
                        nary_writer.Write(ImData[i * Width * 3 + j * 3 + 2]);
                    }
                    for (int j = 0; j < offset; j++)
                    {
                        nary_writer.Write((byte)0);
                    }
                }
            }
            else if (Channels == 1) // 8位，单通道，灰度图
            {

                int step = Width;
                int offset = (int)(step % 4);
                if (offset != 4)
                {
                    step += 4 - offset;
                }

                FidSize = (uint)(54 + 256 * 4 + Width);
                FidReserved1 = 0;
                FidReserved2 = 0;
                FidOffBits = 54 + 256 * 4;
                nary_writer.Write(FidSize);
                nary_writer.Write(FidReserved1);
                nary_writer.Write(FidReserved2);
                nary_writer.Write(FidOffBits);
                Size = 40;
                Width = Width;
                Height = Height;
                Planes = 1;
                BitCount = 8;
                Compression = 0;
                SizeImage = (uint)(Height * step);
                XPelsPerMeter = 0;
                YPelsPerMeter = 0;
                ClrUsed = 256;
                ClrImportant = 256;
                nary_writer.Write(Size);
                nary_writer.Write(Width);
                nary_writer.Write(Height);
                nary_writer.Write(Planes);
                nary_writer.Write(BitCount);
                nary_writer.Write(Compression);
                nary_writer.Write(SizeImage);
                nary_writer.Write(XPelsPerMeter);
                nary_writer.Write(YPelsPerMeter);
                nary_writer.Write(ClrUsed);
                nary_writer.Write(ClrImportant);
                ClRgbQuad = new byte[255 * 4];
                for (int i = 0; i < 256; i++)
                {
                    ClRgbQuad[4 * i] = (byte)i;
                    ClRgbQuad[4 * i + 1] = (byte)i;
                    ClRgbQuad[4 * i + 2] = (byte)i;
                    ClRgbQuad[4 * i + 3] = (byte)0;
                }
                nary_writer.Write(ClRgbQuad);
                for (int i = (int)(Height - 1); i > -1; i--)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        nary_writer.Write(ImData[i * Width + j]);
                    }
                    if (offset != 0)
                    {
                        for (int j = 0; j < offset; j++)
                        {
                            nary_writer.Write((byte)0);
                        }
                    }
                }

            }
            return true;
        }

        private void WriteAttrs(BinaryWriter nary_writer)
        {
            nary_writer.Write(FidSize);
            nary_writer.Write(FidReserved1);
            nary_writer.Write(FidReserved2);
            nary_writer.Write(FidOffBits);
            nary_writer.Write(Size);
            nary_writer.Write(Width);
            nary_writer.Write(-Height);
            nary_writer.Write(Planes);
            nary_writer.Write(BitCount);
            nary_writer.Write(Compression);
            nary_writer.Write(SizeImage);
            nary_writer.Write(XPelsPerMeter);
            nary_writer.Write(YPelsPerMeter);
            nary_writer.Write(ClrUsed);
            nary_writer.Write(ClrImportant);
        }
    }
}
