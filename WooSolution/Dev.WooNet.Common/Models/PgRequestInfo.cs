using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{

    /// <summary>
    /// 页面请求对象
    /// </summary>
    public class PgRequestInfo
    {
        /// <summary>
        /// 条数
        /// </summary>
        public int limit { get; set; } = 20;
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string kword { get; set; }
        /// <summary>
        /// 第三方标识
        /// </summary>
        public int otherId { get; set; } = -1;
        /// <summary>
        /// 查询类型
        /// 默认情况是0
        /// 
        /// </summary>
        public int searchType { get; set; } = 0;
        /// <summary>
        /// 查询where
        /// 当不等于0的时候记得赋值
        /// 
        /// </summary>
        public string searchWhre { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string acToken { get; set; }
        /// <summary>
        /// 选择项
        /// </summary>
        public int selItem { get; set; } = 0;
    }
}
