# _*_ coding:utf-8 _*_
r"""----------------------------------------------------------------------------
@File    : tfr2npy.py
@Time    : 2022/05/11 15:41:48
@Author  : Zheng Han 
@Contact : hzsongrentou1580@gmail.com
@License : (C)Copyright 2022, ZhengHan. All rights reserved.
@Desc    : 

> Files
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\gba0511_spl21_train1.tfrecord
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\gba0511_spl21_train2.tfrecord
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\gba_spl_train50.tfrecord
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\gba_spl_train51.tfrecord
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\gba_spl_train52.tfrecord
> marks
lamd
system:index
> expdatas
B2	7,7
B3	7,7
B4	7,7
> clfile
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\cl_.txt
> dfile
D:\SpecialProjects\TfrecordRW\DcTfrWFA01\DcTfrWFA01\bin\Debug\net5.0-windows\d_.txt

-----------------------------------------------------------------------------"""

import tensorflow as tf
import numpy as np
import sys

def main():
    """ 
    sys 0: pythonfile
    sys 1: setfile
    """
    in_file = r"t01.txt"
    files, marks, expdatas, clfile, dfile = new_func(in_file)
    tfr_file_list = files
    feature_description = {}
    for line in marks:
        feature_description[line.strip()] = tf.io.FixedLenFeature(shape=[], dtype=tf.float32)
    bandnames = []
    for line in expdatas:
        lines = line.strip().split("\t")
        m, n = eval(lines[1].split(",")[0]), eval(lines[1].split(",")[1])
        bandnames.append(lines[0])
        feature_description[lines[0]] = tf.io.FixedLenFeature(shape=[1, m, n], dtype=tf.float32)
    
    tfr_file = tfr_file_list[0]
    ddict = ReadTfrAsTensor(tfr_file, feature_description)
    spls_a = tf.concat([tf.concat([item[k] for k in bandnames], 1) for item in ddict], 0)
    print("samples shape: ", spls_a.shape)
    marks_a = tf.concat([[tf.concat([item[k] for item in ddict], 0)] for k in marks], 0)
    print("marks shape: ", marks_a.shape)


    for tfr_file in tfr_file_list[1:]:
        ddict = ReadTfrAsTensor(tfr_file, feature_description)
        spls = tf.concat([tf.concat([item[k] for k in bandnames], 1) for item in ddict], 0)
        spls_a = tf.concat([spls_a, spls], axis=0)
        print("samples shape: ", spls.shape)
        marks_0 = tf.concat([[tf.concat([item[k] for item in ddict], 0)] for k in marks], 0)
        marks_a = tf.concat([marks_a, marks_0], axis=1)
        print("marks shape: ", marks_0.shape)

    c_m, c_n = int(spls_a.shape[-2]/2), int(spls_a.shape[-1]/2)
    coor_center = tf.transpose(spls_a[:, :, c_m, c_n])
    marks_a = tf.concat([marks_a, coor_center], axis=0)
    print(spls_a.shape)
    print(marks_a.shape)

    np.save(dfile[0], spls_a.numpy())
    np.savetxt(clfile[0], marks_a.numpy().T, fmt="%.8f", delimiter=",")
    pass


def new_func(in_file):
    with open(in_file, "r", encoding="utf-8") as f:
        lines = f.readlines()
        i = 0
        while True:
            if(i == len(lines)):
                break
            if(lines[i].strip() == "> Files"):
                break
            i += 1
        files = []
        while True:
            if(lines[i].strip() == ""):
                i += 1
                continue
            if(lines[i][0] == " "):
                files.append(lines[i].strip())
            if(lines[i].strip() == "> marks"):
                break
            i += 1
        marks = []
        while True:
            if(i == len(lines)):
                break
            if(lines[i].strip() == ""):
                i += 1
                continue
            if(lines[i][0] == " "):
                marks.append(lines[i].strip())
            if(lines[i].strip() == "> expdatas"):
                break
            i += 1
        expdatas = []
        while True:
            if(i == len(lines)):
                break
            if(lines[i].strip() == ""):
                i += 1
                continue
            if(lines[i][0] == " "):
                expdatas.append(lines[i].strip())
            if(lines[i].strip() == "> clfile"):
                break
            i += 1
        clfile = []
        while True:
            if(i == len(lines)):
                break
            if(lines[i].strip() == ""):
                i += 1
                continue
            if(lines[i][0] == " "):
                clfile.append(lines[i].strip())
            if(lines[i].strip() == "> dfile"):
                break
            i += 1
        dfile = []
        while True:
            if(i == len(lines)):
                break
            if(lines[i].strip() == ""):
                i += 1
                continue
            if(lines[i][0] == " "):
                dfile.append(lines[i].strip())
            if(i == len(lines)):
                break
            i += 1
    return files, marks, expdatas, clfile, dfile


def ReadTfrAsTensor( tfr_file: str, feature_des: dict, batch_size=128):
    """ TfRecord转为Tensor

    :param tfr_file: TfRecord文件
    :param featdes: 特征描述
    :return: 解析的数据
    """
    reader = tf.data.TFRecordDataset(tfr_file)  # 打开样本

    def _parse_function(exam_proto):
        return tf.io.parse_single_example(exam_proto, feature_des)

    reader = reader.repeat(1)  # 读取数据的重复次数为 ：1次，这个相当于epoch
    # reader = reader.shuffle(buffer_size=2000)  # 在缓冲区中随机打乱数据
    reader = reader.map(_parse_function)  # 解析数据
    # 每10条数据为一个batch，生成一个新的Dataset
    batchs = reader.batch(batch_size=batch_size)
    items = [item for item in batchs]
    return items


if __name__ == "__main__":
    main()
