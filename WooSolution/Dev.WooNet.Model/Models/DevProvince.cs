using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevProvince
    {
        public int Id { get; set; }
        public int? Cid { get; set; }
        public string Name { get; set; }
        public string ShowName { get; set; }
        public int IsShow { get; set; }
    }
}
