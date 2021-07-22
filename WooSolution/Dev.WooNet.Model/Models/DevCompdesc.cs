using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevCompdesc
    {
        public int Id { get; set; }
        public int? CompId { get; set; }
        public string Item { get; set; }
        public string Remark { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int IsDelete { get; set; }
    }
}
