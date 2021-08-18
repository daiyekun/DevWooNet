using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO.DevFlow
{

    /// <summary>
    /// 保存流程图时请求对象
    /// </summary>
    public class ReqSaveNodeInfo
    {
        /// <summary>
        /// 流程图界面json字符串
        /// </summary>
        public string FlowNodeData { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
    }
}
