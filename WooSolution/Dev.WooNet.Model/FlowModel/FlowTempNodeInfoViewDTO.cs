using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 节点信息
    /// </summary>
   public class FlowTempNodeInfoViewDTO: DevFlowTempNodeInfo
    {
        /// <summary>
        /// 组用户名称
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }
    }
}
