using System;
using System.Collections.Generic;

#nullable disable

namespace Dev.WooNet.WebAPI.Models
{
    public partial class DevSysemail
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int? ServicePort { get; set; }
        public string SenderMail { get; set; }
        public string SendNickname { get; set; }
        public string MailPwd { get; set; }
    }
}
