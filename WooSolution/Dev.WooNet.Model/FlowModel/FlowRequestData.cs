using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 获取模板传递参数
    /// </summary>
    public class GetTempFlowRequestData
    {
        /// <summary>
        /// 审批事项
        /// </summary>
        public int FlowItem { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 审批对象
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 对象类别ID
        /// </summary>
        public int ObjCateId { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }

    }
}
