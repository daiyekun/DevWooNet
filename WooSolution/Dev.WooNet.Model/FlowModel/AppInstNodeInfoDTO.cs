using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.FlowModel
{

    /// <summary>
    /// 实例节点
    /// </summary>
    public class AppInstNodeInfoDTO: DevAppInstNodeInfo
    {
    }

    /// <summary>
    /// 实例节点查看dto
    /// </summary>
    public class AppInstNodeInfoViewDTO: AppInstNodeInfoDTO
    {
        /// <summary>
        /// 组用户名称
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public string StateDic { get; set; }
    }
}
