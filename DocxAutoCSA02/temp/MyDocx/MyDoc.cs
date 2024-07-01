/**-----------------------------------------------------------------------------
 * File     : MyDoc.cs
 * Time     : 2022/2/4 13:50:12
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [��֯]
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
        /// �ĵ��������б�
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
        /// ������С��1.5�о�
        /// </summary>
        /// <param name="p"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ParaOne Adr01(ParaOne p, string text)
        {
            Text2Fmt text2Fmt = new Text2Fmt(text);
            text2Fmt.Size = 12.0f; // �ֺ�С��
            p.Text2Fmts.Add(text2Fmt);

            p.CharacterUnitFirstLineIndent = 2; // �����������ַ�
            p.LineSpacing = -15; // 1.5���о�

            return p;
        }

        /// <summary>
        /// ��ӱ���
        /// </summary>
        /// <param name="p"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ParaOne AdHeading(ParaOne p, string text, int nheading)
        {
            if (nheading==1)
            {
                //  һ��: ���ź�����У��м�������ո�1.5���м�࣬��ǰ��0.5�оࡣ
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 16.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5���о�
                p.SpaceBefore = 8; // 0.5���о�
                p.SpaceAfter = 8; // 0.5���о�
                p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter; // ���ж���
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel1; // һ���ı�
            }
            else if(nheading == 2)
            {
                //  ����: �������һ��д���⣬�ĺź��壬1.5���м�ࡣ
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 14.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5���о�
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel2; // �����ı�
            }
            else if(nheading >= 3 & nheading <= 9)
            {
                //  ����: �������һ��д���⣬С�ĺź��壬1.5���м�ࡣ
                Text2Fmt text2Fmt = new Text2Fmt(text);
                text2Fmt.Size = 12.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LineSpacing = -15; // 1.5���о�
                p.OutlineLevel = (Wd.WdOutlineLevel)nheading; // �����ı�
            }
            else
            {
                throw new Exception("nheading must last 10 not get 10\n");
            }
            return p;
        }

        public void Fit()
        {
            // ����ȱʡֵ������ʹ�õ���COM�⣬�������������Ҫ��Missing.Value����
            object N = Missing.Value;

            // ����Ѵ��ڣ���ɾ��
            if (File.Exists((string)DocFileName))
            {
                File.Delete((string)DocFileName);
            }

            // WordӦ�ó������
            WordApp = new Wd.ApplicationClass();
            // Word�ĵ�����
            WordDoc = WordApp.Documents.Add(ref N, ref N, ref N, ref N);
            WordApp.Visible = true;

            for (int i = 0; i < BodyArrayList.Count; i++)
            {
                
                if (BodyArrayList[i] is ParaOne) // ������ֶ���
                {
                    AddTextP((ParaOne)BodyArrayList[i]);
                }
            }

            // WdSaveFormatΪWord 2003�ĵ��ı����ʽ��office 2007����wdFormatDocumentDefault
            object format = Wd.WdSaveFormat.wdFormatDocument;
            // ��wordDoc�ĵ���������ݱ���ΪDOCX�ĵ�
            WordDoc.SaveAs(ref DocFileName, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N);
            // �ر�wordDoc�ĵ����󣬿��ǲ���Ҫ��ӡ wordDoc.PrintOut();
            WordDoc.Close(ref N, ref N, ref N);
            // �ر�wordApp�������
            WordApp.Quit(ref N, ref N, ref N);
            
        }

        private void AddTextP(ParaOne bodyone)
        {
            object unite = Wd.WdUnits.wdStory;
            WordApp.Selection.EndKey(ref unite, ref N); // ������Ƶ��ı�ĩβ
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
