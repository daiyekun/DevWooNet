using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstOpin
    {
        public int Id { get; set; }
        public int InstId { get; set; }
        public int? NodeId { get; set; }
        public string NodeStrId { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddDateTime { get; set; }
        public string Opinion { get; set; }
        public int Result { get; set; }
    }
}
