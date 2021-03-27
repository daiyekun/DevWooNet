using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{

    /// <summary>
    ///  部门状态枚举
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum DepartStateEnums
    {
        /// <summary>
        /// 启用：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "启用")]
        QiYong = 1,
        /// <summary>
        /// 禁用:0
        /// </summary>
        [EnumItem(Value = 0, Desc = "禁用")]
        JinYong = 0,

    }

    /// <summary>
    /// 其他状态值：比如是、否
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

