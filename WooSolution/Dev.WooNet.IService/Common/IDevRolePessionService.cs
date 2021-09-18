using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
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

        #region 合同对方权限
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同对方权限表达式树</returns>
        Expression<Func<DevCompany, bool>> GetCompanyListPermissionExpression(string funcCode, int userId, int deptId = 0);
        /// <summary>
        /// 判断当前用户是否有新建合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetCompanyAddPermission(string funcCode, int userId);
        /// <summary>
        //获取删除合同对方权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
         PermissionDataInfo GetCompanyDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 判断当前用户是否有修改合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetCompanyUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        /// <summary>
        /// 判断当前用户是否有修改合同对方次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetCompanySecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        /// <summary>
        /// 判断当前用户是否有查看合同对方详情的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        bool GetCompanyDetailPermission(string funcCode, int userId, int deptId, int detailObjId);
        #endregion


    }
}
