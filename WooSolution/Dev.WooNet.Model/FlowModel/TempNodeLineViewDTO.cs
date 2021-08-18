using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model
{

    /// <summary>
    /// 连接线
    /// </summary>
    public  class TempNodeLineViewDTO
    {

        public string strid { get; set; }

        public string name { get; set; }

        public string type { get; set; }
        public double M { get; set; }
        public string from { get; set; }

        public string to { get; set; }

        public bool dash { get; set; }

        public bool marked { get; set; }

        public bool alt { get; set; }
    }
}
