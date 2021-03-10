﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevDeptmain
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
        public string LawPerson { get; set; }
        public string TaxId { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Fax { get; set; }
        public string TelePhone { get; set; }
        public string InvoiceName { get; set; }
        public sbyte IsDelete { get; set; }
    }
}
