using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial interface IDevRoleService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        AjaxListResult<DevRoleDTO> GetList<s>(PageInfo<DevRole> pageInfo, Expression<Func<DevRole, bool>> whereLambda,
            Expression<Func<DevRole, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<DevRoleDTO> GetAll();
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">字典枚举</param>
        /// <returns></returns>
        void SetRedisHash();
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        DevRole SaveRole(DevRole roleinfo);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">需要删除的ID</param>
        /// <returns></returns>
        int DelRole(string Ids);
        /// <summary>
        /// 根据Id 查询用户信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        DevRoleDTO GetRoleById(int Id);
        /// <summary>
        /// 保存角色用户
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        int SaveRoleUser(int RoleId, string Ids);
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        int DeleteRoleUser(int RoleId, string Ids);


        }
}
