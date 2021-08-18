﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevAppInstNodeinfoHist
    {
        public int Id { get; set; }
        public int InstHistId { get; set; }
        public int? InstNodeHistId { get; set; }
        public string NodeStrId { get; set; }
        public int? Nrule { get; set; }
        public int? ReviseText { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public int IsMin { get; set; }
        public int IsMax { get; set; }
        public int? NodeState { get; set; }
    }
}
