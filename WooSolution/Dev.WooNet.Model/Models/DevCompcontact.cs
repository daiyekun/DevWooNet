using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevCompcontact
    {
        public int Id { get; set; }
        public int? CompId { get; set; }
        public string Name { get; set; }
        public string Dname { get; set; }
        public string RoleName { get; set; }
        public string PhoneTel { get; set; }
        public int? PhoneNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Qq { get; set; }
        public int? AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int IsDelete { get; set; }
    }
}
