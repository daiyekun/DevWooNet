using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppGroupUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int? UserId { get; set; }
        public int? InstId { get; set; }
        public string NodeStrId { get; set; }
    }
}
