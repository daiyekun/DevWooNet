using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public sbyte Rstate { get; set; }
        public string Remark { get; set; }
        public int? DeptId { get; set; }
        public sbyte IsDelete { get; set; }
    }
}
