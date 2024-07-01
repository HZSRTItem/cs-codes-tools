/**-----------------------------------------------------------------------------
 * File     : Text2Fmt.cs
 * Time     : 2022/2/4 12:27:49
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : [1] Font �ӿ�
 *              https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.office.interop.word._font?view=word-pia
 * Desc     : Text2Fmt [һ�����ּ����ʽ]
 * 
 *      ///// <summary>
        ///// ����һ�� Shading ���󣬸ö������ָ������ĵ��Ƹ�ʽ��
        ///// </summary>
        //public int Shading;

        ///// <summary>
        ///// �����ָ����������Ϊ��Ӱ��ʽ���������ֵΪ True�� �� �� �� �� �� wdUndefined �� Intege
        ///// r �ͣ��ɶ�/д��
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
    /// һ�����ּ����ʽ
    /// </summary>
    public class Text2Fmt
    {
        /// <summary>
        /// һ�����ּ����ʽ
        /// </summary>
        /// <param name="text">����</param>
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
        /// �ı�
        /// </summary>
        public string Text = "";

        /// <summary>
        /// ����
        /// </summary>
        public string FontName = "����";

        /// <summary>
        /// ��� ��������������ĸ�ʽ����Ϊ�Ӵָ�ʽ�� ����true�� false��wdUndefined �� true��Fa
        /// lse�Ļ�ϣ��� ��������Ϊ �� �� �� �� wdToggle �� Integer �ͣ��ɶ�/д��
        /// </summary>
        public int Bold = 0;

        /// <summary>
        /// ���ػ�����ָ�������24λ��ɫ Font ��
        /// </summary>
        public Wd.WdColor Color = Wd.WdColor.wdColorBlack;

        /// <summary>
        /// ������ָ������ĸ�ʽ����Ϊ˫ɾ�����ı��� ���� True��False �� wdUndefined��������ֵ��
        /// ��Ϊ True��Ҳ��Ϊ False ʱȡ��ֵ���� ��������Ϊ �� �� �� �� wdToggle �� Integer �ͣ��ɶ�
        /// /д��
        /// </summary>
        public int DoubleStrikeThrough = 0;

        /// <summary>
        /// ��� ������������ĸ�ʽ����Ϊ��б��ʽ�� ����true�� false��wdUndefined �� true��Fals
        /// e�Ļ�ϣ��� ��������Ϊ �� �� �� �� wdToggle �� Integer �ͣ��ɶ�/д��
        /// </summary>
        public int Italic = 0;

        /// <summary>
        /// ���ػ����������С���԰�Ϊ��λ�� ��/д ���� ��
        /// </summary>
        public float Size = 10.5f;

        /// <summary>
        /// ��� �������ĸ�ʽ����Ϊ�±ꡣ ����true�� false��wdUndefined �� true��False�Ļ�ϣ���
        /// ��������Ϊ �� �� �� �� wdToggle �� Integer �ͣ��ɶ�/д��
        /// </summary>
        public int Subscript = 0;

        /// <summary>
        /// ��� ��������ʽΪ�ϱꡣ ����true�� false��wdUndefined �� true��False�Ļ�ϣ��� ��������
        /// Ϊ �� �� �� �� wdToggle �� ��/д �� ��
        /// </summary>
        public int Superscript = 0;


    }
}
