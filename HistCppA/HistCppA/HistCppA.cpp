/*------------------------------------------------------------------------------
 * File    : HistCppA.cpp 
 * Time    : 2022-4-22 20:29:15
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
------------------------------------------------------------------------------*/

#include <iostream>
#include <fstream>
using namespace std;

int main(int argc, char* argv[])
{
    if (argc > 1)
    {
        int n_hist = 10;
        ifstream ifs(argv[1]);
        if (ifs.is_open())
        {
            if (argc > 2)
            {
                try 
                {
                    n_hist = atoi(argv[2]);
                }
                catch (exception e)
                {
                    cout << "Error: the second parameter should be the number of boxes\n    " << e.what() << endl;
                }
            }
            if (n_hist < 2)
            {
                cout << "Error: the number of boxes have to great then 2 -- input n hist = " << n_hist << endl;
            }
            else
            {
                
                try
                {
                    double* p = new double[n_hist + 2]{ 0 };
                    double x = 0;
                    ifs >> x;
                    int n = 1;
                    double x_min = x;
                    double x_max = x;
                    int i;

                    while (ifs >> x)
                    {
                        x_min = x > x_min ? x_min : x;
                        x_max = x < x_max ? x_max : x;
                        n++;
                    }

                    cout << "Min: " << x_min << endl;
                    cout << "Max: " << x_max << endl;
                    cout << "Number of data: " << n << endl;
                    double len0 = (x_max - x_min) / n_hist;
                    double k = 1.0 / (x_max - x_min) * n_hist;
                    ifs.close();

                    ifstream ifs0(argv[1]);
                    while (ifs0 >> x)
                    {
                        i = (int)((x - x_min) * k);
                        p[i]++;
                    }
                    cout << endl;

                    cout << "number" << " ";
                    cout << "hist" << " ";
                    cout << "count" << " ";
                    cout << "percentage" << "\n";
                    for (i = 0; i < n_hist - 1; i++)
                    {
                        cout << i + 1 << " ";
                        cout << x_min + i * len0 + len0 / 2 << " ";
                        cout << p[i] << " ";
                        cout << p[i] / n << "\n";
                    }
                    cout << i + 1 << " ";
                    cout << x_min + i * len0 + len0 / 2 << " ";
                    cout << p[i] + p[i + 1] << " ";
                    cout << (p[i] + p[i + 1]) / n << "\n\n";
                    ifs0.close();
                    delete[] p;


                }
                catch (exception e)
                {
                    cout << "Error: " << e.what() << endl;
                }
            }

        }
        else
        {
            ifs.close();
            cout << "Error: can not find file -- " << argv[0] << endl;
        }
        
    }
    
    cout << "srt_hist [data file] [/ number of boxs]" << endl;
    cout << "(C)Copyright 2022, ZhengHan. All rights reserved." << endl;

}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
