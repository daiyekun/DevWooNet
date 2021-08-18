using Dev.WooNet.Model;
using Dev.WooNet.Model.FlowModel;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{
   public partial interface IDevFlowTempNodeService
    {
        /// <summary>
        /// 提交流程时显示流程图程序入口
        /// </summary>
        /// <param name="submitWfRes">提交流程是参数对象</param>
        /// <returns></returns>
       // FlowNodeData SubmitLoadNodes(SubmitWfRequest submitWfRes);
        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempinfo">模板对象</param>
        /// <returns></returns>
        FlowNodeData LoadNodes(DevFlowTemp tempinfo);
        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempId">模板Id</param>
        /// <returns></returns>
        FlowNodeData LoadNodes(int tempId);

        FlowNodeData LoadNodes(SubmitWfRequest submitWfRes);
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowNodeData">流程节点信息</param>
        /// <param name="tempId">流程模板ID</param>
        /// <returns></returns>
        int AddFlowNodes(FlowNodeData flowNodeData, int tempId);
        /// <summary>
        /// 清除节点数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        int ClearFlowNodes(int tempId);
    }
}
