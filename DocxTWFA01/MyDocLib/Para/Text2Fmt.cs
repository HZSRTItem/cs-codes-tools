/**-----------------------------------------------------------------------------
 * File     : Text2Fmt.cs
 * Time     : 2022/2/4 12:27:49
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : [1] Font 接口
 *              https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.office.interop.word._font?view=word-pia
 * Desc     : Text2Fmt [一段文字及其格式]
 * 
 *      ///// <summary>
        ///// 返回一个 Shading 对象，该对象代表指定对象的底纹格式。
        ///// </summary>
        //public int Shading;

        ///// <summary>
        ///// 如果将指定字体设置为阴影格式，则该属性值为 True。 可 真 、 假 或 wdUndefined 。 Intege
        ///// r 型，可读/写。
        ///// </summary>
        //public int Shadow;
 **---------------------------------------------------------------------------*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;

namespace MyDocLib
{
    /// <summary>
    /// 一段文字及其格式
    /// </summary>
    public class Text2Fmt
    {
        /// <summary>
        /// 一段文字及其格式
        /// </summary>
        /// <param name="text">文字</param>
        public Text2Fmt(string text)
        {
            foreach (char item in text)
            {
                if (item != '\n')
                {
                    Text += item;
                }
            }
        }
        /// <summary>
        /// 文本
        /// </summary>
        public string Text = "";

        /// <summary>
        /// 字体
        /// </summary>
        public string FontName = "宋体";

        /// <summary>
        /// 如此 如果将字体或区域的格式设置为加粗格式。 返回true、 false或wdUndefined （ true和Fa
        /// lse的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。
        /// </summary>
        public int Bold = 0;

        /// <summary>
        /// 返回或设置指定对象的24位颜色 Font 。
        /// </summary>
        public Wd.WdColor Color = Wd.WdColor.wdColorBlack;

        /// <summary>
        /// 如此如果指定字体的格式设置为双删除线文本。 返回 True、False 或 wdUndefined（当返回值既
        /// 可为 True，也可为 False 时取该值）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读
        /// /写。
        /// </summary>
        public int DoubleStrikeThrough = 0;

        /// <summary>
        /// 如此 如果字体或区域的格式设置为倾斜格式。 返回true、 false或wdUndefined （ true和Fals
        /// e的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。
        /// </summary>
        public int Italic = 0;

        /// <summary>
        /// 返回或设置字体大小，以磅为单位。 读/写 单个 。
        /// </summary>
        public float Size = 10.5f;

        /// <summary>
        /// 如此 如果字体的格式设置为下标。 返回true、 false或wdUndefined （ true和False的混合）。
        /// 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。
        /// </summary>
        public int Subscript = 0;

        /// <summary>
        /// 如此 如果字体格式为上标。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置
        /// 为 真 、 假 或 wdToggle 。 读/写 长 。
        /// </summary>
        public int Superscript = 0;


    }
}
