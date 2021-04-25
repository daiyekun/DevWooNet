using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    ///角色API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevRoleController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevRoleService _IDevRoleService;
        public DevRoleController(IMapper iMapper, IDevRoleService iDevRoleService)
        {
            _IMapper = iMapper;
            _IDevRoleService = iDevRoleService;

        }
        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="roledto">角色信息</param>
        /// <returns></returns>
        [Route("roleSave")]
        [HttpPost]
        [CustomActionFilter]
        public IActionResult RoleSave([FromBody] DevRoleDTO roledto)
        {
            var info = _IMapper.Map<DevRole>(roledto);
            _IDevRoleService.SaveRole(info);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("delrole")]
        [HttpGet]
        public IActionResult DeleteUser(string Ids)
        {
            _IDevRoleService.DelRole(Ids);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pgInfo"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public IActionResult GetListRole([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevRole>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevRole>();
            prdAnd = prdAnd.And(a => a.IsDelete != 1);
            var prdOr = PredBuilder.False<DevRole>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.Code.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            var pagelist = _IDevRoleService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 显示页面信息-主要用于修改和查看
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("showView")]
        [HttpGet]
        public IActionResult ShowView(int Id)
        {
            return new DevResultJson(new AjaxResult<DevRoleDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevRoleService.GetRoleById(Id)


            });

        }

        /// <summary>
        /// 添加角色用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("saveroleUser")]
        [HttpGet]
        public IActionResult SaveroleUser(int RoleId,string Ids)
        {

            var res = _IDevRoleService.SaveRoleUser(RoleId, Ids);
            return new DevResultJson(new AjaxResult<DevRoleDTO>()
            {
                msg = "",
                code = 0,
               


            });

        }
        /// <summary>
        /// 删除角色用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("deleteroleUser")]
        [HttpGet]
        public IActionResult DeleteroleUser(int RoleId, string Ids)
        {

            var res = _IDevRoleService.DeleteRoleUser(RoleId, Ids);
            return new DevResultJson(new AjaxResult<DevRoleDTO>()
            {
                msg = "",
                code = 0,



            });

        }
       
    }
}
