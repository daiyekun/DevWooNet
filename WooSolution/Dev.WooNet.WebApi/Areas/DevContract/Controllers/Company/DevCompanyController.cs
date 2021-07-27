using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
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
/// 合同对方 操作
/// </summary>

namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers
{
    /// <summary>
    /// 合同对方
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevCompanyController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompanyService _IDevCompanyService;
        public DevCompanyController(IMapper iMapper, IDevCompanyService iDevCompanyService)
        {
            _IMapper = iMapper;
            _IDevCompanyService = iDevCompanyService;

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
            var pageInfo = new PageInfo<DevCompany>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevCompany>();
            prdAnd = prdAnd.And(a => a.IsDelete != 1);
            var prdOr = PredBuilder.False<DevCompany>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {//小心搜索时如果计算是字符串。如果存在为null情况也需要判断下。不然会报找不到对象。比如IdNo字段问题
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.Code.Contains(pgInfo.kword));
              
                prdAnd = prdAnd.And(prdOr);
            }
            
            var pagelist = _IDevCompanyService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }

        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("companySave")]
        [HttpPost]
        public IActionResult CompanySave([FromBody] DevCompanyDTO info)
        {   
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            };
            if (info.Id > 0)
            {//修改
                var existname = _IDevCompanyService.GetQueryable(a => a.Name == info.Name && a.Id != info.Id).Any();
                var existno = _IDevCompanyService.GetQueryable(a => a.Code == info.Code&&a.Id!=info.Id).Any();
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";

                }
                else if (existno)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前编号已经存在";
                }
                else
                {
                    var currinfo = _IDevCompanyService.Find(info.Id);
                    var saveinfo = _IMapper.Map<DevCompanyDTO, DevCompany>(info);
                    _IDevCompanyService.Update(saveinfo);
                    _IDevCompanyService.UpdateItems(saveinfo.Id, userId);

                }
               

              

            }
            else
            {
                var existname = _IDevCompanyService.GetQueryable(a => a.Name == info.Name).Any();
                var existno = _IDevCompanyService.GetQueryable(a => a.Code == info.Code).Any();
                if (existname)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前名称已经存在";
                }
                else if (existno)
                {
                    result.code = (int)MessageEnums.IsExist;
                    result.msg = "当前编号已经存在";
                }
                else
                {
                    var savinfo = _IMapper.Map<DevCompany>(info);
                    savinfo.AddDateTime = DateTime.Now;
                    savinfo.UpdateDateTime = DateTime.Now;
                    savinfo.UpdateUserId = userId;
                    savinfo.AddUserId = userId;
                    savinfo.Dstatus = 0;
                    var teminfo = _IDevCompanyService.Add(savinfo);
                    _IDevCompanyService.UpdateItems(teminfo.Id, userId);
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
            return new DevResultJson(new AjaxResult<DevCompanyDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevCompanyService.GetInfoById(Id)

                
            });

        }

        /// <summary>
        /// 清除垃圾数据
        /// </summary>
        /// <returns></returns>
        [Route("cleardata")]
        [HttpGet]
        public IActionResult ClearData()
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            _IDevCompanyService.ClearItemData(userId);
            return new DevResultJson(new AjaxResult()
            {
                msg = "",
                code = 0
                


            });

        }



    }
}
