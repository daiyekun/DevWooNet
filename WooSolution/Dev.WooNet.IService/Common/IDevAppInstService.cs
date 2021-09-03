using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.FlowModel;
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
    /// 审批实例
    /// </summary>
    public partial interface IDevAppInstService
    {

        /// <summary>
        /// 提交审批流程
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>审批实例</returns>
        DevAppInst SubmitWorkFlow(DevAppInst appInst);
        /// <summary>
        /// 提交流程修改对象流程信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns></returns>
        int SubmitWfUpdateObjWfInfo(DevAppInst appInst);
        /// <summary>
        /// 审批历史
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="sessionUserId">当前登录人员</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
         AjaxListResult<DevApproveHistListDTO> GetAppHistList<s>(PageInfo<DevAppInst> pageInfo, int sessionUserId, Expression<Func<DevAppInst, bool>> whereLambda, Expression<Func<DevAppInst, s>> orderbyLambda, bool isAsc);

         /// <summary>
        /// 根据用户和审批对象ID获取当前数据审批权限
        /// 
        /// </summary>
        /// <param name="reqFlowInfo"></param>
        /// <returns>审批实例ID以及当前用户的权限</returns>
         AppFlowInceInfo GetAppFlowInceInfo(ReqFlowInfoData reqFlowInfo);
    }
}
