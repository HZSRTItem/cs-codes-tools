# _*_ coding:utf-8 _*_
r"""----------------------------------------------------------------------------
@File    : pdf2image.py
@Time    : 2022/09/12 20:31:44
@Author  : Zheng Han 
@Contact : hzsongrentou1580@gmail.com
@License : (C)Copyright 2022, ZhengHan. All rights reserved.
@Desc    : 
-----------------------------------------------------------------------------"""
import os
import fitz  
import sys

def pyMuPDF_fitz(pdfPath, imagePath, qianzui):
    pdfDoc = fitz.open(pdfPath)
    for pg in range(pdfDoc.page_count):
        page = pdfDoc[pg]
        rotate = int(0)
        zoom_x = 1.33333333 
        zoom_y = 1.33333333
        mat = fitz.Matrix(zoom_x, zoom_y).prerotate(rotate)
        pix:fitz.Pixmap = page.get_pixmap(matrix=mat, alpha=False)
        pix.save(os.path.join(imagePath, f'{qianzui}_{pg+1}.png')) 

if __name__ == "__main__":
    pdfPath = sys.argv[1]
    imagePath = sys.argv[2]
    qianzui = sys.argv[3]
    pyMuPDF_fitz(pdfPath, imagePath, qianzui)
