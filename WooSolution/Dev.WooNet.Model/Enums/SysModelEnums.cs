using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{

    /// <summary>
    /// 页面类型
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum PageTypeEnum
    {
        /// <summary>
        /// Iframe：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "Iframe")]
        Iframe = 2,
        /// <summary>
        /// html:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "html")]
        html = 1,


    
}
}
