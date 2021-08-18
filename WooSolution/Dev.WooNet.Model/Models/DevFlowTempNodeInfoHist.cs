using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevFlowTempNodeInfoHist
    {
        public int Id { get; set; }
        public int TempHistId { get; set; }
        public string NodeStrId { get; set; }
        public int Nrule { get; set; }
        public int ReviseText { get; set; }
        public int GroupId { get; set; }
        public decimal? Max { get; set; }
        public decimal? Min { get; set; }
        public int? IsMax { get; set; }
        public int? IsMin { get; set; }
    }
}
