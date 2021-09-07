using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO.DevFlow
{

    /// <summary>
    /// 流程pdf生成信息
    /// </summary>
    public class DevFlowPdfBase
    {
        public Dictionary<string, List<WfOption>> DicWfData = new Dictionary<string, List<WfOption>>();
    }

    /// <summary>
    /// 审批意见
    /// </summary>
    public class WfOption
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeStrId { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int? NodeId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string AppUserName { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AppDate { get; set; }

        public int AddUserId { get; set; }

        public string UserEs { get; set; }
        public string UserEsTy { get; set; }

        public string ImgSrc { get; set; }
    }
}
