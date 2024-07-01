# 写word的快速排版工具

完成MarkDown的功能，标题，段落，图片，表格，参考文献和相关的编号

# 标题

一级标题，二级标题，三级标题，四级标题，五级标题 ... 

## 标题编号

设置不同的记号

第一章 1.1 1.1.1 ...

1 1.1 1.1.1

## 标题字体，段落格式等

黑体，三号，居中等的

# 段落

段落的格式

两端对齐，字体，大小等一些事情

# 图片

路径，读取，大小，序号，中英文标题等

# 表格

表格格式啥的

# 参考文献

编号，格式等


# C# 技巧
using System.Collections; ArrayList 不定类型列表

## 段落格式

class Paragraph

class TextFmt \
class 

字体

段落缩进

## 



AllCaps	
如果字体格式为全部字母大写，则该属性值为 True。 返回 True、False 或 wdUndefined（当返回值既可为 True，也可为 False 时取该值）。 该属性可设置为 True、False 或 wdToggle（与当前设置相反）。 Integer 型，可读/写。

Animation	
此对象、成员或枚举已被弃用并且不适合在您的代码中使用。

Application	
返回一个 Application 代表 Microsoft Word 应用程序的对象。

Bold	
如此 如果将字体或区域的格式设置为加粗格式。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

BoldBi	
如此 如果将字体或区域的格式设置为加粗格式。 返回True、 False或wdUndefined （为粗体和非粗体文本的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Borders	
返回一个 Borders 集合，该集合代表指定对象的所有边框。

Color	
返回或设置指定对象的24位颜色 Font 。

ColorIndex	
返回或设置指定的 Border 或对象的颜色 Font 。

ColorIndexBi	
返回或设置 Font 从右到左语言的文档中指定对象的颜色。

ContextualAlternates	
指定对指定的字体启用上下文替代字。

Creator	
返回一个 32 位整数，它指示在其中创建指定的对象的应用程序。 只读 Integer。

DiacriticColor	
返回或设置用于指定对象的音调符号的24位颜色 Font 。 可以是任何有效 WdColor 常量，也可以是 Visual Basic 的RGB函数返回的值。 读/写。

DisableCharacterSpaceGrid	
如此如果 Microsoft Word 将忽略对应的对象每行的字符数 Font 。 读/写 Boolean。

DoubleStrikeThrough	
如此如果指定字体的格式设置为双删除线文本。 返回 True、False 或 wdUndefined（当返回值既可为 True，也可为 False 时取该值）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Duplicate	
返回一个只读 Font 对象，该对象代表指定字体的字符格式。

Emboss	
如此 如果将指定的字体的格式设置为阳文。 返回 真 、 假 或 wdUndefined 。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

EmphasisMark	
返回或设置字符或指定的字符字符串的着重号。

Engrave	
如果该字体的格式设置为阴文， 则返回 true 。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Fill	
获取一个 FillFormat 对象，该对象包含指定文本范围使用的字体的填充格式属性。

Glow	
获取一个 GlowFormat 对象，表示指定范围的文本使用的字体的发光格式。

Hidden	
如此 如果字体的格式设置为隐藏文字。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Italic	
如此 如果字体或区域的格式设置为倾斜格式。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

ItalicBi	
如此 如果字体或区域的格式设置为倾斜格式。 返回True、 False或wdUndefined （以斜体和非斜体文本的混合形式）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Kerning	
返回或设置 Microsoft Word 将调整自动字距调整的最小字号。 读/写 单个 。

Ligatures	
获取或设置指定Font对象的连字设置。

Line	
获取一个 LineFormat 对象，该对象指定行的格式。

Name	
返回或设置指定对象的名称。 可读/写 String 类型。

NameAscii	
返回或设置用于西文文本 (字符代码为 0 (零) 到 127 个字符) 的字体。 读/写 String。

NameBi	
返回或设置从右到左语言的文档中的字体的名称。 读/写， 字符串 。

NameFarEast	
返回或设置一种东亚字体名称。 读/写 String。

NameOther	
返回或设置用于从 128 到 255 的字符代码的字符的字体。 读/写 String。

NumberForm	
返回或设置 OpenType 字体的数字形式设置。

NumberSpacing	
获取或设置字体的数字间距设置。

Outline	
如果字体格式为镂空，则该属性值为 True。 返回true、 false或wdUndefined （true 和 False 的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Parent	
返回一个对象，代表指定对象的父对象。

Position	
返回或设置 (以磅为单位) 的文本相对于基准线的位置。 正值将文本提升，负值将文本降低。 Integer 型，可读/写。

Reflection	
获取一个 ReflectionFormat 对象，该对象代表形状的反射格式。

Scaling	
返回或设置应用于字体的缩放百分比。 本属性以当前字体大小的百分比水平拉长或压缩文字（缩放范围从 1 到 600）。 Integer 型，可读/写。

Shading	
返回一个 Shading 对象，该对象代表指定对象的底纹格式。

Shadow	
如果将指定字体设置为阴影格式，则该属性值为 True。 可 真 、 假 或 wdUndefined 。 Integer 型，可读/写。

Size	
返回或设置字体大小，以磅为单位。 读/写 单个 。

SizeBi	
返回或设置字体大小，以磅为单位。 读取/写入单。

SmallCaps	
如果字体格式为小型大写字母，则该属性值为 True。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Spacing	
返回或设置字符之间的间距 (以磅为单位)。 读/写 单个 。

StrikeThrough	
如此 如果字体的格式设置为带删除线的文本。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

StylisticSet	
获取或设置指定字体的样式集。

Subscript	
如此 如果字体的格式设置为下标。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 Integer 型，可读/写。

Superscript	
如此 如果字体格式为上标。 返回true、 false或wdUndefined （ true和False的混合）。 可以设置为 真 、 假 或 wdToggle 。 读/写 长 。

TextColor	
获取一个 ColorFormat 对象，该对象代表指定字体的颜色。

TextShadow	
获取一个 ShadowFormat 对象，该对象指定指定字体的阴影格式。

ThreeD	
获取一个 ThreeDFormat 对象，该对象包含指定字体的三维效果格式设置属性。

Underline	
返回或设置应用于字体的下划线类型。 读/写 WdUnderline 。

UnderlineColor	
返回或设置指定对象的下划线的24位颜色 Font 。