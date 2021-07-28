using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{
    /// <summary>
    /// 图片
    /// </summary>
    public class PicViewDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Url { get; set; }
    }
}
