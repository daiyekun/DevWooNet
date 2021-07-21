using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public class DevSysmodelDTO: DevSysmodel,IModelDTO
    {
        /// <summary>
        /// 父菜单名称
        /// </summary>
        public string PName { get; set; }
        public int id { get; set; }
        public int pid { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public string IsShowDic { get; set; }
        /// <summary>
        /// 页面类型
        /// </summary>
        public string PageTypeDic { get; set; }
        /// <summary>
        /// 系统菜单
        /// </summary>

        public string IsSystemDic { get; set; }
    }
    /// <summary>
    /// windows桌面菜单
    /// </summary>
    public class WinuiMenu
    {
        /// <summary>
        /// pageurl
        /// </summary>
        public string pageURL { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 打开类型
        /// </summary>
        public int openType { get; set; } = 1;
        /// <summary>
        /// 
        /// </summary>
        public int maxOpen { get; set; } = -1;
        /// <summary>
        /// 打开
        /// </summary>
        public bool extend { get; set; } = false;
        /// <summary>
        /// 子类
        /// </summary>
        public IList<WinuiMenu> childs { get; set; } = null;
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int id { get; set; }

        


    } 
}
