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
        /// Iframe：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "Iframe")]
        Iframe = 1,
        /// <summary>
        /// html:0
        /// </summary>
        [EnumItem(Value = 0, Desc = "html")]
        html = 0,


    
}
}
