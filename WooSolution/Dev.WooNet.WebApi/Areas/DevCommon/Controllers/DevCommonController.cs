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
        private IDevDepartmentService _DevDepartmentService;
        private IDevCountryService _IDevCountryService;

        public DevCommonController(IDevDatadicService iDevDatadicService,
            IDevUserinfoService iDevUserinfoService
            ,IDevDepartmentService iDevDepartmentService
            , IDevCountryService iDevCountryService)
        {
            _IDevDatadicService = iDevDatadicService;
            _IDevUserinfoService = iDevUserinfoService;
            _DevDepartmentService = iDevDepartmentService;
            _IDevCountryService = iDevCountryService;
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
            _DevDepartmentService.SetRedisHash();
            _IDevCountryService.SetHashAddress();
            return "Redis Init OK";
        }
    }
}
