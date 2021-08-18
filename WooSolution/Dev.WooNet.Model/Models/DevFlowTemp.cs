﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevFlowTemp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public int IsValid { get; set; }
        public int ObjType { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddDateTime { get; set; }
        public int IsDelete { get; set; }
        public string DeptIds { get; set; }
        public string CategoryIds { get; set; }
        public string FlowItems { get; set; }
    }
}
