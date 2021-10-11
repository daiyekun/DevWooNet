using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Extend;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers
{
    /// <summary>
    /// 备忘录
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevCompDescController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompdescService _IDevCompdescService;

        public DevCompDescController(IMapper iMapper, IDevCompdescService iDevCompdescService)
        {
            _IDevCompdescService = iDevCompdescService;
            _IMapper = iMapper;

        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pgInfo">分页对象</param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public IActionResult GetList([FromBody] PgRequestInfo pgInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageInfo = new NoPageInfo<DevCompdesc>();
            var prdAnd = PredBuilder.True<DevCompdesc>();
            prdAnd = prdAnd.And(a => a.IsDelete == 0 && ((pgInfo!=null&&a.CompId == pgInfo.otherId)||a.CompId==-userId));
            var prdOr = PredBuilder.False<DevCompcontact>();

            var pagelist = _IDevCompdescService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("save")]
        [HttpPost]
        public IActionResult CustcontactSave([FromBody] DevCompdescDTO infodto)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (infodto.Id > 0)
            {
                var cinfo = _IDevCompdescService.Find(infodto.Id);
                var saveinfo = _IMapper.Map<DevCompdescDTO, DevCompdesc>(infodto, cinfo);
                saveinfo.UpdateUserId = userId;
                saveinfo.UpdateDateTime = DateTime.Now;
                _IDevCompdescService.Update(saveinfo);

            }
            else
            {
                var saveinfo = _IMapper.Map<DevCompdesc>(infodto);
                saveinfo.AddUserId = userId;
                saveinfo.AddDateTime = DateTime.Now;
                saveinfo.UpdateUserId = userId;
                saveinfo.CompId = (saveinfo.CompId ?? 0) == 0 ? -userId : saveinfo.CompId;
                _IDevCompdescService.Add(saveinfo);

            }
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


        }
        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("showView")]
        [HttpGet]
        public IActionResult ShowView(int Id)
        {
            return new DevResultJson(new AjaxResult<DevCompdescDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevCompdescService.GetInfoById(Id)


            });

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除IDs</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteContact(string Ids)
        {
            string strsql = $"DELETE from dev_compdesc where Id in({Ids})";

            _IDevCompdescService.ExecuteSqlCommand(strsql);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }
    }
}
