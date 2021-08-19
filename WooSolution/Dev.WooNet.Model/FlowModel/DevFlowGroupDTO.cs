using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 组
    /// </summary>
    public class DevFlowGroupDTO: DevFlowGroup, IModelDTO
    {
        public string AddUserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string GstateDic { get; set; }
        /// <summary>
        /// 组下用户
        /// </summary>
        public string UserNames { get; set; }

    }
}
