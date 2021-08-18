using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevOptionLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Remark { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestData { get; set; }
        public string RequestIp { get; set; }
        public string RequestNetIp { get; set; }
        public float? TotalTime { get; set; }
        public DateTime CreateDatetime { get; set; }
        public sbyte? Status { get; set; }
        public string ActionTitle { get; set; }
        public string ExecResult { get; set; }
        public int? OptionType { get; set; }
    }
}
