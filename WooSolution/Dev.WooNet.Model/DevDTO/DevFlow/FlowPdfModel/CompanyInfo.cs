using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO.DevFlow.FlowPdfModel
{

    /// <summary>
    /// 合同对方pdf生成基本信息
    /// </summary>
    public class CompanyInfo: DevFlowPdfBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string AddDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string AddUserName { get; set; }
        /// <summary>
        /// 行业
        /// </summary>

        public string Trade { get; set; }
        /// <summary>
        ///负责人
        /// </summary>

        public string FzrlUser { get; set; }
    }
}
