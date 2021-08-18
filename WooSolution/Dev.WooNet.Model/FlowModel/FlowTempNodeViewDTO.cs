using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{
    /// <summary>
    /// 流程节点
    /// </summary>
   public class FlowTempNodeViewDTO
    {
        public string strid { get; set; }
        public string name { get; set; }
        public int? left { get; set; }
        public int? top { get; set; }
        public string type { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
        public bool alt { get; set; }
        public bool marked { get; set; }
    }
}
