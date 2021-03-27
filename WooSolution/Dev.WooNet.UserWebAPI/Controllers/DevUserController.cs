using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Dev.WooNet.WooService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.WooNet.UserWebAPI.Controllers
{
    /// <summary>
    /// 用户相关操作接口WebAPI
    /// </summary>
   // [CORSFilter]
    
    [Route("api/[controller]")]
    [ApiController]
    public class DevUserController : ControllerBase
    {
        private IDevUserinfoService _IDevUserinfoService;
        public DevUserController(IDevUserinfoService DevUserinfoService)
        {
            _IDevUserinfoService = DevUserinfoService;
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="info">用户对象</param>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpPost]
       // [TypeFilter(typeof(CustomAction2CommitFilterAttribute))]
        public JsonResult AddDevUser([FromForm]DevUserinfo info)
        {
            _IDevUserinfoService.Add(info);
            return new JsonResult(new AjaxResult()
            {
                Result = true,
                msg = "success"
            });
        }
        [Route("query")]
        [HttpGet]
        public JsonResult QueryUser(string uname, string upwd)
        {


            AjaxResult<DevUserinfo> ajaxResult = null;
            DevUserinfo tbUser = _IDevUserinfoService.GetQueryable(a => a.Name == uname && a.Pwd == upwd).FirstOrDefault();

            ajaxResult = new AjaxResult<DevUserinfo>()
            {
                Result = true,
                data = tbUser
            };
            return new JsonResult(ajaxResult);
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pgInfo">分页查询对象</param>
        /// <returns>页数据</returns>
    
        // [Route("list")]
        [Route("list")]
        [HttpGet]
        public IActionResult GetListUser(PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevUserinfo>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevUserinfo>();
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

       
        
       
    }
}
