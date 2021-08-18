﻿using Dev.WooNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{

    /// <summary>
    /// 节点信息
    /// </summary>
    public partial interface IDevFlowTempNodeInfoService
    {

        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <returns></returns>
        FlowTempNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int tempId);
        
        }
}
