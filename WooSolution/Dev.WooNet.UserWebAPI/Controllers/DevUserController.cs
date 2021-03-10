using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dev.WooNet.UserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevUserController : ControllerBase
    {
        private IDevUserinfoService _IDevUserinfoService;
        public DevUserController(IDevUserinfoService DevUserinfoService)
        {
            _IDevUserinfoService = DevUserinfoService;
        }
        [Route("AddUser")]
        [HttpPost]
       // [TypeFilter(typeof(CustomAction2CommitFilterAttribute))]
        public JsonResult AddDevUser([FromForm]DevUserinfo info)
        {
            _IDevUserinfoService.Add(info);
            return new JsonResult(new AjaxResult()
            {
                Result = true,
                Message = "success"
            });
        }
        [Route("query")]
        [HttpGet]
        public JsonResult QueryUser(string uname, string upwd)
        {
            
            AjaxResult<DevUserinfo> ajaxResult = null;
            DevUserinfo tbUser = _IDevUserinfoService.GetQueryable(a=>a.Name== uname&&a.Pwd==upwd).FirstOrDefault();

            ajaxResult = new AjaxResult<DevUserinfo>()
            {
                Result = true,
                TValue = tbUser
            };
            return new JsonResult(ajaxResult);
        }
    }
}
