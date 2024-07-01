/*------------------------------------------------------------------------------
 * File    : ModTrainTest
 * Time    : 2023/1/26 14:49:35
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[ModTrainTest]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SVMGeoWFA
{
    class ModTrainTest
    {
        public string modelName = null;

        public SVMDataSet svmDS;
        public List<string> selectTrainColumnNames = new List<string>(10);
        public Dictionary<string, string> argsTrain = new Dictionary<string, string>(21);
        public List<string> selectTestColumnNames = new List<string>(10);
        public Dictionary<string, string> argsTest = new Dictionary<string, string>(21);
        public string trainCateColumnName = null;

        public string modelDir = null;
        public string modelFileName = null;
        public string trainDataFileName = null;
        public string testDataFileName = null;
        public string trainResFileName = null;
        public string testResFileName = null;

        public string imageFileName = null;

        public string trainLine = null;

        public ModTrainTest(string name)
        {
            modelName = name;
        }

        public bool setDataSet(SVMDataSet ds)
        {
            svmDS = ds;
            return true;
        }

        public bool addTrainColumnName(string name)
        {
            selectTrainColumnNames.Add(name);
            return true;
        }

        public bool clearTrainColumnNames()
        {
            selectTrainColumnNames.Clear();
            return true;
        }

        public bool addTrainArg(string mark, string info)
        {
            if (argsTrain.Keys.Contains(mark))
            {
                argsTrain[mark] = info;
            }
            else
            {
                argsTrain.Add(mark, info);
            }
            return true;
        }

        public bool train()
        {
            // 构建数据集
            StreamWriter sw = new StreamWriter(trainDataFileName);
            for (int i = 0; i < svmDS.DT.Rows.Count; i++)
            {
                sw.Write(svmDS.DT.Rows[i][trainCateColumnName].ToString());
                for (int j = 0; j < selectTrainColumnNames.Count; j++)
                {
                    sw.Write(" ");
                    sw.Write((j + 1).ToString());
                    sw.Write(":");
                    sw.Write(svmDS.DT.Rows[i][selectTrainColumnNames[j]].ToString());
                }
                sw.Write("\n");
            }
            sw.Close();
            // 构建训练参数
            trainLine = "svm-train.exe";
            foreach (KeyValuePair<string, string> item in argsTrain)
            {
                if (item.Key == "-s" & item.Value != "0") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-t" & item.Value != "2") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-d" & item.Value != "3") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-g" & item.Value != "0") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-r" & item.Value != "0") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-c" & item.Value != "1") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-n" & item.Value != "0.5") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-p" & item.Value != "0.1") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-m" & item.Value != "100") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-e" & item.Value != "0.001") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-h" & item.Value != "1") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-b" & item.Value != "0") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-wi" & item.Value != "1") { trainLine += " " + item.Key + " " + item.Value; }
                else if (item.Key == "-v" & item.Value != "-1") { trainLine += " " + item.Key + " " + item.Value; }
            }
            trainLine += " " + trainDataFileName;
            trainLine += " " + modelFileName;
            trainLine += "\nsvm-predict.exe " + trainDataFileName + " " + modelFileName + " " + trainResFileName;
            return true;
        }
    }

    class ModTrainTestList
    {
        List<ModTrainTest> list = new List<ModTrainTest>(3);

        public ModTrainTest this[int i]
        {
            get { return list[i]; }
        }

        public ModTrainTest this[string name]
        {
            get { return getByName(name); }
        }

        public ModTrainTest getByName(string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (name == list[i].modelName)
                {
                    return list[i];
                }
            }
            return null;
        }

        public bool IsModIn(string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (name == list[i].modelName)
                {
                    return true;
                }
            }
            return false;
        }

        public ModTrainTest addOne(string name, string modelDir)
        {
            if (IsModIn(name))
            {
                return null;
            }
            modelDir = Path.Combine(modelDir, name);
            if (!Directory.Exists(modelDir))
            {
                Directory.CreateDirectory(modelDir);
            }

            ModTrainTest mt = new ModTrainTest(name)
            {
                modelDir = modelDir,
                modelFileName = Path.Combine(modelDir, name + ".model"),
                trainDataFileName = Path.Combine(modelDir, "traindata_" + name + ".txt"),
                testDataFileName = Path.Combine(modelDir, "testdata_" + name + ".txt"),
                trainResFileName = Path.Combine(modelDir, "trainres_" + name + ".txt"),
                testResFileName = Path.Combine(modelDir, "testres_" + name + ".txt"),
            };

            list.Add(mt);

            return mt;
        }
    }
}
