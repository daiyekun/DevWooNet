﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 提交审批意见
    /// </summary>
   public class SubmitOptionInfo
    {
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
        /// <summary>
        /// 审批对象类别（客户，供应商、合同）
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 对象金额
        /// </summary>
        public decimal? ObjMoney { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 审批对象意见
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 审批对象结果1:同意，2:不同意
        /// </summary>
        public int OptRes { get; set; }
        /// <summary>
        /// 提交意见人ID
        /// </summary>
        public int SubmitUserId { get; set; }
    }
}
