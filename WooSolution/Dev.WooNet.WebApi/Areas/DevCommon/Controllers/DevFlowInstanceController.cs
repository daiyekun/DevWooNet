using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model;
using Dev.WooNet.Model.FlowModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebAPI.Models;
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
/// 审批实例相关
/// </summary>
namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevFlowInstanceController : ControllerBase
    {
        private IDevFlowTempService _IDevFlowTempService;
        private IDevFlowTempNodeService _IDevFlowTempNodeService;
        private IDevFlowTempNodeInfoService _IDevFlowTempNodeInfoService;
        private IMapper _IMapper;
        private IDevAppInstService _IDevAppInstService;
        private IDevAppInstNodeService _IDevAppInstNodeService;
        public DevFlowInstanceController(IDevFlowTempService iDevFlowTempService
            , IDevFlowTempNodeService iDevFlowTempNodeService
            , IDevFlowTempNodeInfoService iDevFlowTempNodeInfoService
            , IMapper iMapper
            , IDevAppInstService iDevAppInstService
            , IDevAppInstNodeService iDevAppInstNodeService)
        {
            _IDevFlowTempService = iDevFlowTempService;
            _IDevFlowTempNodeService = iDevFlowTempNodeService;
            _IDevFlowTempNodeInfoService = iDevFlowTempNodeInfoService;
            _IMapper = iMapper;
            _IDevAppInstService = iDevAppInstService;
            _IDevAppInstNodeService = iDevAppInstNodeService;

        }
        /// <summary>
        /// 根据条件获取流程模板对象
        /// </summary>
        /// <returns></returns>
        [Route("getflowinfo")]
        [HttpPost]
        public IActionResult GetFlowInfo([FromBody]GetTempFlowRequestData requestTemp)
        {
            var userdeptId = HttpContext.User.Claims.GetTokenDeptId();
            if (requestTemp.DeptId == -1)
            {
                requestTemp.DeptId = userdeptId;
            }
           
            return new DevResultJson(new AjaxResult<ResponTempData>()
            {
                msg = "",
                code = 0,
                data = _IDevFlowTempService.GetFlowInfoByWhere(requestTemp)
               

            });
        }

        /// <summary>
        /// 流程节点加载
        /// </summary>
        /// <param name="submitWfRes">请求对象</param>
        /// <returns></returns>
        /// 
        
        public IActionResult SubmitFlowNodeLoad(SubmitWfRequest submitWfRes)
        {
            var data = _IDevFlowTempNodeService.LoadNodes(submitWfRes);
            return new DevResultJson(data);
        }
        

        /// <summary>
        /// 根据节点ID查询节点信息
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetNodeInfoView")]
        [HttpGet]
        public IActionResult GetNodeInfoView(string nodeStr, int tempId)
        {
            var info = _IDevFlowTempNodeInfoService.GetNodeInfoByStrId(nodeStr, tempId);
            if (info == null)
            {
                info = new FlowTempNodeInfoViewDTO()
                {
                    Id = 0,
                    NodeStrId = nodeStr,
                    TempId = tempId,
                    UserNames = "",
                    GroupName = ""

                };
            }
            return new DevResultJson(new AjaxResult<FlowTempNodeInfoViewDTO>()
            {
                msg = "",
                code = 0,
                data = info


            });
        }

        /// <summary>
        /// 判断流程
        /// </summary>
        /// <param name="requestCheckFlow">校验流程模板对象</param>
        /// <returns></returns>

        [Route("CheckSubmitFlow")]
        [HttpPost]
        public IActionResult ChekSubmitFlowData([FromBody]RequestCheckFlow requestCheckFlow)
        {
            var res = _IDevFlowTempService.SubmitCheckFlow(requestCheckFlow);

            return new DevResultJson(new AjaxResult()
            {
                msg = "",
                code = 0,
                OtherValue = res

            });
        }

        /// <summary>
        /// 保存审批实例
        /// </summary>
        /// <param name="appInstdto">审批实例对象</param>
        /// <returns></returns>
        [Route("SubmitWorkFlow")]
        [HttpPost]
        public async Task<IActionResult> SubmitWorkFlow([FromBody]SubDevAppInst appInstdto)
        {
            DevAppInst instInfo = null;

            var userId = HttpContext.User.Claims.GetTokenUserId();
            var saveInfo = _IMapper.Map<DevAppInst>(appInstdto);
            saveInfo.StartUserId = userId;
            saveInfo.StartDateTime = DateTime.Now;
            saveInfo.AddUserId = userId;
            saveInfo.AddDateTime = DateTime.Now;
            saveInfo.AppState = 0;
            instInfo = _IDevAppInstService.SubmitWorkFlow(saveInfo);
            
            await Task.Factory.StartNew(() =>
            {

                _IDevAppInstService.SubmitWfUpdateObjWfInfo(instInfo);
            });



            return new DevResultJson(new AjaxResult()
            {
                msg = "操作成功",
                code = 0,
                OtherValue = 0

            });
        }

        /// <summary>
        /// 审批历史
        /// </summary>
        /// <returns></returns>
        [Route("GetAppHistList")]
        [HttpGet]
        public IActionResult GetAppHistList(int appObjId, int objType)
        {
            var pageInfo = new NoPageInfo<DevAppInst>();
            var predicateAnd = PredBuilder.True<DevAppInst>();
            var userId = HttpContext.User.Claims.GetTokenUserId();
            predicateAnd = predicateAnd.And(a => a.ObjType == objType && a.AppObjId == appObjId);
            var layPage = _IDevAppInstService.GetAppHistList(pageInfo, userId, predicateAnd, a => a.Id, true);
            return new DevResultJson(layPage);
        }

        


        /// <summary>
        /// 查看流程图时根据节点ID查询节点信息
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("ShowNodeInfoView")]
        [HttpGet]
        public IActionResult ShowNodeInfoView(string nodeStr, int instId)
        {
            var info = _IDevAppInstNodeService.GetNodeInfoByStrId(nodeStr, instId);
            if (info == null)
            {
                info = new AppInstNodeInfoViewDTO()
                {
                    Id = 0,
                    NodeStrId = nodeStr,
                    InstId = instId,
                    UserNames = "",
                    GroupName = ""

                };
            }
            return new DevResultJson(new AjaxResult<AppInstNodeInfoViewDTO>()
            {
                msg = "",
                code = 0,
                data = info


            });
        }
        [Route("getAppFlowInfo")]
        [HttpPost]
        public IActionResult GetAppFlowInfo([FromBody] ReqFlowInfoData reqFlowInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            reqFlowInfo.CurrUserId = userId;
            var reqdata = _IDevAppInstService.GetAppFlowInceInfo(reqFlowInfo);
            return new DevResultJson(new AjaxResult<AppFlowInceInfo>()
            {
                msg = "",
                code = 0,
                data = reqdata


            });
            
        }

    }
}
