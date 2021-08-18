using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstNodeLineHist
    {
        public int Id { get; set; }
        public int NodeHistId { get; set; }
        public string StrId { get; set; }
        public int? InstHistId { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int? Dash { get; set; }
        public int? Marked { get; set; }
        public int? Alt { get; set; }
    }
}
