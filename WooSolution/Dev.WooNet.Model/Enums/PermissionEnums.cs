using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{
    /// <summary>
    /// 权限枚举
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum PermissionDicEnum
    {
        /// <summary>
        /// 无权限：0
        /// </summary>
        [EnumItem(Value = -1, Desc = "无权限")]
        None = -1,
        /// <summary>
        /// 有权限：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "有权限")]
        OK = 0,
        /// <summary>
        /// 当前状态不允许操作：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "当前状态不允许此操作")]
        NotState = 2,


    }
    [EnumClass(Max = 15, Min = 0, None = -1)]
    /// <summary>
    /// 系统数据状态
    /// </summary>
    public enum SysDataSateEnum
    {
        /// <summary>
        ///未审核
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审核")]
        Unreviewed = 0,
        /// <summary>
        ///审核通过
        /// </summary>
        [EnumItem(Value = 1, Desc = "审核通过")]
        Verified = 1,

    }

}
