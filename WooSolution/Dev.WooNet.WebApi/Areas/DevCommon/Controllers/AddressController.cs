using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.WebCore.FilterExtend;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 国家地址级联查询
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
   // [TokenSessionActionFilter]
    public class AddressController : ControllerBase
    {
        private IDevCountryService _IDevCountryService;
        public AddressController(IDevCountryService iDevCountryService)
        {
            _IDevCountryService = iDevCountryService;
        }

        [Route("address")]
        [HttpGet]
        /// <summary>
        /// 返回3级联动数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAddress()
        {

            if (RedisUtility.KeyExists(RedisKeys.redisAddressKey))
            {
                return Content(RedisUtility.StringGet(RedisKeys.redisAddressKey), "application/json");
            }
            else
            {
                var list = _IDevCountryService.GetAddress();
                var strdata = JsonUtility.SerializeObject(list).ToLower();
                RedisUtility.StringSetAsync(RedisKeys.redisAddressKey, strdata);
                return Content(strdata, "application/json");
            }
        }
    }
}
