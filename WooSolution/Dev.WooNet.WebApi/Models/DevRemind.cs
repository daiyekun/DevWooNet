using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevRemind
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Name { get; set; }
        public string CustomName { get; set; }
        public int? AheadDays { get; set; }
        public int? DelayDays { get; set; }
    }
}
