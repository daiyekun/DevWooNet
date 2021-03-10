using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevUserinfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string ShowName { get; set; }
        public sbyte? Sex { get; set; }
        public int? Age { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? EntryDatetime { get; set; }
        public string IdNo { get; set; }
        public int? DepId { get; set; }
        public int? Dsort { get; set; }
        public int Ustate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public sbyte IsDelete { get; set; }
        public sbyte Mstart { get; set; }
        public string WxCode { get; set; }
    }
}
