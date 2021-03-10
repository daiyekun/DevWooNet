using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevLoginLog
    {
        public int Id { get; set; }
        public int? LoginUserId { get; set; }
        public string RequestNetIp { get; set; }
        public string LoginIp { get; set; }
        public sbyte? Result { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public sbyte? Status { get; set; }
    }
}
