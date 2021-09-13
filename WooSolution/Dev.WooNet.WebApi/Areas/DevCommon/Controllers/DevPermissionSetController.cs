using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 权限设置
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevPermissionSetController : ControllerBase
    {
        private IDevRolePessionService _IDevRolePessionService;
        public DevPermissionSetController(IDevRolePessionService iDevRolePessionService)
        {
            _IDevRolePessionService = iDevRolePessionService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pgInfo">分页对象</param>
        /// <returns></returns>
        [Route("getfunctionlist")]
        [HttpPost]
        public IActionResult GetFunctions([FromBody] PgRequestInfo pgInfo)
        {
            var lists = DevSysModelUtility.InitFuncts();
            var resultlist = lists.Where(a => a.Mid == pgInfo.otherId).ToList();
            return new DevResultJson( new AjaxListResult<SysModelFuncSet>()
            {
                data = resultlist,
                count = resultlist.Count,
                code = 0


            });
           

        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="rolePermissions"></param>
        /// <returns></returns>
        [Route("saverolepermssion")]
        [HttpPost]
        public IActionResult AllotPermssion([FromBody]IList<DevRolePession> rolePermissions)
        {
            _IDevRolePessionService.SavePermission(rolePermissions);
            return new DevResultJson(new AjaxResult()
            {
                msg = "操作成功",
                code = 0
            });
        }
    }
}
