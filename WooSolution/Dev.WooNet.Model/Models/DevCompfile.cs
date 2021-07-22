using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevCompfile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? FileClassId { get; set; }
        public string Remark { get; set; }
        public int? CompId { get; set; }
        public int? DowNumber { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int? IsDelete { get; set; }
        public string FolderName { get; set; }
        public string GuidFileName { get; set; }
        public string Extension { get; set; }
    }
}
