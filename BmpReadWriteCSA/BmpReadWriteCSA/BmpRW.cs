/*------------------------------------------------------------------------------
 * File    : BmpRW
 * Time    : 2022/8/4 7:53:36
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[BmpRW]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BmpReadWriteCSA
{
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
            Width = width;
            Height = height;
            Channels = 3;

            int step = Channels * Width;
            int offset = (int)(step % 4);
            step += offset == 4 ? 0 : 4 - offset;

            FidSize = (uint)(Height * step + 54);
            FidReserved1 = 0;
            FidReserved2 = 0;
            FidOffBits = 54;
            Size = 40;
            Width = width;
            Height = height;
            Planes = 1;
            BitCount = 24;
            Compression = 0;
            SizeImage = (uint)(Height * step);
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
            FileStream fs = new FileStream(save_file, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter nary_writer = new BinaryWriter(fs);
            nary_writer.Write('B');
            nary_writer.Write('M');
            int step = Channels * Width;
            int offset = (int)(step % 4);
            step += offset == 4 ? 0 : 4 - offset;
            WriteAttrs(nary_writer);
            for (int i = Height - 1; i > -1; i--)
            {
                for (int j = 0; j < Width; j++)
                {
                    nary_writer.Write(d[i * Width * 3 + j * 3]);
                    nary_writer.Write(d[i * Width * 3 + j * 3 + 1]);
                    nary_writer.Write(d[i * Width * 3 + j * 3 + 2]);
                }
                for (int j = 0; j < offset; j++)
                {
                    nary_writer.Write((byte)0);
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

            if (BitCount == 8)
            {
                // 非真彩色
                Channels = 1;
                int offset = (int)((Channels * Width) % 4);
                if (offset != 0)
                {
                    offset = 4 - offset;
                }
                ImData = new byte[Width * Height];
                int step = Channels * Width;
                ClRgbQuad = nary_reader.ReadBytes(256 * 4);
                // 数据读取
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        ImData[(Height - 1 - i) * step + j] = nary_reader.ReadByte();
                    }
                    nary_reader.ReadBytes(offset);
                }
            }
            else if (BitCount == 24)
            {
                // 真彩色
                Channels = 3;
                ImData = new byte[Width * Height * 3];
                int step = Channels * Width;
                int offset = (int)((Channels * Width) % 4);
                if (offset != 0)
                {
                    offset = 4 - offset;
                }
                uint offset1 = FidOffBits - 4 * 13;
                nary_reader.ReadBytes((int)offset1);
                // 数据读取
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            ImData[(Height - 1 - i) * step + j * 3 + k] = nary_reader.ReadByte();
                        }
                    }
                    nary_reader.ReadBytes(offset);
                }
            }
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
            nary_writer.Write(Height);
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
