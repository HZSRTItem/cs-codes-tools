using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotogrammetryWFA
{
    /// <summary>
    /// 存储地面点信息
    /// </summary>
    class GroundPoint
    {
        /// <summary>
        /// 地面点点号
        /// </summary>
        public int PointNumber = 0;
        /// <summary>
        /// 地面点类型: 控制点 模型点 连接点 检查点
        /// </summary>
        public string Style = "像点";

        /// <summary>
        /// 模型点X坐标
        /// </summary>
        public double X_ = 0;
        /// <summary>
        /// 模型点Y坐标
        /// </summary>
        public double Y_ = 0;
        /// <summary>  
        /// 模型点H坐标 
        /// </summary> 
        public double H_ = 0;

        /// <summary>
        /// 地面点X坐标
        /// </summary>
        public double X = 0;
        /// <summary>
        /// 地面点Y坐标
        /// </summary>
        public double Y = 0;
        /// <summary>
        /// 地面点H坐标
        /// </summary>
        public double H = 0;


        
        

    }
}
