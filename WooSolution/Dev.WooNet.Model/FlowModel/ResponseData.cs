using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 获取模板返回数据
    /// </summary>
    public class ResponTempData
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 模板历史
        /// </summary>
        public int TempHistId { get; set; }
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
    }
}
