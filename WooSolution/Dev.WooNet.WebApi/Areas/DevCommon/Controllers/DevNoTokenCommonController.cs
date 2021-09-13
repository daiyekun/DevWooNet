using AutoMapper;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
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
    /// 没有传递token访问的类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevNoTokenCommonController : ControllerBase
    {
        private IDevDepartmentService _IDevDepartmentService;
        private IMapper _IMapper { get; set; }
        private IDevRolePessionService _IDevRolePessionService;
        public DevNoTokenCommonController(IMapper iMapper, IDevDepartmentService iDevDepartmentService
            , IDevRolePessionService iDevRolePessionService)
        {
            _IDevDepartmentService = iDevDepartmentService;
            _IMapper = iMapper;
            _IDevRolePessionService = iDevRolePessionService;
        }

        /// <summary>
        /// 根据角色、模块、分配对象类型查询所拥有的部门
        /// 如果选择部门时实现跨部门管理
        /// </summary>
        /// <param name="funcId">功能ID</param>
        /// <param name="Id">角色、用户、岗位ID</param>
        /// <param name="setType">1、角色、0用户，目前只实现角色</param>
        /// <returns></returns>
        [Route("getpessionxtree")]
        [HttpGet]
        public IActionResult GetPessionXTree(int? funcId, int? Id, int? setType)
        {
            IList<XTree> xTrees = new List<XTree>();
            if (setType == 1)
            {//角色
                var predicateAnd = PredBuilder.True<DevRolePession>();
                predicateAnd = predicateAnd.And(a => a.FuncId == funcId && a.RoleId == Id);
                var deptIds = _IDevRolePessionService.GetQueryable(predicateAnd).Select(a => a.DeptIds);
                var listdeptIds = StringHelper.String2ArrayInt(string.Join(',', deptIds));
                xTrees = _IDevDepartmentService.GetXtTree(listdeptIds);

            }

            return new DevResultJson(xTrees, StringHelper.RepleaceStr);
        }

    }
}
