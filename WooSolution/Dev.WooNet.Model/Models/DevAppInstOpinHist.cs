using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstOpinHist
    {
        public int Id { get; set; }
        public int InstHistId { get; set; }
        public int? NodeHistId { get; set; }
        public string NodeStrId { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDatetTme { get; set; }
        public string Opinion { get; set; }
        public int? Result { get; set; }
    }
}
