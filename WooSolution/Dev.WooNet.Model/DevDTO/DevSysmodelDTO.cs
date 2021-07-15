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
}
