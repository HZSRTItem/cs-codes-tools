/**-----------------------------------------------------------------------------
 * File     : MyDoc.cs
 * Time     : 2022/2/4 13:50:12
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [组织]
 **---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Wd = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;

namespace MyDocLib
{
    public class MyDoc
    {
        /// <summary>
        /// 文档的主体列表
        /// </summary>
        public ArrayList BodyArrayList = new ArrayList();

        public object DocFileName = @"";
        public Wd.Application WordApp = null;
        public Wd.Document WordDoc = null;
        private object N = Missing.Value;


        public MyDoc(string docfilename)
        {
            DocFileName = docfilename;
        }

        /// <summary>
        /// 首缩宋小四1.5行距
        /// </summary>
        /// <param name="p"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ParaOne Adr01(ParaOne p, string text)
        {
            Text2Fmt text2Fmt = new Text2Fmt(text);
            text2Fmt.Size = 12.0f; // 字号小四
            p.Text2Fmts.Add(text2Fmt);

            p.CharacterUnitFirstLineIndent = 2; // 首行缩进两字符
            p.LineSpacing = -15; // 1.5倍行距

            return p;
        }

        /// <summary>
        /// 添加标题
        /// </summary>
        /// <param name="p"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ParaOne AdHeading(ParaOne p, string text, int nheading)
        {
            if (nheading==1)
            {
                //  一级: 三号黑体居中，中间空两个空格，1.5倍行间距，段前后0.5行距。
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 16.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5倍行距
                p.SpaceBefore = 8; // 0.5倍行距
                p.SpaceAfter = 8; // 0.5倍行距
                p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter; // 居中对齐
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel1; // 一级文本
            }
            else if(nheading == 2)
            {
                //  二级: 序数后空一格写标题，四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 14.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5倍行距
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel2; // 二级文本
            }
            else if(nheading >= 3 & nheading <= 9)
            {
                //  三级: 序数后空一格写标题，小四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 12.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5倍行距
                p.OutlineLevel = (Wd.WdOutlineLevel)nheading; // 三级文本
            }
            else
            {
                throw new Exception("nheading must last 10 not get 10\n");
            }
            return p;
        }

        public void Fit()
        {
            // 函数缺省值，由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            object N = Missing.Value;

            // 如果已存在，则删除
            if (File.Exists((string)DocFileName))
            {
                File.Delete((string)DocFileName);
            }

            // Word应用程序变量
            WordApp = new Wd.ApplicationClass();
            // Word文档变量
            WordDoc = WordApp.Documents.Add(ref N, ref N, ref N, ref N);
            WordApp.Visible = true;

            for (int i = 0; i < BodyArrayList.Count; i++)
            {
                
                if (BodyArrayList[i] is ParaOne) // 添加文字段落
                {
                    AddTextP((ParaOne)BodyArrayList[i]);
                }
            }

            // WdSaveFormat为Word 2003文档的保存格式，office 2007就是wdFormatDocumentDefault
            object format = Wd.WdSaveFormat.wdFormatDocument;
            // 将wordDoc文档对象的内容保存为DOCX文档
            WordDoc.SaveAs(ref DocFileName, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N);
            // 关闭wordDoc文档对象，看是不是要打印 wordDoc.PrintOut();
            WordDoc.Close(ref N, ref N, ref N);
            // 关闭wordApp组件对象
            WordApp.Quit(ref N, ref N, ref N);
            
        }

        private void AddTextP(ParaOne bodyone)
        {
            object unite = Wd.WdUnits.wdStory;
            WordApp.Selection.EndKey(ref unite, ref N); // 将光标移到文本末尾
            WordDoc.Paragraphs.Last.Range.Text = "";
            Wd.Paragraph wdp = WordDoc.Paragraphs.Last;

            for (int i=0;i<bodyone.Text2Fmts.Count; i++)
            {
                Text2Fmt text2Fmt = bodyone.Text2Fmts[i];
                wdp.Range.Text = text2Fmt.Text;
                wdp.Range.Font.Bold = text2Fmt.Bold;
                wdp.Range.Font.Name = text2Fmt.FontName;
                wdp.Range.Font.Color = text2Fmt.Color;
                wdp.Range.Font.DoubleStrikeThrough = text2Fmt.DoubleStrikeThrough;
                wdp.Range.Font.Italic = text2Fmt.Italic;
                wdp.Range.Font.Size = text2Fmt.Size;
                wdp.Range.Font.Subscript = text2Fmt.Subscript;
                wdp.Range.Font.Superscript = text2Fmt.Superscript;
            }

            wdp.Format.FirstLineIndent = bodyone.FirstLineIndent;
            wdp.Format.Alignment = bodyone.Alignment;
            wdp.Format.CharacterUnitFirstLineIndent = bodyone.CharacterUnitFirstLineIndent;
            wdp.Format.CharacterUnitLeftIndent = bodyone.CharacterUnitLeftIndent;
            wdp.Format.CharacterUnitRightIndent = bodyone.CharacterUnitRightIndent;
            //wdp.Format.FirstLineIndent = bodyone.FirstLineIndent;
            wdp.Format.LeftIndent = bodyone.LeftIndent;
            wdp.Format.OutlineLevel = bodyone.OutlineLevel;
            wdp.Format.PageBreakBefore = bodyone.PageBreakBefore;
            wdp.Format.RightIndent = bodyone.RightIndent;
            wdp.Format.SpaceAfter = bodyone.SpaceAfter;
            wdp.Format.SpaceBefore = bodyone.SpaceBefore;
            
            if(bodyone.LineSpacing == -15)
            {
                wdp.Format.Space15();
            }
            else
            {
                wdp.Format.LineSpacing = bodyone.LineSpacing;
            }

            wdp.Range.InsertAfter("\n");
        }

    }
}
