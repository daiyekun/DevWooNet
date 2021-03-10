using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevDepartment
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? CateId { get; set; }
        public string Dsort { get; set; }
        public string Remark { get; set; }
        public sbyte? IsMain { get; set; }
        public string Sname { get; set; }
        public sbyte? IsCompany { get; set; }
        public sbyte IsDelete { get; set; }
        public sbyte? Dstatus { get; set; }
        public string Dpath { get; set; }
        public int? Leaf { get; set; }
    }
}
