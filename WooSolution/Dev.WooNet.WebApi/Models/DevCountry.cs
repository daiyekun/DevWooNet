﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShowName { get; set; }
        public uint IsShow { get; set; }
    }
}
