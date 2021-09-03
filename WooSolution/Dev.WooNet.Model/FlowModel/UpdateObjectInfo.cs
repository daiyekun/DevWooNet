﻿using Dev.WooNet.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 修改对象
    /// </summary>
   public class UpdateObjectInfo
    {
        /// <summary>
        /// 审批对象枚举
        /// </summary>
        public FlowObjEnums ObjType { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 流程状态0：默认，1：审批中,2审批通过
        /// </summary>
        public int WfState { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int WfItem { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
    }
}
