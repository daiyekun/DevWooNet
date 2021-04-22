using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{
    /// <summary>
    /// 显示类
    /// </summary>
    public class DevUserinfoDTO: DevUserinfo, IModelDTO
    {
        /// <summary>
        /// 姓名描述
        /// </summary>
        public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDic { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

    }
}
