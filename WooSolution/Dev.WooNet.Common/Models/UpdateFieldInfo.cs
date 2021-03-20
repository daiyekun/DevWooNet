using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{
    /// <summary>
    /// 修改对象
    /// </summary>
    public class UpdateFieldInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 其他ID 
        /// </summary>
        public int OtherId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 修改值
        /// </summary>
        public string FieldVal { get; set; }
        /// <summary>
        /// 修改其他字段，比如金额等
        /// </summary>
        public decimal UpOther { get; set; } = 0;
    }
}
