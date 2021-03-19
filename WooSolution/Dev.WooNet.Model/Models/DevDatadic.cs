using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevDatadic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pid { get; set; }
        public int? Sort { get; set; }
        public int TypeInt { get; set; }
        public string Remark { get; set; }
        public int? FundsNature { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public sbyte IsDelete { get; set; }
    }
}
