using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 请求参数
    /// </summary>
    public class SubmitWfRequest
    {
        /// <summary>
        /// 流程模板
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 审批金额
        /// </summary>
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// 判断提交流程模板
    /// </summary>
    public class RequestCheckFlow
    {
        /// <summary>
        /// 流程模板ID
        /// </summary>
        public int? TempId { get; set; }
        /// <summary>
        /// 流程对象金额
        /// </summary>
        public decimal? Amount { get; set; }

        public int? FlowType { get; set; }
    }
}
