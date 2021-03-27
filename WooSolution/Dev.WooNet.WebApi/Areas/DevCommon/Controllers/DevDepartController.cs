using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// 部门控制器
/// </summary>
namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevDepartController : ControllerBase
    {
        private IDevDepartmentService _IDevDepartmentService;
        public DevDepartController(IDevDepartmentService iDevDepartmentService)
        {
            _IDevDepartmentService = iDevDepartmentService;
        }
        
        [Route("list")]
        [HttpPost]
        public IActionResult GetListDepart([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevDepartment>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevDepartment>();
            var prdOr = PredBuilder.False<DevDepartment>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.Code.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            var pagelist = _IDevDepartmentService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="deptInfo">部门对象</param>
        /// <param name="deptMain"></param>
        /// <returns></returns>
        [Route("departSave")]
        [HttpPost]
        public IActionResult AddSave([FromBody] DepartData deptdata )
        {
            //[FromBody] DevDeptmain deptMain
            var list = _IDevDepartmentService.GetAll();
            deptdata.DeptInfo.IsMain = deptdata.DeptInfo.IsMain == null ? 0 : deptdata.DeptInfo.IsMain;
            deptdata.DeptInfo.IsCompany = deptdata.DeptInfo.IsCompany == null ? 0 : deptdata.DeptInfo.IsCompany;
            GetDeptPathInfo(deptdata.DeptInfo, list);
            _IDevDepartmentService.SaveDeptInfo(deptdata.DeptInfo, deptdata.DeptMain);
            return DeptSubmitSave(deptdata.DeptInfo, deptdata.DeptMain);
          
        }
        /// <summary>
        /// 生成树目录
        /// </summary>
        /// <param name="deptInfo"></param>
        /// <param name="list"></param>
        private static void GetDeptPathInfo(DevDepartment deptInfo, IList<DevDepartmentDTO> list)
        {

            if (deptInfo.Pid == 0)
            {
                deptInfo.Leaf = 1;
                deptInfo.Dpath = "0" + (list.Where(a => a.Pid == 0).Count() + 1);
            }
            else
            {
                var partInfo = list.Where(c => c.Id == deptInfo.Pid).FirstOrDefault();
                if (partInfo != null)
                {
                    deptInfo.Leaf = partInfo.Leaf + 1;
                    deptInfo.Dpath = partInfo.Dpath + "/0" + (list.Where(a => a.Pid == partInfo.Id).Count() + 1);
                }

            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="deptInfo">部门</param>
        /// <param name="deptMain">签约主体特有信息</param>
        /// <returns></returns>
        private IActionResult DeptSubmitSave(DevDepartment deptInfo, DevDeptmain deptMain)
        {
            _IDevDepartmentService.SaveDeptInfo(deptInfo, deptMain);
            _IDevDepartmentService.SetRedisHash();
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = 0,


            });
        }
        /// <summary>
        /// 显示页面信息-主要用于修改和查看
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("ShowValues")]
        [HttpGet("{Id}")]
        public IActionResult ShowValues(int Id)
        {
            return new DevResultJson(new AjaxResult<DevDepartmentDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevDepartmentService.ShowValues(Id)


            });

        }

        /// <summary>
        /// 获取部门选择树
        /// </summary>
        /// <returns></returns>
        [Route("GetTree")]
        [HttpGet]
        public IActionResult GetTree()
        {

            return new DevResultJson(_IDevDepartmentService.GetTreeSelect(), (v) => { return v.Replace("Checked", "checked"); });



        }

    }
}
