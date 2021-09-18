using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.WebCore.Extend;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// 合同系统模块权限操作
/// </summary>
namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevPermissionController : ControllerBase
    {
        private readonly IDevRolePessionService _IDevRolePessionService;
        public DevPermissionController(IDevRolePessionService iDevRolePessionService)
        {
            _IDevRolePessionService = iDevRolePessionService;
        }

        /// <summary>
        /// 新增数据权限
        /// </summary>
        /// <param name="funcode">权限code 代码</param>
        /// <returns></returns>
        [Route("addpermission")]
        [HttpGet]
        public IActionResult AddPermission(string funcode)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var departId = HttpContext.User.Claims.GetTokenDeptId();
            var res = false;
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            switch (funcode)
            {
                case "CustomerAdd"://客户新建
                case "SupplierAdd"://供应商
                case "OtherPartyAdd"://其他对方
                    res = _IDevRolePessionService.GetCompanyAddPermission(funcode, userId);
                    
                    break;

            }


            if (!res)
            {
                result.msg = "无权限";
                result.Tag = -1;
            }
            return new DevResultJson(result);

        }

        /// <summary>
        /// 删除数据权限
        /// </summary>
        /// <param name="funcode">权限code 代码</param>
        /// <param name="ids">删除的数据Id</param>
        /// <returns></returns>
        [Route("delpermission")]
        [HttpGet]
        public IActionResult DelPermission(string funcode,string ids)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var departId = HttpContext.User.Claims.GetTokenDeptId();
            var listIds = StringHelper.String2ArrayInt(ids);
            PermissionDataInfo res = null;
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            switch (funcode)
            {
                case "CustomerDelete"://删除客户
                case "SupplierDelete"://删除供应商
                case "OtherPartyDelete"://删除其他对方
                    res = _IDevRolePessionService.GetCompanyDeletePermission(funcode, userId, departId, listIds);

                    break;

            }


            if (res.Code!=0)
            {
                result.msg = $"存在数据没有权限删除！数据：{StringHelper.ArrayString2String(res.noteAllow)}";
                result.Tag = -1;
            }
            return new DevResultJson(result);

        }

        /// <summary>
        /// 修改数据权限
        /// </summary>
        /// <param name="funcode">权限code 代码</param>
        /// <param name="Id">修改数据ID</param>
        /// <returns></returns>
        [Route("updatepermission")]
        [HttpGet]
        public IActionResult UpdatePermission(string funcode, int Id)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var departId = HttpContext.User.Claims.GetTokenDeptId();

            PermissionDicEnum res = PermissionDicEnum.OK;
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            switch (funcode)
            {
                case "CustomerUpdate"://修改客户
                case "SupplierUpdate"://修改供应商
                case "OtherPartyUpdate"://其他对方
                    res = _IDevRolePessionService.GetCompanyUpdatePermission(funcode, userId, departId, Id);

                    break;

            }
            if (res != PermissionDicEnum.OK)
            {
                result.msg = $"无权限或者当前状态不允许修改！！！";
                result.Tag = -1;
            }
            return new DevResultJson(result);

        }

        /// <summary>
        /// 查看详情数据权限
        /// </summary>
        /// <param name="funcode">权限code 代码</param>
        /// <param name="Id">修改数据ID</param>
        /// <returns></returns>
        [Route("viewpermission")]
        [HttpGet]
        public IActionResult ViewPermission(string funcode, int Id)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var departId = HttpContext.User.Claims.GetTokenDeptId();

            var  res = false;
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            switch (funcode)
            {
                case "CustomerView"://客户
                case "SupplierView"://供应商
                case "OtherPartyView"://其他对方
                    res = _IDevRolePessionService.GetCompanyDetailPermission(funcode, userId, departId, Id);

                    break;

            }
            if (!res)
            {
                result.msg = $"无权限";
                result.Tag = -1;
            }
            return new DevResultJson(result);

        }
        /// <summary>
        /// 次要字段编辑
        /// </summary>
        /// <param name="funcode">权限code 代码</param>
        /// <param name="Id">修改数据ID</param>
        /// <returns></returns>
        [Route("SecFieldPermission")]
        [HttpGet]
        public IActionResult SecFieldPermission(string funcode, int Id)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var departId = HttpContext.User.Claims.GetTokenDeptId();

            PermissionDicEnum res = PermissionDicEnum.OK;
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            switch (funcode)
            {
                case "CustomerSecondaryField"://客户
                case "SupplierSecondaryField"://供应商
                case "OtherPartySecondaryField"://其他对方
                    res = _IDevRolePessionService.GetCompanySecFieldUpdatePermission(funcode, userId, departId, Id);

                    break;

            }
            if (res!= PermissionDicEnum.OK)
            {
                result.msg = $"无权限";
                result.Tag = -1;
            }
            return new DevResultJson(result);

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("UpdateField")]
        [HttpPost]
        public IActionResult UpdateField()
        {
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,
            };
            return new DevResultJson(result);
        }


    }
}
