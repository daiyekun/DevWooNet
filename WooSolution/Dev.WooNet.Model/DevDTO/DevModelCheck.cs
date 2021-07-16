using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{
    /// <summary>
    /// 菜单权限承载对象
    /// </summary>
    public class DevModelCheck
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Chk { get; set; } = false;
        /// <summary>
        /// 菜单名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 子项
        /// </summary>
        public IList<DevModelCheck> ChildrenItem { get; set; }
        
    }
   
}
