using AutoMapper;
using Dev.WooNet.AutoMapper.Extend;
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


/// <summary>
/// 联系人
/// </summary>

namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers
{
    /// <summary>
    /// 合同对方联系人
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevCompContactController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompcontactService _IDevCompcontactService;

           public DevCompContactController(IMapper iMapper,IDevCompcontactService iDevCompcontactService)
           {
            _IDevCompcontactService = iDevCompcontactService;
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
            var pageInfo = new NoPageInfo<DevCompcontact>();
            var prdAnd = PredBuilder.True<DevCompcontact>();
            prdAnd = prdAnd.And(a => a.IsDelete == 0&&a.CompId== pgInfo.otherId);
            var prdOr = PredBuilder.False<DevCompcontact>();
            
            var pagelist = _IDevCompcontactService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("custcontactsave")]
        [HttpPost]
        public IActionResult CustcontactSave([FromBody] DevCompcontactDTO infodto)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            if (infodto.Id > 0)
            {
                var cinfo = _IDevCompcontactService.Find(infodto.Id);
                var saveinfo= _IMapper.Map<DevCompcontactDTO, DevCompcontact>(infodto, cinfo);
                saveinfo.UpdateUserId = userId;
                saveinfo.UpdateDateTime = DateTime.Now;
                _IDevCompcontactService.Update(saveinfo);

            }
            else
            {
                var saveinfo = _IMapper.Map<DevCompcontact>(infodto);
                saveinfo.AddUserId = userId;
                saveinfo.AddDateTime = DateTime.Now;
                saveinfo.UpdateUserId = userId;
                saveinfo.CompId = (saveinfo.CompId??0)== 0 ? -userId : saveinfo.CompId;
                _IDevCompcontactService.Add(saveinfo);

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
            return new DevResultJson(new AjaxResult<DevCompcontactDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevCompcontactService.GetInfoById(Id)


            });

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除IDs</param>
        /// <returns></returns>
        [Route("deletecontact")]
        [HttpGet]
        public IActionResult DeleteContact(string Ids)
        {
            string strsql = $"DELETE from dev_compcontact where Id in({Ids})";
           
            _IDevCompcontactService.ExecuteSqlCommand(strsql);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }



    }
}
