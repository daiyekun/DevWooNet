using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{
    /// <summary>
    /// 是、否
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum IsYesNOEnum
    {
        /// <summary>
        /// 否：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "否")]
        No = 0,
        /// <summary>
        /// 是:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "是")]
        Yes = 1,
    }
}

