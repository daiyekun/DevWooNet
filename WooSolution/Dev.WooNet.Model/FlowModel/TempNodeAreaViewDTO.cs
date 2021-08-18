using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 泳道
    /// </summary>
    public class TempNodeAreaViewDTO
    {
        public string strid { get; set; }
        public string name { get; set; }

        public int? left { get; set; }

        public int? top { get; set; }

        public int? width { get; set; }

        public int? height { get; set; }

        public string color { get; set; }

        public bool alt { get; set; }
    }
}
