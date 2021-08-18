using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 流程所用json格式
    /// </summary>
    public  class FlowNodeData
    {
        /// <summary>
        /// 流程标题
        /// </summary>

        public string title { get; set; }
        /// <summary>
        /// 节点字典
        /// </summary>

        public Dictionary<string, FlowTempNodeViewDTO> nodes { get; set; }
        /// <summary>
        /// 节点连线
        /// </summary>

        public Dictionary<string, TempNodeLineViewDTO> lines { get; set; }
        /// <summary>
        /// 区域
        /// </summary>

        public Dictionary<string, TempNodeAreaViewDTO> areas { get; set; }
        /// <summary>
        /// 节点连线区域总个数
        /// </summary>

        public int initNum { get; set; }
    }
}
