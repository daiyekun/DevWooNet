using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstNodeArea
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int InstId { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Color { get; set; }
        public int Alt { get; set; }
    }
}
