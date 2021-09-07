using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.FlowModel;
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



namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 审批意见相关
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevAppInstOptionController : ControllerBase
    {
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 实例服务
        /// </summary>
        private IDevAppInstService _IAppInstService;
        /// <summary>
        /// 实例节点
        /// </summary>
        private IDevAppInstNodeService _IAppInstNodeService;
        /// <summary>
        /// 意见
        /// </summary>
        private IDevAppInstOpinService _IAppInstOpinService;
        public DevAppInstOptionController(
          IMapper IMapper,
          IDevAppInstService IDevAppInstService
          , IDevAppInstNodeService IDevAppInstNodeService
          , IDevAppInstOpinService IDevAppInstOpinService)
        {
            _IMapper = IMapper;
            _IAppInstService = IDevAppInstService;
            _IAppInstNodeService = IDevAppInstNodeService;
            _IAppInstOpinService = IDevAppInstOpinService;

        }

        /// <summary>
        /// 同意时提交意见
        /// </summary>
        /// <returns></returns>
        [Route("SubmitAgree")]
        [HttpPost]
        public IActionResult SubmitAgree([FromBody]SubmitOptionInfo optionInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            optionInfo.SubmitUserId = userId;
            _IAppInstOpinService.SubmintOption(optionInfo);
            
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = 0,
               


            });
           
        }

        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <returns></returns>
        [Route("SubmitDisagree")]
        [HttpPost]
        public IActionResult SubmitDisagree([FromBody]SubmitOptionInfo optionInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            optionInfo.SubmitUserId = userId;
            _IAppInstOpinService.SubmintDisagreeOption(optionInfo);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = 0,
            });
        }
        /// <summary>
        /// 审批意见列表
        /// </summary>
        /// <param name="instId">审批实例ID</param>
        /// <param name="nodestrId">节点ID</param>
        /// <returns></returns>
        [Route("GetWorkFlowOption")]
        [HttpPost]
        public IActionResult GetWorkFlowOption([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevAppInstOpin>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var predicateAnd = PredBuilder.True<DevAppInstOpin>();
           
            predicateAnd = predicateAnd.And(a => a.InstId == pgInfo.otherId);
            if (!string.IsNullOrEmpty(pgInfo.otherstr))
            {
                predicateAnd = predicateAnd.And(a => a.NodeStrId == pgInfo.otherstr);
            }
            var layPage = _IAppInstOpinService.GetOptinionList(pageInfo, predicateAnd, a => a.Id, true);
            return new DevResultJson(layPage);
        }

    }
}
