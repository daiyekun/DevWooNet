using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevCurrencyManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sname { get; set; }
        public string Abbreviation { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
    }
}
