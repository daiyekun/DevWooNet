using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevAppInstHist
    {
        public int Id { get; set; }
        public int? InstId { get; set; }
        public int TempHistId { get; set; }
        public int Version { get; set; }
        public int ObjType { get; set; }
        public int AppObjId { get; set; }
        public string AppObjName { get; set; }
        public string AppObjNo { get; set; }
        public decimal AppObjAmount { get; set; }
        public int AppObjCateId { get; set; }
        public int AppState { get; set; }
        public int Mission { get; set; }
        public int StartUserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddDatetime { get; set; }
        public int CurrentNodeId { get; set; }
        public string CurrentNodeName { get; set; }
        public DateTime? CompleteDateTime { get; set; }
        public int? NewInstId { get; set; }
        public string CurrentNodeStrId { get; set; }
        public int TempId { get; set; }
    }
}
