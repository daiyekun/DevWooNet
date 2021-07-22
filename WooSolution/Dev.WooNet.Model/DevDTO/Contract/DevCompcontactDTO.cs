using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{

    /// <summary>
    /// 合同对方联系人
    /// </summary>
    public class DevCompcontactDTO: DevCompcontact
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string AddUserName { get; set; }
    }
}
