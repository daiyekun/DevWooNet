using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevFlowTempNodeInfo
    {
        public int Id { get; set; }
        public int TempId { get; set; }
        public string NodeStrId { get; set; }
        public int Nrule { get; set; }
        public int ReviseText { get; set; }
        public int GroupId { get; set; }
    }
}
