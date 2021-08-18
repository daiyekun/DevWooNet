using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{
    /// <summary>
    /// 流程模板对象
    /// </summary>
    public partial class DevFlowTempDTO: DevFlowTemp
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string AddUserName { get; set; }
        /// <summary>
        /// 审批对象描述
        /// </summary>
        public string ObjTypeDic { get; set; }
        /// <summary>
        /// 审批对象类别
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 所属经办机构
        /// </summary>
        public string DeptsName { get; set; }
        /// <summary>
        /// 流程审批事项
        /// </summary>
        public string FlowItemsDic { get; set; }

        /// <summary>
        /// 字典类别数组
        /// </summary>
        public IList<int> CateIdsArray { get; set; }
        /// <summary>
        /// 组织机构数组
        /// </summary>
        public IList<int> DeptIdsArray { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public IList<int> FlowItemsArray { get; set; }

    }
}
