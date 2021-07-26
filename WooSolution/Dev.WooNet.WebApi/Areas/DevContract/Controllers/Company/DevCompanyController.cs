﻿using AutoMapper;
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
            if (info.Id > 0)
            {//修改
                var currinfo = _IDevCompanyService.Find(info.Id);
                var saveinfo = _IMapper.Map<DevCompanyDTO, DevCompany>(info);
                _IDevCompanyService.Update(saveinfo);

            }
            else
            {
                var savinfo = _IMapper.Map<DevCompany>(info);
                savinfo.AddDateTime = DateTime.Now;
                savinfo.UpdateDateTime = DateTime.Now;
                savinfo.UpdateUserId = userId;
                savinfo.UpdateUserId= userId;
                _IDevCompanyService.Add(savinfo);

            }
            
            
            
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


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



    }
}
