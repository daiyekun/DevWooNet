using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{

    /// <summary>
    /// 菜单权限类别
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum FunTypeEnums
    {
        /// <summary>
        /// 不设置权限-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "不设置权限")]
        FunTypeN = -1,
        /// <summary>
        /// 是/否
        /// </summary>
        [EnumItem(Value = 0, Desc = "是/否")]
        FunType0 = 0,
        /// <summary>
        /// 个人/本机构/部门/全部
        /// </summary>
        [EnumItem(Value = 1, Desc = "个人/本机构/部门/全部/无权限")]
        FunType1 = 1,
       
    }
}
