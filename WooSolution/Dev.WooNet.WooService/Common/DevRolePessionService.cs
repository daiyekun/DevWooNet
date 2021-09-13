using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 角色权限
    /// </summary>
    public partial class DevRolePessionService
    {

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="rolePermissions">角色权限集合</param>
        /// <returns></returns>
        public IEnumerable<DevRolePession> SavePermission(IEnumerable<DevRolePession> rolePermissions)
        {
            var firstinfo = rolePermissions.FirstOrDefault();

            string sqlstr = "delete from  dev_role_pession where MId=" + firstinfo.Mid + " and  RoleId=" + firstinfo.RoleId;
            ExecuteSqlCommand(sqlstr);
            return Add(rolePermissions);
        }
    }
}
