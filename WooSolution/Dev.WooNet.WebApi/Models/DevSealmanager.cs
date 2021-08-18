using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevSealmanager
    {
        public int Id { get; set; }
        public string SealName { get; set; }
        public string SealCode { get; set; }
        public int MainDeptId { get; set; }
        public int UserId { get; set; }
        public int DeptId { get; set; }
        public DateTime? EnabledDate { get; set; }
        public sbyte SealState { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public sbyte IsDelete { get; set; }
    }
}
