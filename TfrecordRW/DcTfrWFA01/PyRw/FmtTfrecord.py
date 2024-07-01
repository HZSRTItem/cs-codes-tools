# _*_ coding:utf-8 _*_
r"""----------------------------------------------------------------------------
@File    : FmtTfrecord.py
@Time    : 2022/05/08 10:54:50
@Author  : Zheng Han
@Contact : hzsongrentou1580@gmail.com
@License : (C)Copyright 2022, ZhengHan. All rights reserved.
@Desc    : 格式化tfrecord文件
-----------------------------------------------------------------------------"""


import tensorflow as tf
import numpy as np


def LookTfRFeaInfo(tfr_file):
    """ 查看 tfrecord 的信息

    :param tfr_file: tfrecord file
    :return: 信息
    """
    with tf.compat.v1.Session() as sess:
        # 加载TFRecord数据
        ds = tf.data.TFRecordDataset(tfr_file)
        ds = ds.batch(1)
        iterator = tf.compat.v1.data.make_one_shot_iterator(ds)
        # 为了加快速度，仅仅简单拿一组数据看下结构
        batch_data = iterator.get_next()
        res = sess.run(batch_data)
        serialized_example = res[0]
        example_proto = tf.train.Example.FromString(serialized_example)
        features = example_proto.features
        feature_des = {}
        for i, key in enumerate(features.feature):
            feature = features.feature[key]
            feature_des[key] = []
            if len(feature.bytes_list.value) > 0:
                feature_des[key].append('bytes_list')
                feature_des[key].append(feature.bytes_list.value)
                feature_des[key].append("")
            elif len(feature.float_list.value) > 0:
                feature_des[key].append('float_list')
                feature_des[key].append(feature.float_list.value)
                feature_des[key].append("tf.io.FixedLenFeature(shape=[{1}], dtype=tf.float32)".format(
                    key, " " if len(feature.float_list.value) == 1 else len(feature.float_list.value)))
            elif len(feature.int64_list.value) > 0:
                feature_des[key].append('int64_list')
                feature_des[key].append(feature.int64_list.value)
                feature_des[key].append("tf.io.FixedLenFeature(shape=[{1}], dtype=tf.int64)".format(
                    key, " " if len(feature.int64_list.value) == 1 else len(feature.int64_list.value)))
            else:
                feature_des[key].append('not_know')
                feature_des[key].append([])
                feature_des[key].append("")
    feature_des = dict([(k,feature_des[k]) for k in sorted(feature_des.keys())])
    feature_des_names = feature_des.keys()
    feature_des_names_len = map(len, feature_des_names)
    n = max(feature_des_names_len)
    # print(feature_des)
    print('tfrecord info -f {0} :'.format(tfr_file))
    for i, ikey in enumerate(feature_des):
        fvalue= feature_des[ikey][1]
        if (len(fvalue) > 6):
            outs = "[{0:.6f}, {1:.6f}, {2:.6f}, ..., {3:.6f}, {4:.6f}, {5:.6f}]".format(
                fvalue[0], fvalue[1], fvalue[2],
                fvalue[-3], fvalue[-2], fvalue[-1],
            )
        else:
            outs = fvalue
        print("    {0:<3d} {1:<{2}}: {3:<10} {4} {5}".format(i+1, ikey,n+1, feature_des[ikey][0], len(feature_des[ikey][1]), outs))
    print("\nfeature_des = {")
    for ikey in feature_des:
        if(len(feature_des[ikey][1]) == 1):
            print("   ", ikey, ":", feature_des[ikey][2])
    for ikey in feature_des:
        if(len(feature_des[ikey][1]) != 1):
            print("   ", ikey, ":", feature_des[ikey][2])
    
    print("}")
    print("feature_names = ", feature_des.keys())

    # "\"{0}\": tf.io.FixedLenFeature(shape=[{1}], dtype=tf.float32)",
    # \"{0}\": tf.io.FixedLenFeature(shape=[{1}], dtype=tf.int64),


def ReadTfrAsTensor(cls, tfr_file: str, feature_des: dict, batch_size=128):
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


def GetTestJieYi():
    tfr_file_list = []
    feature_description = {
        'B2': tf.io.FixedLenFeature(shape=[1, 100, 100], dtype=tf.float32),
        'B3': tf.io.FixedLenFeature(shape=[1, 100, 100], dtype=tf.float32),
        'B4': tf.io.FixedLenFeature(shape=[1, 100, 100], dtype=tf.float32),
        'Xc': tf.io.FixedLenFeature(shape=[], dtype=tf.float32),
        'Yc': tf.io.FixedLenFeature(shape=[], dtype=tf.float32),
        'claes': tf.io.FixedLenFeature(shape=[], dtype=tf.float32)
    }
    # "D:\GraduationProject\Framework\1Sample\TestSample\JieYi\Data\gba_spl04.tfrecord\gba_spl04.tfrecord"
    tfr_file = tfr_file_list[0]
    ddict = Tf2Utils.ReadTfrAsTensor(tfr_file, feature_description)
    bandnames = ["B2", "B3", "B4"]
    spls_a = tf.concat([tf.concat([item[k] for k in bandnames], 1)
                       for item in ddict], 0)
    claes_a = tf.concat([item["claes"] for item in ddict], 0)
    Xc_a = tf.concat([item["Xc"] for item in ddict], 0)
    Yc_a = tf.concat([item["Yc"] for item in ddict], 0)
    for tfr_file in tfr_file_list[1:]:
        ddict = Tf2Utils.ReadTfrAsTensor(tfr_file, feature_description)
        bandnames = ["B2", "B3", "B4"]
        spls = tf.concat([tf.concat([item[k] for k in bandnames], 1)
                         for item in ddict], 0)
        spls_a = tf.concat([spls_a, spls], axis=0)
        print("samples shape: ", spls.shape)
        claes = tf.concat([item["claes"] for item in ddict], 0)
        claes_a = tf.concat([claes_a, claes], axis=0)
        print("claes shape: ", claes.shape)
        Xc = tf.concat([item["Xc"] for item in ddict], 0)
        Xc_a = tf.concat([Xc_a, Xc], axis=0)
        print("Xc shape: ", Xc.shape)
        Yc = tf.concat([item["Yc"] for item in ddict], 0)
        Yc_a = tf.concat([Yc_a, Yc], axis=0)
        print("Yc shape: ", Yc.shape)
    print(spls_a.shape)
    geom = np.concatenate(
        [[claes_a.numpy()], [Xc_a.numpy()], [Yc_a.numpy()]]).T
    print(geom.shape)
    np.save(r"..\Data\spl_gba_test02.npy", spls_a.numpy())
    np.savetxt(r"..\Data\geom_gba_test02.txt", geom, fmt="%.8f", delimiter=",")


def main():
    LookTfRFeaInfo(r".\Data\gba_spl_train50.tfrecord")
    pass


if __name__ == "__main__":
    main()
