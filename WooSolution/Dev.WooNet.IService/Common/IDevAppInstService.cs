using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
