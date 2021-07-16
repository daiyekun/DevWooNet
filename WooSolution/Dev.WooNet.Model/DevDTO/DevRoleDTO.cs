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
    /// 校色
    /// </summary>
    public class DevRoleDTO:DevRole, IModelDTO
    {

    }
    /// <summary>
    /// 角色模块
    /// </summary>

    public class RoleModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModelIds { get; set; }

    }
}
