using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevCity
    {
        public int Id { get; set; }
        public int PrId { get; set; }
        public string Name { get; set; }
        public string ShowName { get; set; }
        public int IsShow { get; set; }
    }
}
