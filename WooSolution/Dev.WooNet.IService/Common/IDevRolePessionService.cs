using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{

    /// <summary>
    /// 角色权限
    /// </summary>
    public partial  interface IDevRolePessionService
    {
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="rolePermissions">角色权限集合</param>
        /// <returns></returns>
        IEnumerable<DevRolePession> SavePermission(IEnumerable<DevRolePession> rolePermissions);
        
        }
}
