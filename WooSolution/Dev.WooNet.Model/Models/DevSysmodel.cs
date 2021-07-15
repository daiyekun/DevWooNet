using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevSysmodel
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Sort { get; set; }
        public string RequestUrl { get; set; }
        public string Remark { get; set; }
        public int IsShow { get; set; }
        public int IsDelete { get; set; }
        public int IsSystem { get; set; }
        public string Mpath { get; set; }
        public int Leaf { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public string Ico { get; set; }
        public string Title { get; set; }
        public int PageType { get; set; }
    }
}
