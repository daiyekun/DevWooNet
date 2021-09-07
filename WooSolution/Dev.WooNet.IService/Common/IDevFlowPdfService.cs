using Dev.WooNet.Model.DevDTO.DevFlow.FlowPdfModel;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{

    /// <summary>
    /// pdf生成
    /// </summary>
    public interface IDevFlowPdfService:IBaseService<DevAppInst>
    {
        /// <summary>
        /// 获取合同对方审批单相关信息
        /// 包含审批节点信息及意见
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        CompanyInfo GetCompanyFlowPdfInfo(DevAppInst appInst);
    }
}
