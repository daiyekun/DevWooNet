using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevUserController : ControllerBase
    {
        private IDevUserinfoService _IDevUserinfoService;
        private IMapper _IMapper;
        public DevUserController(IDevUserinfoService DevUserinfoService
            ,IMapper iMapper)
        {
            _IDevUserinfoService = DevUserinfoService;
            _IMapper = iMapper;
        }
        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("userSave")]
        [HttpPost]
        [CustomActionFilter]
        public IActionResult UserSave([FromBody] DevUserinfoDTO userdto)
        {
            var userinfo = _IMapper.Map<DevUserinfo>(userdto);
            _IDevUserinfoService.SaveUser(userinfo);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("deluser")]
        [HttpGet]
        public IActionResult DeleteUser(string Ids)
        {
            _IDevUserinfoService.DelUser(Ids);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }

        [Route("list")]
        [HttpPost]
        public IActionResult  GetListUser([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevUserinfo>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevUserinfo>();
            prdAnd = prdAnd.And(a=>a.IsDelete!=1);
            var prdOr = PredBuilder.False<DevUserinfo>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.ShowName.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.IdNo.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            var pagelist = _IDevUserinfoService.GetList(pageInfo, prdAnd, a => a.Id, false);
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
            return new DevResultJson(new AjaxResult<DevUserinfoDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevUserinfoService.GetUserById(Id)


            });

        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [Route("updateState")]
        [HttpPost]
        public IActionResult UpdateState([FromBody] UpdateField updateField)
        {
            _IDevUserinfoService.UpdateState(updateField);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });
        }

    }
}
