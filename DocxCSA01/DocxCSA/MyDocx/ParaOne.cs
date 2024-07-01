/**-----------------------------------------------------------------------------
 * File     : ParaOne.cs
 * Time     : 2022/2/4 12:26:28
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [һ������]
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
    /// һ������
    /// </summary>
    public class ParaOne
    {
        /// <summary>
        /// ÿ������
        /// </summary>
        public List<Text2Fmt> Text2Fmts = new List<Text2Fmt>();


        /// <summary>
        /// ���ػ�����һ�� WdParagraphAlignment �������ó�������ָ������Ķ��뷽ʽ��
        /// Ĭ�ϣ����˶���
        /// Center ����
        /// Distribute ����
        /// Justify ֤������
        /// JustifyHi ֤��
        /// JustifyLow �绤
        /// JustifyMed ������
        /// Left ���
        /// Right ����
        /// ThaiJustify ̩��˹��
        /// </summary>
        public Wd.WdParagraphAlignment Alignment = Wd.WdParagraphAlignment.wdAlignParagraphJustify;

        /// <summary>
        /// ���ػ��������л�����������ֵ (���ַ�Ϊ��λ)��
        /// </summary>
        public float CharacterUnitFirstLineIndent = 0;

        /// <summary>
        /// ���ػ�����ָ�������������ֵ (���ַ�Ϊ��λ)��
        /// </summary>
        public float CharacterUnitLeftIndent = 0;

        /// <summary>
        /// �����Է��ػ�����ָ��������������������ַ�Ϊ��λ����
        /// </summary>
        public float CharacterUnitRightIndent = 0;

        /// <summary>
        /// ���ػ��������е��л�����������ֵ (�԰�Ϊ��λ)��
        /// </summary>
        public float FirstLineIndent = 0;

        /// <summary>
        /// ���ػ�����һ��Single���͵�ֵ����ֵ����ָ�����䡢����л� HTML ���ֵ�������ֵ���԰�Ϊ��
        /// λ����
        /// </summary>
        public float LeftIndent = 0;

        /// <summary>
        /// ���ػ�����ָ��������о� (�԰�Ϊ��λ)��
        /// </summary>
        public float LineSpacing = -15;

        /// <summary>
        /// ���ػ�����ָ������Ĵ�ټ���
        /// Ĭ�ϣ�û�д�ټ���
        /// </summary>
        public Wd.WdOutlineLevel OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevelBodyText;

        /// <summary>
        /// ȷ����ָ������ǰ�Ƿ�ǿ�Ʒ�ҳ��
        /// </summary>
        public int PageBreakBefore = 0;

        /// <summary>
        /// ���ػ�����ָ������������������԰�Ϊ��λ����
        /// </summary>
        public float RightIndent = 0;

        /// <summary>
        /// ���ػ�����ָ��������ı�������ļ�� (�԰�Ϊ��λ) ��������
        /// </summary>
        public float SpaceAfter = 0;

        /// <summary>
        /// ���ػ�����ָ������Ķ�ǰ��� (�԰�Ϊ��λ)��
        /// </summary>
        public float SpaceBefore = 0;



    }
}
