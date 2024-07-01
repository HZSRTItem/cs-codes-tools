/**-----------------------------------------------------------------------------
 * File     : ParaOne.cs
 * Time     : 2022/2/4 12:26:28
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [一个段落]
 **---------------------------------------------------------------------------*/


using System;
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
    /// 一个段落
    /// </summary>
    public class ParaOne
    {
        /// <summary>
        /// 每个部分
        /// </summary>
        public List<Text2Fmt> Text2Fmts = new List<Text2Fmt>();


        /// <summary>
        /// 返回或设置一个 WdParagraphAlignment 常量，该常量代表指定段落的对齐方式。
        /// 默认：两端对齐
        /// Center 居中
        /// Distribute 分配
        /// Justify 证明正当
        /// JustifyHi 证明
        /// JustifyLow 辩护
        /// JustifyMed 正当的
        /// Left 左边
        /// Right 正当
        /// ThaiJustify 泰胡斯蒂
        /// </summary>
        public Wd.WdParagraphAlignment Alignment = Wd.WdParagraphAlignment.wdAlignParagraphJustify;

        /// <summary>
        /// 返回或设置首行或悬挂缩进的值 (以字符为单位)。
        /// </summary>
        public float CharacterUnitFirstLineIndent = 0;

        /// <summary>
        /// 返回或设置指定段落的左缩进值 (以字符为单位)。
        /// </summary>
        public float CharacterUnitLeftIndent = 0;

        /// <summary>
        /// 该属性返回或设置指定段落的右缩进量（以字符为单位）。
        /// </summary>
        public float CharacterUnitRightIndent = 0;

        /// <summary>
        /// 返回或设置首行的行或悬挂缩进的值 (以磅为单位)。
        /// </summary>
        public float FirstLineIndent = 0;

        /// <summary>
        /// 返回或设置一个Single类型的值，该值代表指定段落、表格行或 HTML 划分的左缩进值（以磅为单
        /// 位）。
        /// </summary>
        public float LeftIndent = 0;

        /// <summary>
        /// 返回或设置指定段落的行距 (以磅为单位)。
        /// </summary>
        public float LineSpacing = -15;

        /// <summary>
        /// 返回或设置指定段落的大纲级别。
        /// 默认：没有大纲级别
        /// </summary>
        public Wd.WdOutlineLevel OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevelBodyText;

        /// <summary>
        /// 确定在指定段落前是否强制分页。
        /// </summary>
        public int PageBreakBefore = 0;

        /// <summary>
        /// 返回或设置指定段落的右缩进量（以磅为单位）。
        /// </summary>
        public float RightIndent = 0;

        /// <summary>
        /// 返回或设置指定段落或文本栏后面的间距 (以磅为单位) 的数量。
        /// </summary>
        public float SpaceAfter = 0;

        /// <summary>
        /// 返回或设置指定段落的段前间距 (以磅为单位)。
        /// </summary>
        public float SpaceBefore = 0;



    }
}
