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

    }
}
