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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevCompFileController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompfileService _IDevCompfileService;

        public DevCompFileController(IMapper iMapper, IDevCompfileService iDevCompfileService)
        {
            _IDevCompfileService = iDevCompfileService;
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
            var pageInfo = new NoPageInfo<DevCompfile>();
            var prdAnd = PredBuilder.True<DevCompfile>();
            prdAnd = prdAnd.And(a => a.IsDelete == 0 && a.CompId == pgInfo.otherId);
            var prdOr = PredBuilder.False<DevCompfile>();

            var pagelist = _IDevCompfileService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("save")]
        [HttpPost]
        public IActionResult CustcontactSave([FromBody] DevCompfileDTO infodto)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (infodto.Id > 0)
            {
                var cinfo = _IDevCompfileService.Find(infodto.Id);
                var saveinfo = _IMapper.Map<DevCompfileDTO, DevCompfile>(infodto, cinfo);
                saveinfo.UpdateUserId = userId;
                saveinfo.UpdateDateTime = DateTime.Now;
                _IDevCompfileService.Update(saveinfo);

            }
            else
            {
                var saveinfo = _IMapper.Map<DevCompfile>(infodto);
                saveinfo.AddUserId = userId;
                saveinfo.AddDateTime = DateTime.Now;
                saveinfo.UpdateUserId = userId;
                saveinfo.CompId = (saveinfo.CompId ?? 0) == 0 ? -userId : saveinfo.CompId;
                _IDevCompfileService.Add(saveinfo);

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
            return new DevResultJson(new AjaxResult<DevCompfileDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevCompfileService.GetInfoById(Id)


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
            string strsql = $"DELETE from dev_compfile where Id in({Ids})";

            _IDevCompfileService.ExecuteSqlCommand(strsql);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }
    }
}
