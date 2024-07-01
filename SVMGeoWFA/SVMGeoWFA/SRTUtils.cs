/*------------------------------------------------------------------------------
 * File    : SRTUtils
 * Time    : 2023/1/25 16:49:09
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTUtils]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVMGeoWFA
{
    class SRTUtils
    {
        static Color[] colors = new Color[]
         {
            Color.Red, Color.Green, Color.Yellow, Color.Blue
         };

        public static Color GetColor(int n)
        {
            if (n < colors.Length)
            {
                return colors[n];
            }
            else
            {
                int R = new Random().Next(255);
                int G = new Random().Next(255);
                int B = new Random().Next(255);
                B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
                B = (B > 255) ? 255 : B;
                return Color.FromArgb(R, G, B);
            }
        }

        public static List<Color> cateColors = new List<Color>(9);

        public static Color getCateColor(int n)
        {
            if (n < cateColors.Count)
            {
                return cateColors[n];
            }
            else
            {
                cateColors.Add(GetColor(n));
                return cateColors[cateColors.Count - 1];
            }
        }


    }

    class ColumnCateorys
    {

        private class ColumnCateory
        {
            public List<int> FindIndex = new List<int>(20);
            public string CateName = "";
        }

        int indexall = 0;

        List<ColumnCateory> Cates = new List<ColumnCateory>(20);

        public void Add(string name, int i = -1)
        {
            int j = Cates.FindIndex(c => c.CateName.Equals(name));
            if (j != -1)
            {
                if (i == -1)
                {
                    Cates[j].FindIndex.Add(indexall++);
                }
                else
                {
                    Cates[j].FindIndex.Add(i);
                }
            }
            else
            {
                ColumnCateory columnCateory = new ColumnCateory()
                {
                    CateName = name
                };

                if (i == -1)
                {
                    columnCateory.FindIndex.Add(indexall++);
                }
                else
                {
                    columnCateory.FindIndex.Add(i);
                }

                Cates.Add(columnCateory);
            }
        }

        public int Count { get { return Cates.Count; } }

        public string GetNameByIndex(int i)
        {
            return Cates[i].CateName;
        }

        public int[] GetIndexByIndex(int i)
        {
            return Cates[i].FindIndex.ToArray();
        }
    }


}
