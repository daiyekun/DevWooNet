﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevRolePession
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int FuncId { get; set; }
        public string FuncCode { get; set; }
        public int FuncType { get; set; }
        public string DeptIds { get; set; }
    }
}