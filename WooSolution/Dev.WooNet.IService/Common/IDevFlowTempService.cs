using Dev.WooNet.Common.Models;
using Dev.WooNet.Model;
using Dev.WooNet.Model.DevDTO;
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
    /// 流程模板
    /// </summary>
   public partial interface IDevFlowTempService
    {
        /// <summary>
        /// 流程模板列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        AjaxListResult<DevFlowTempDTO> GetList<s>(PageInfo<DevFlowTemp> pageInfo, Expression<Func<DevFlowTemp, bool>> whereLambda,
            Expression<Func<DevFlowTemp, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        DevFlowTempDTO ShowView(int Id);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="flowTemp">流程模板</param>
        /// <returns></returns>
        DevFlowTemp AddSave(DevFlowTemp flowTemp, DevFlowTempHist flowTempHist);
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="flowTemp"></param>
        /// <returns></returns>
        DevFlowTemp UpdateSave(DevFlowTemp flowTemp, DevFlowTempHist flowTempHist);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 根据参数获取流程模板及审批实例
        /// 用于判断当前是否能够提交流程
        /// </summary>
        /// <param name="requestTemp">请求对象</param>
        /// <returns></returns>
        ResponTempData GetFlowInfoByWhere(GetTempFlowRequestData requestTemp);
        /// <summary>
        /// 判断流程节点是否匹配完成
        /// </summary>
        /// <param name="requestCheckFlow">判断流程模板条件对象</param>
        /// <returns></returns>
        int SubmitCheckFlow(RequestCheckFlow requestCheckFlow);

    }
}
