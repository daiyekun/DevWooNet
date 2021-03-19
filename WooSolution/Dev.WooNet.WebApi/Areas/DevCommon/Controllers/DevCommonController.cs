using Dev.WooNet.IWooService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 系统公用API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevCommonController : ControllerBase
    {
        
        private IDevDatadicService _IDevDatadicService;
        private IDevUserinfoService _IDevUserinfoService;

        public DevCommonController(IDevDatadicService iDevDatadicService, IDevUserinfoService iDevUserinfoService)
        {
            _IDevDatadicService = iDevDatadicService;
            _IDevUserinfoService = iDevUserinfoService;
        }

        /// <summary>
        /// Redis 初始化
        /// </summary>
        /// <returns></returns>
        [Route("InitRedisData")]
        [HttpGet]
        public string InitRedisData()
        {
            _IDevDatadicService.SetRedisHash();
            _IDevUserinfoService.SetRedisHash();
            return "Redis Init OK";
        }
    }
}
