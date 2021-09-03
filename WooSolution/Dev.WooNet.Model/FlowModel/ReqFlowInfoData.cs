using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 获取对象流程信息
    /// </summary>
    public class ReqFlowInfoData
    {
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int AppObjId { get; set; }
        /// <summary>
        /// 当前登录人员ID
        /// </summary>
        public int CurrUserId { get; set; } = 0;
        /// <summary>
        /// 审批对象类型
        /// 0：客户
        /// ....
        /// </summary>
        public int ObjType { get; set; }


    }
    /// <summary>
    /// 审批实例信息
    /// </summary>
    public class AppFlowInceInfo
    {
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; } = 0;
        /// <summary>
        /// 审批权限
        /// 1:当前登录人有审批权限
        /// </summary>
        public int AppAuth { get; set; } = 0;
        /// <summary>
        /// 审批节点ID
        /// </summary>
        public string NodeStrId { get; set; }
        /// <summary>
        /// 节点intId
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 审批节点名称
        /// </summary>
        public string NodeName { get; set; }
    }

}
