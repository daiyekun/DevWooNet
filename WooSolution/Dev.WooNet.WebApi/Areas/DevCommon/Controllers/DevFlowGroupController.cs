using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model;
using Dev.WooNet.Model.Enums;
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


/// <summary>
/// 审批组
/// </summary>

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{

    /// <summary>
    /// 审批组
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevFlowGroupController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevFlowGroupService _IDevFlowGroupService;
        public DevFlowGroupController(IMapper iMapper, IDevFlowGroupService iDevFlowGroupService)
        {
            _IMapper = iMapper;
            _IDevFlowGroupService = iDevFlowGroupService;

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
            var pageInfo = new PageInfo<DevFlowGroup>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevFlowGroup>();
            prdAnd = prdAnd.And(a => a.IsDelete != 1);
            var prdOr = PredBuilder.False<DevFlowGroup>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {//小心搜索时如果计算是字符串。如果存在为null情况也需要判断下。不然会报找不到对象。比如IdNo字段问题
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
               

                prdAnd = prdAnd.And(prdOr);
            }

            var pagelist = _IDevFlowGroupService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="roledto">角色信息</param>
        /// <returns></returns>
        [Route("groupsave")]
        [HttpPost]
        public IActionResult FlowGroupSave([FromBody] DevFlowGroupDTO info)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            };
            if (info.Id > 0)
            {//修改
                var existname = _IDevFlowGroupService.GetQueryable(a => a.Name == info.Name && a.Id != info.Id).Any();
              
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";

                }
               
                else
                {
                    var currinfo = _IDevFlowGroupService.Find(info.Id);
                    var saveinfo = _IMapper.Map<DevFlowGroupDTO, DevFlowGroup>(info);
                    saveinfo.Gstate = 0;
                    saveinfo.AddDateTime = currinfo.AddDateTime;
                    saveinfo.AddUserId = currinfo.AddUserId;
                    _IDevFlowGroupService.Update(saveinfo);
                   

                }




            }
            else
            {
                var existname = _IDevFlowGroupService.GetQueryable(a => a.Name == info.Name && a.IsDelete != 1).Any();
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";
                }
                else
                {
                    var savinfo = _IMapper.Map<DevFlowGroup>(info);
                    savinfo.AddDateTime = DateTime.Now;
                    savinfo.AddUserId = userId;
                    savinfo.Gstate = 0;
                    var teminfo = _IDevFlowGroupService.Add(savinfo);
                    
                }


            }



            return new DevResultJson(result);


        }

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("showView")]
        [HttpGet]
        public IActionResult ShowView(int Id)
        {
            return new DevResultJson(new AjaxResult<DevFlowGroupDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevFlowGroupService.GetInfoById(Id)


            });

        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteFlowGroup(string Ids)
        {
            string sqlstr = $"update dev_flow_group set IsDelete=1 where Id in({Ids}) ";
            var reslut = _IDevFlowGroupService.ExecuteSqlCommand(sqlstr);
            return new DevResultJson(reslut);

        }
    }
}
