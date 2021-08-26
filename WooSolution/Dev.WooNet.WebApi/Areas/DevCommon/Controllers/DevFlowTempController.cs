using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.DevDTO.DevFlow;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebAPI.Areas.DevCommon.Data;
using Dev.WooNet.WebCore.Extend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// 流程模板相关设置
/// </summary>

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevFlowTempController : ControllerBase
    {
        private IDevFlowTempService _IDevFlowTempService;
        private IMapper _IMapper;
        private IDevDatadicService _IDevDatadicService;
        private IDevDepartmentService _IDevDepartmentService;
        private IDevFlowTempNodeService _IDevFlowTempNodeService;
        private IDevFlowTempNodeInfoService _IDevFlowTempNodeInfoService;
        public DevFlowTempController(IDevFlowTempService iDevFlowTempService, IMapper iMapper
            , IDevDatadicService iDevDatadicService, IDevDepartmentService iDevDepartmentService
            , IDevFlowTempNodeService iDevFlowTempNodeService
            , IDevFlowTempNodeInfoService iDevFlowTempNodeInfoService)
        {
            _IDevFlowTempService = iDevFlowTempService;
            _IMapper = iMapper;
            _IDevDatadicService = iDevDatadicService;
            _IDevDepartmentService = iDevDepartmentService;
            _IDevFlowTempNodeService = iDevFlowTempNodeService;
            _IDevFlowTempNodeInfoService = iDevFlowTempNodeInfoService;
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
            var pageInfo = new PageInfo<DevFlowTemp>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevFlowTemp>();
            prdAnd = prdAnd.And(a => a.IsDelete != 1);
            var prdOr = PredBuilder.False<DevFlowTemp>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {//小心搜索时如果计算是字符串。如果存在为null情况也需要判断下。不然会报找不到对象。比如IdNo字段问题
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
              
                prdAnd = prdAnd.And(prdOr);
            }

            var pagelist = _IDevFlowTempService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 显示页面信息-主要用于修改和查看
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("showView")]
        [HttpGet]
        public IActionResult ShowView(int Id)
        {
            return new DevResultJson(new AjaxResult<DevFlowTempDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevFlowTempService.ShowView(Id)


            });

        }

        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("Save")]
        [HttpPost]
        public IActionResult FlowTempSave([FromBody] DevFlowTempDTO info)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            };
            if (info.Id > 0)
            {//修改
                var existname = _IDevFlowTempService.GetQueryable(a => a.Name == info.Name && a.Id != info.Id).Any();
              
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";

                }
               
                else
                {
                    var currinfo = _IDevFlowTempService.Find(info.Id);
                    var vis = currinfo.Version;
                    var saveinfo = _IMapper.Map<DevFlowTempDTO, DevFlowTemp>(info, currinfo);
                    var saveinfohist = _IMapper.Map<DevFlowTempDTO, DevFlowTempHist>(info);
                    // saveinfo.IsValid = 0;
                    saveinfo.AddDateTime = currinfo.AddDateTime;
                    saveinfo.AddUserId = currinfo.AddUserId;
                    saveinfo.Version = vis + 1;
                    saveinfohist.Version = vis;
                    saveinfohist.AddDateTime = DateTime.Now;
                    saveinfohist.AddUserId = userId;
                   _IDevFlowTempService.UpdateSave(saveinfo, saveinfohist);
                   

                }




            }
            else
            {
                var existname = _IDevFlowTempService.GetQueryable(a => a.Name == info.Name && a.IsDelete != 1).Any();
              
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";
                }
               
                else
                {
                     var savinfo = _IMapper.Map<DevFlowTemp>(info);
                    var saveinfohist = _IMapper.Map<DevFlowTempHist>(info);
                    savinfo.AddDateTime = DateTime.Now;
                    savinfo.AddDateTime = DateTime.Now;
                    savinfo.AddUserId = userId;
                    savinfo.AddUserId = userId;
                    savinfo.Version = 1;
                    var teminfo = _IDevFlowTempService.AddSave(savinfo, saveinfohist);
                    
                }


            }



            return new DevResultJson(result);


        }

        /// <summary>
        /// 获取流程对象集合
        /// </summary>
        /// <returns></returns>
        [Route("getobjtypes")]
        [HttpGet]
        public IActionResult GetWfObjTypes()
        {
            return new DevResultJson(new AjaxResult<IList<EnumItemAttribute>>()
            {
                msg = "ok",
                code = 0,
                data = EmunUtility.GetAttr(typeof(FlowObjEnums))

            });
        }

        /// <summary>
        /// 流程节点加载
        /// </summary>
        /// <param name="TempId">模板ID</param>
        /// <returns></returns>
        [Route("TempFlowNodeLoad")]
        [HttpGet]
        public IActionResult TempFlowNodeLoad(int TempId)
        {
            var data = _IDevFlowTempNodeService.LoadNodes(TempId);
            return new DevResultJson(data);
        }

        /// <summary>
        /// 流程节点加载
        /// </summary>
        /// <param name="submitWfRes">请求对象</param>
        /// <returns></returns>
        //[Route("SubmitFlowNodeLoad")]
        [HttpGet]
        public IActionResult SubmitFlowNodeLoad(SubmitWfRequest submitWfRes)
        {
            var data = _IDevFlowTempNodeService.LoadNodes(submitWfRes);
            return new DevResultJson(data);
        }
        /// <summary>
        /// 加载节点
        /// </summary>
        /// <param name="submitWfRes"></param>
        /// <returns></returns>
        [Route("SubmitFlowNodeLoad")]
        [HttpGet]
        public IActionResult SubmitFlowNodeLoad(int? TempId, decimal? Amount)
        {
            SubmitWfRequest submitWfRes = new SubmitWfRequest();
            submitWfRes.Amount = Amount ?? 0;
            submitWfRes.TempId = TempId ?? 0;
            var data = _IDevFlowTempNodeService.LoadNodes(submitWfRes);
            return new DevResultJson(data);
        }

        /// <summary>
        /// 根据节点ID查询节点信息
        /// </summary>
        /// <returns></returns>
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
        /// 保存节点
        /// </summary>
        /// <param name="flowNodeData">节点json字符串</param>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>


        [Route("SaveNode")]
        [HttpPost]
        public IActionResult SaveNode([FromBody] ReqSaveNodeInfo reqSaveNode)
        {

            var nodeData = NodeJsonDataUtility.DeserializeToInfo(reqSaveNode.FlowNodeData);
            _IDevFlowTempNodeService.AddFlowNodes(nodeData, reqSaveNode.TempId);
            return new DevResultJson(new AjaxResult()
            {
                msg = "保存成功",
                code = 0,


            });
        }

        /// <summary>
        /// 保存节点信息
        /// 选择节点组的时候调用
        /// </summary>
        /// <returns></returns>
        [Route("SaveNodeInfo")]
        [HttpPost]
        public IActionResult SaveNodeInfo([FromBody]FlowTempNodeInfoDTO tempNodeInfoDTO)
        {

            var saveInfo = _IMapper.Map<DevFlowTempNodeInfo>(tempNodeInfoDTO);
            _IDevFlowTempNodeInfoService.SaveFlowTempNodeInfo(saveInfo);
            return new DevResultJson(new AjaxResult()
            {
                msg = "保存成功",
                code = 0,
            });
        }

        /// <summary>
        /// 新建流程图清除当前节点所有数据
        /// </summary>
        /// <returns></returns>
        [Route("ClearNodeData")]
        [HttpGet]
        public IActionResult ClearNodeData(int tempId)
        {
            _IDevFlowTempNodeService.ClearFlowNodes(tempId);
            return new DevResultJson(new AjaxResult()
            {
                msg = "保存成功",
                code = 0,


            });
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        [Route("UpdateField")]
        [HttpPost]
        public IActionResult UpdateField([FromBody]UpdateFieldInfo info)
        {
            var res = _IDevFlowTempService.UpdateField(info);
            AjaxResult reqInfo = reqInfo = new AjaxResult()
            {
                msg = "修改成功",
                code = 0,
            };
            if (res <= 0)
            {
                reqInfo.msg = "修改失败";
            }

            return new DevResultJson(reqInfo);
        }





    }
}
