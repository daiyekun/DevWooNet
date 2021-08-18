using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{

    /// <summary>
    /// 系统一些基础数数据
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevBaseDataController : ControllerBase
    {

        private IMapper _IMapper;
        private IDevDatadicService _IDevDatadicService;
        private IDevDepartmentService _IDevDepartmentService;
        public DevBaseDataController( IMapper iMapper
            , IDevDatadicService iDevDatadicService, IDevDepartmentService iDevDepartmentService)
        {
            
            _IMapper = iMapper;
            _IDevDatadicService = iDevDatadicService;
            _IDevDepartmentService = iDevDepartmentService;
        }
        /// <summary>
        /// 审批事项
        /// </summary>
        /// <returns></returns>

        [Route("GetFlowItems")]
        [HttpGet]
        public IActionResult GetFlowItems(int objEnum)
        {

            var result = new AjaxResult<IList<SelectMultiple>>()
            {
                msg = "",
                code = 0,


            };
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            IList<SelectMultiple> flowItems = new List<SelectMultiple>();
            foreach (var item in list)
            {
                SelectMultiple flow = new SelectMultiple();
                flow.Name = item.Desc;
                flow.Value = item.Value;
                flowItems.Add(flow);
            }
            result.data = flowItems;
            return new DevResultJson(result, a => a.ToLower());

        }
        /// <summary>
        /// 获取类别选择树
        /// </summary>
        /// <returns></returns>
        [Route("GetFlowClassTree")]
        [HttpGet]
        public IActionResult GetFlowClassTree(int objEnum)
        {
            var result = new AjaxResult<IList<SelectMulTreeInfo>>()
            {
                msg = "",
                code = 0,
                data = GetTreeListContTxtClass(objEnum)

            };
            return new DevResultJson(result, a => a.ToLower());
        }
        /// <summary>
        /// 获取流选择树
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetFlowDeptTree")]
        [HttpGet]
        public IActionResult GetFlowDeptTree()
        {
            var result = new AjaxResult<IList<SelectMulTreeInfo>>()
            {
                msg = "",
                code = 0,
                data = GetGetTreeDept()

            };
            return new DevResultJson(result, a => a.ToLower());
        }


        #region  递归部门多选Tree
        /// <summary>
        /// Tree 递归
        /// </summary>
        /// <returns></returns>
        private IList<SelectMulTreeInfo> GetGetTreeDept()
        {
            IList<SelectMulTreeInfo> listTree = new List<SelectMulTreeInfo>();
            var listAll = _IDevDepartmentService.GetAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
            {
                SelectMulTreeInfo treeInfo = new SelectMulTreeInfo();
                treeInfo.Value = item.Id;
                treeInfo.Name = item.Name;

                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        private void RecursionChrenNode(IList<DevDepartmentDTO> listDepts, SelectMulTreeInfo treeInfo, DevDepartmentDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<SelectMulTreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    SelectMulTreeInfo treeInfotmp = new SelectMulTreeInfo();
                    treeInfotmp.Value = chrenItem.Id;
                    treeInfotmp.Name = chrenItem.Name;


                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.Children = listchrennode;

            }




        }
        #endregion


        #region  递归合同类别多选Tree
        /// <summary>
        /// Tree 递归
        /// </summary>
        /// <returns></returns>
        private IList<SelectMulTreeInfo> GetTreeListContTxtClass(int objEnum)
        {
            IList<SelectMulTreeInfo> listTree = new List<SelectMulTreeInfo>();
            var listAll = _IDevDatadicService.GetQueryable(a => a.TypeInt == objEnum).ToList();

            foreach (var item in listAll.Where(a => a.Pid == 0))
            {
                SelectMulTreeInfo treeInfo = new SelectMulTreeInfo();
                treeInfo.Value = item.Id;
                treeInfo.Name = item.Name;

                RecursionChrenNodeContTxtClass(listAll, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        private void RecursionChrenNodeContTxtClass(IList<DevDatadic> listDepts, SelectMulTreeInfo treeInfo, DevDatadic item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<SelectMulTreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    SelectMulTreeInfo treeInfotmp = new SelectMulTreeInfo();
                    treeInfotmp.Value = chrenItem.Id;
                    treeInfotmp.Name = chrenItem.Name;


                    RecursionChrenNodeContTxtClass(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.Children = listchrennode;

            }




        }
        #endregion
    }
}
