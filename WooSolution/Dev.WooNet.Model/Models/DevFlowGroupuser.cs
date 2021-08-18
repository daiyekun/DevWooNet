using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevFlowGroupuser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
