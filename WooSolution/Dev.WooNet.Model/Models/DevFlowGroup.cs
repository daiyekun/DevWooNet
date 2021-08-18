using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevFlowGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int Gstate { get; set; }
        public int IsDelete { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddDateTime { get; set; }
    }
}
