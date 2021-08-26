using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 流程实例对象
    /// </summary>
    public class DevAppInstDTO: DevAppInst
    {
    }

    /// <summary>
    /// 提交审批实例对象
    /// </summary>
    public class SubDevAppInst
    {
        public int Id { get; set; } = 0;
        /// <summary>
        /// 审批金额
        /// </summary>
        public decimal AppObjAmount { get; set; }
        /// <summary>
        /// 审批类别ID
        /// </summary>
        public int AppObjCateId { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int AppObjId { get; set; }
        /// <summary>
        /// 审批对象名称
        /// </summary>
        public string AppObjName { get; set; }
        /// <summary>
        /// 审批对象编号
        /// </summary>
        public string AppObjNo { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int Mission { get; set; }
        /// <summary>
        /// 审批类型0：客户//1：供应商....
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 审批历史模板ID
        /// </summary>
        public int TempHistId { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }



    }
}
