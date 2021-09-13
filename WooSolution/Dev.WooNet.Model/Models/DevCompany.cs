using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Dtype { get; set; }
        public int? LevelId { get; set; }
        public int? CareditId { get; set; }
        public int? CompType { get; set; }
        public int? CompClassId { get; set; }
        public int IsDelete { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public string Trade { get; set; }
        public string Caddress { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string Cfax { get; set; }
        public string RegCapital { get; set; }
        public string RegAddress { get; set; }
        public DateTime? EsDateTime { get; set; }
        public string BusTerm { get; set; }
        public DateTime? ExpDateTime { get; set; }
        public string DutyNo { get; set; }
        public string InvAddress { get; set; }
        public string InvTitle { get; set; }
        public string InvTel { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string PaidCapital { get; set; }
        public string LegalPerson { get; set; }
        public string WebUrl { get; set; }
        public string Remark { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string BusScope { get; set; }
        public int? FaceUserId { get; set; }
        public int? Dstatus { get; set; }
        public int? Wstatus { get; set; }
        public int? FlowTo { get; set; }
        public string WnodeName { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual DevUserinfo AddUser { get; set; }
        public virtual DevUserinfo FaceUser { get; set; }
    }
}
