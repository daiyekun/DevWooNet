using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevSysModelController : ControllerBase
    {
        private IDevSysmodelService _IDevSysmodelService;
        private IMapper _IMapper;
        private IHttpContextAccessor _IHttpContextAccessor;
        public DevSysModelController(IDevSysmodelService iDevSysmodelService
            , IMapper iMapper
            , IHttpContextAccessor iHttpContextAccessor)
        {
            _IDevSysmodelService = iDevSysmodelService;
            _IMapper = iMapper;
            _IHttpContextAccessor = iHttpContextAccessor;
        }

       
        [Route("gettreetable")]
        [HttpGet]
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeTable()
        {
            var list = _IDevSysmodelService.GetListAll().Where(a => a.IsDelete != 1).OrderBy(a => a.Sort).ToList();
            var laypage = new AjaxListResult<DevSysmodelDTO>()
            {
                code = 0,
                msg = "ok",
                data = list

            };
            return new DevResultJson(laypage);
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [Route("GetSelectTree")]
        [HttpGet]
        public IActionResult GetSelectTree()
        {
            return new DevResultJson(_IDevSysmodelService.GetModelTreeSelect(), (v) => { return v.Replace("Checked", "checked"); });
        }
        /// <summary>
        /// 用户新增修改
        /// </summary>
        /// <param name="userdto">用户信息</param>
        /// <returns></returns>
        [Route("Save")]
        [HttpPost]
        public IActionResult SysModelSave([FromBody] DevSysmodelDTO savedata)
        {
            
            var info = _IMapper.Map<DevSysmodel>(savedata);
            info.Code = "";
            info.Mpath = "";
            info.IsDelete = 0;
            info.Leaf = 0;
            info.Id = savedata.id;
            info.Pid= savedata.pid;
            _IDevSysmodelService.SaveData(info);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });
            


        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("delsysmodel")]
        [HttpGet]
        public IActionResult DelSysModel(string Ids)
        {
            _IDevSysmodelService.DelSysModel(Ids);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

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
            return new DevResultJson(new AjaxResult<DevSysmodelDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevSysmodelService.GetSysModelById(Id)


            });

        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">修改字段对象</param>
        /// <returns></returns>
        [Route("updatefield")]
        [HttpPost]
        public IActionResult UpdateField(UpdateField updateField)
        {
            _IDevSysmodelService.UpdateField(updateField);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }
        /// <summary>
        /// 根据角色ID获取菜单集合
        /// </summary>
        /// <returns></returns>
        [Route("getmodelchecks")]
        [HttpGet]

        public IActionResult GetModelChecks(int roleId)
        {
            return new DevResultJson(new AjaxResult<IList<DevModelCheck>>()
            {
                msg = "",
                code = 0,
                data = _IDevSysmodelService.GetModelChecks(roleId)


            });
        }
        /// <summary>
        /// 新增角色模块权限
        /// </summary>
        /// <param name="rolemodel">角色模块</param>
        /// <returns></returns>
        [Route("saverolemodel")]
        [HttpPost]
        public IActionResult Saverolemodel([FromBody] RoleModel rolemodel)
        {

            _IDevSysmodelService.SaveRolemodel(rolemodel);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


        }

        /// <summary>
        /// 首页桌面菜单查询
        /// </summary>
        /// <returns></returns>
        [Route("deskmenus")]
        [HttpGet]
       
        public IActionResult GetDeskmenus()
        {
           // string struse = Newtonsoft.Json.JsonConvert.SerializeObject(HttpContext.User.Claims.Select(c => c.Value));
           
            var claim= HttpContext.User.Claims.Where(a => a.Type == "UserId").FirstOrDefault();
            var userid = 0;
            if (claim != null)
            {
                int.TryParse(claim.Value, out userid);
            }
            return new DevResultJson(new AjaxResult<IList<WinuiMenu>>
            {
                msg = "success",
                code = (int)MessageEnums.success,
                data = _IDevSysmodelService.GetWinDeskMenus(userid)



            });



        }

        /// <summary>
        /// 开始菜单，查询系统菜单为是的
        /// </summary>
        /// <returns></returns>
        [Route("allmenus")]
        [HttpGet]
        public IActionResult GetWindowStartmenus()
        {
            // string struse = Newtonsoft.Json.JsonConvert.SerializeObject(HttpContext.User.Claims.Select(c => c.Value));

            var claim = HttpContext.User.Claims.Where(a => a.Type == "UserId").FirstOrDefault();
            var userid = 0;
            if (claim != null)
            {
                int.TryParse(claim.Value, out userid);
            }
            return new DevResultJson(new AjaxResult<IList<WinuiMenu>>
            {
                msg = "success",
                code = (int)MessageEnums.success,
                data = _IDevSysmodelService.GetWinStartMenus(userid)



            });



        }

    }
}
