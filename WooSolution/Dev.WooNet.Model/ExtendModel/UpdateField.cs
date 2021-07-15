using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{

    /// <summary>
    /// 修改字段对象
    /// </summary>
    public class UpdateField
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 修改字段
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 修改值
        /// </summary>
        public string UpdateVal { get; set; }
        /// <summary>
        /// 修改金额字段
        /// </summary>
        public decimal UpMonery { get; set; }
        /// <summary>
        /// 修改状态
        /// </summary>

        public int State { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int CurrState { get; set; }

    }
}
