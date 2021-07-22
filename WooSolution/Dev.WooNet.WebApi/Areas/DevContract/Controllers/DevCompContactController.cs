using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
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


    }
}
