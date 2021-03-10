using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevUserminor
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public string Minfo { get; set; }
        public string PhName { get; set; }
        public string PhPath { get; set; }
        public sbyte? UserQz { get; set; }
        public sbyte? QzState { get; set; }
        public int? UserId { get; set; }
    }
}
