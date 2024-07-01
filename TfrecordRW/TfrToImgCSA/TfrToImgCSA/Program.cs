/*------------------------------------------------------------------------------
 * File    :
 * Time    : 2022-5-15 16:36:23
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of
 * ————————————————
版权声明：本文为CSDN博主「Heisenberg -」的原创文章，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/Mrsherlock_/article/details/123272889
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Tensorflow;
using System.Drawing;
using System.Drawing.Imaging;

namespace TfrToImgCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void Arr2Png(double[] d1, double[] d2, double[] d3, int width, int height, string out_file, double vmin, double vmax)
        {
            Bitmap bmp = new Bitmap(width, height);
            double dd = vmax - vmin;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int r = (int)((d1[i * height + j] - vmin) / dd);
                    color = Color.FromArgb(d1[i* height+j], pixel_bmp, pixel_bmp);  //<15ms
                    point_bmp.SetPixel(i, j, color);
                    //bmp.SetPixel(i, j, color);  //150-200ms
                    index++;
                }
            }
            point_bmp.UnlockBits();
        }


    }
}
