using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevTempNodeLine
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int TempId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Dash { get; set; }
        public int Marked { get; set; }
        public int Alt { get; set; }
        public float? M { get; set; }
    }
}
