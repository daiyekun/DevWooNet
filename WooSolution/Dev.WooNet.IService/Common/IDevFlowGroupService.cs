using Dev.WooNet.Common.Models;
using Dev.WooNet.Model;
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
    /// 审批组
    /// </summary>
    public partial interface IDevFlowGroupService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        AjaxListResult<DevFlowGroupDTO> GetList<s>(PageInfo<DevFlowGroup> pageInfo, Expression<Func<DevFlowGroup, bool>> whereLambda,
             Expression<Func<DevFlowGroup, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DevFlowGroupDTO GetInfoById(int Id);
        /// <summary>
        /// 保存组用户
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="GroupId">组ID</param>
        /// <returns></returns>
        int SaveGroupUser(int GroupId, string Ids);
    }
}
