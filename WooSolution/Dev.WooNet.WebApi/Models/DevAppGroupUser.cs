using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevAppGroupUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserIds { get; set; }
        public int? InstId { get; set; }
        public int? NodeStrId { get; set; }
    }
}
