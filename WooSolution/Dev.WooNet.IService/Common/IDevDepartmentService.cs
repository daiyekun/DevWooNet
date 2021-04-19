using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.DevDTO;
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
    /// 部门
    /// </summary>
   public partial interface IDevDepartmentService
    {
        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        AjaxListResult<DevDepartmentDTO> GetList<s>(PageInfo<DevDepartment> pageInfo, Expression<Func<DevDepartment, bool>> whereLambda,
            Expression<Func<DevDepartment, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <returns></returns>
        IList<DevDepartmentDTO> GetAll();
        /// <summary>
        /// 设置用户Redis
        /// </summary>
        void SetRedisHash();
        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="deptInfo">部门主要信息</param>
        /// <param name="deptMain">签约主体信息</param>
        /// <returns></returns>
        DevDepartment SaveDeptInfo(DevDepartment deptInfo, DevDeptmain deptMain);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        DevDepartmentDTO ShowValues(int Id);
        /// <summary>
        /// 树形菜单选择
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetTreeSelect();
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteDept(string ids);
    }
}
