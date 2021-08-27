using Dev.WooNet.Model.FlowModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{

    /// <summary>
    /// 审批实例节点
    /// </summary>
    public partial interface IDevAppInstNodeService
    {
        /// <summary>
        /// 根据实例加载流程图 
        /// </summary>
        /// <param name="instId">实例ID</param>
        /// <returns></returns>
        AppFlowNodeDataJson LoadFlowChart(int instId);
        AppInstNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int instId);

    }
}
