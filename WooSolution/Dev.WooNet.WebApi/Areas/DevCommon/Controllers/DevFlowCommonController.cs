using Dev.WooNet.IWooService;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{

    /// <summary>
    /// 流程一些公用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevFlowCommonController : ControllerBase
    {
        private IDevAppInstNodeService _IDevAppInstNodeService;
        public DevFlowCommonController(IDevAppInstNodeService iDevAppInstNodeService)
        {
            _IDevAppInstNodeService = iDevAppInstNodeService;
        }
        /// <summary>
        /// 查看页面查看流程信息
        /// </summary>
        /// <param name="instId">实例Id</param>
        /// <returns></returns>
        [Route("ViewFlowChart")]
        [HttpGet]
        public IActionResult ViewFlowChart(int instId)
        {
            var data = _IDevAppInstNodeService.LoadFlowChart(instId);
            return new DevResultJson(data);

        }
    }
}
