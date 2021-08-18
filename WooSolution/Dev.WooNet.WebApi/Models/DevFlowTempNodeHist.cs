using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevFlowTempNodeHist
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int TempHistId { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Alt { get; set; }
        public int Marked { get; set; }
        public int Rule { get; set; }
        public int ReviseText { get; set; }
        public int GroupId { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddDateTime { get; set; }
    }
}
