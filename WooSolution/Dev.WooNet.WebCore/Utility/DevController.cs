using Dev.WooNet.Model.ExtendModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WebCore.Utility
{
    /// <summary>
    /// 自定义base 控制器
    /// </summary>
     public class DevController: ControllerBase
     {
        public RequestBaseData ReqData { get; set; }

        public DevController()
        {
            var data = new RequestBaseData();
            var devuserId = 0;
            var devdeptId = 0;
            if (HttpContext.User.Claims.Count()>0)
            {  var claimsuserId= HttpContext.User.Claims.Where(a => a.Type == "UserId").FirstOrDefault();
                var claimsName = HttpContext.User.Claims.Where(a => a.Type == "Name").FirstOrDefault();
                var claimsDeptId = HttpContext.User.Claims.Where(a => a.Type == "DeptId").FirstOrDefault();
                var claimsShowName = HttpContext.User.Claims.Where(a => a.Type == "ShowName").FirstOrDefault();
                var claimsDeptName = HttpContext.User.Claims.Where(a => a.Type == "DeptName").FirstOrDefault();
                var claimsRoleIds = HttpContext.User.Claims.Where(a => a.Type == "RoleIds").FirstOrDefault();
                int.TryParse((claimsuserId != null ? claimsuserId.Value : "0"), out devuserId);
                int.TryParse((claimsDeptId != null ? claimsDeptId.Value : "0"), out devdeptId);
                data.UserId = devuserId;
                data.Name = claimsName != null ? claimsName.Value : "";
                data.DeptId = devdeptId;
                data.ShowName = claimsShowName != null ? claimsShowName.Value : "";
                data.DeptName = claimsDeptName != null ? claimsDeptName.Value : "";
                data.RoleIds = claimsRoleIds != null ? claimsRoleIds.Value : "";

            }
            ReqData = data;

        }

    }
}
