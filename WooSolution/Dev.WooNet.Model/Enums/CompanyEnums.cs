using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{

    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum CompanyStateEnum
    {
        /// <summary>
        /// 未审核：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审核")]
        WeiShenHe = 0,
        /// <summary>
        /// 审核通过:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "审核通过")]
        ShenHeTongGuo = 1,
        /// <summary>
        /// 已终止：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "已终止")]
        YiZhongZhi = 2,

    }
}
