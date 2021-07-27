using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{
    /// <summary>
    ///  枚举消息
    /// </summary>
    [EnumClass(Max = 1000, Min = 0, None = -1)]
    public enum MessageEnums
    {
        /// <summary>
        /// 操作成功：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "操作成功")]
        success = 0,
        /// <summary>
        /// 错误:500
        /// </summary>
        [EnumItem(Value = 500, Desc = "系统异常")]
        Error = 500,
        /// <summary>
        /// 已经存在相关数据：10000
        /// </summary>
        [EnumItem(Value = 10000, Desc = "已经存在相关数据")]
        IsExist = 10000,

    }
}
