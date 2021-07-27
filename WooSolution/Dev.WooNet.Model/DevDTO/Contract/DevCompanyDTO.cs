using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{

    /// <summary>
    /// 合同对方
    /// </summary>
    public class DevCompanyDTO: DevCompany
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string AddUserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateDic { get; set; }
        /// <summary>
        /// 国家
        /// </summary>

        public string GjName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string PrName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CompClassName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        public string CareditName { get; set; }

    }
}
