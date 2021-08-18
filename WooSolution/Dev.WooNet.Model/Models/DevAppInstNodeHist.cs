﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstNodeHist
    {
        public int Id { get; set; }
        public int InstHistId { get; set; }
        public int TempHistId { get; set; }
        public string NodeStrId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Type { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Alt { get; set; }
        public int? Marked { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public int? Norder { get; set; }
        public int? NodeState { get; set; }
        public DateTime? ReceDateTime { get; set; }
        public DateTime? CompDateTime { get; set; }
    }
}
