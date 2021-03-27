using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{

    /// <summary>
    /// 树形选择对象
    /// </summary>
    public class TreeSelectInfo
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string title { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int id { get; set; }
        public bool open { get; set; } = true;

        public bool Checked { get; set; } = false;
        public IList<TreeSelectInfo> children;
    }
}
