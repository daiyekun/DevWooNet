using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.FlowModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{
    /// <summary>
    /// 流程模板设置
    /// </summary>
    public partial class DevFlowTempService
    {

        /// <summary>
        /// 流程模板列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevFlowTempDTO> GetList<s>(PageInfo<DevFlowTemp> pageInfo, Expression<Func<DevFlowTemp, bool>> whereLambda,
             Expression<Func<DevFlowTemp, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevFlowTemp>().AsTracking().Where<DevFlowTemp>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevFlowTemp>))
            { //分页
                tempquery = tempquery.Skip<DevFlowTemp>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevFlowTemp>(pageInfo.PageSize);
            }
            //部门
            var listdepts = DevDb.Set<DevDepartment>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();
            //数据字典
            var listdic = DevDb.Set<DevDatadic>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Version = a.Version,
                            IsValid = a.IsValid,
                            ObjType = a.ObjType,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            DeptIds = a.DeptIds,
                            CategoryIds = a.CategoryIds,
                            FlowItems = a.FlowItems,
                           

                        };
            var local = from a in query.AsEnumerable()
                        select new DevFlowTempDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Version = a.Version,
                            IsValid = a.IsValid,
                            ObjType = a.ObjType,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            DeptIds = a.DeptIds,
                            CategoryIds = a.CategoryIds,
                            FlowItems = a.FlowItems,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId),
                            CateName = GetDataDics(listdic, a.CategoryIds),
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            DeptsName = GetDeptNames(listdepts, a.DeptIds),//部门名称
                            FlowItemsDic = GetFlowItems(a.FlowItems, a.ObjType),//审批事项


                        };
            return new AjaxListResult<DevFlowTempDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 根据IDS获取名称"销售类、采购类"
        /// </summary>
        /// <param name="dataDictionaries">字典集合</param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        private string GetDataDics(IList<DevDatadic> dataDictionaries, string Ids)
        {
            var listids = StringHelper.String2ArrayInt(Ids);
            var listdic = dataDictionaries.Where(a => listids.Contains(a.Id)).Select(a => a.Name).ToList();
            return StringHelper.ArrayString2String(listdic);

        }
        /// <summary>
        /// 根据Ids获取部门
        /// </summary>
        /// <param name="departments">部门集合</param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        private string GetDeptNames(IList<DevDepartment> departments, string Ids)
        {
            var listids = StringHelper.String2ArrayInt(Ids);
            var listdic = departments.Where(a => listids.Contains(a.Id)).Select(a => a.Name).ToList();
            return StringHelper.ArrayString2String(listdic);

        }
        /// <summary>
        /// 获取审批事项
        /// </summary>
        /// <returns></returns>
        private string GetFlowItems(string Ids, int objTypeEnum)
        {
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objTypeEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            var listids = StringHelper.String2ArrayInt(Ids);
            var listDics = list.Where(a => listids.Contains(a.Value)).Select(a => a.Desc).ToList();
            return StringHelper.ArrayString2String(listDics);

        }

        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public DevFlowTempDTO ShowView(int Id)
        {
            //部门
            var listdepts = DevDb.Set<DevDepartment>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();
            //数据字典
            var listdic = DevDb.Set<DevDatadic>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();
            var query = from a in DevDb.Set<DevFlowTemp>().AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Version = a.Version,
                            IsValid = a.IsValid,
                            ObjType = a.ObjType,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            DeptIds = a.DeptIds,
                            CategoryIds = a.CategoryIds,
                            FlowItems = a.FlowItems,

                        };
            var local = from a in query.AsEnumerable()

                        select new DevFlowTempDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ObjType = a.ObjType,
                            DeptIds = a.DeptIds,
                            DeptIdsArray = StringHelper.String2ArrayInt(a.DeptIds),
                            CategoryIds = a.CategoryIds,
                            CateIdsArray = StringHelper.String2ArrayInt(a.CategoryIds),
                            FlowItems = a.FlowItems,
                            FlowItemsArray = StringHelper.String2ArrayInt(a.FlowItems),
                            AddDateTime = a.AddDateTime,
                            AddUserId = a.AddUserId,
                            //ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            CateName = GetDataDics(listdic, a.CategoryIds),
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            DeptsName = GetDeptNames(listdepts, a.DeptIds),//部门名称
                            FlowItemsDic = GetFlowItems(a.FlowItems, a.ObjType),//审批事项
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="flowTemp">流程模板</param>
        /// <returns></returns>
        public DevFlowTemp AddSave(DevFlowTemp flowTemp, DevFlowTempHist flowTempHist)
        {
          

            flowTemp.Version = 1;
            flowTemp.IsValid = 1;
            //var Hist = flowTemp.ToModel<DevFlowTemp, DevFlowTempHist>(flowTemp);
            var info = Add(flowTemp);
            flowTempHist.TempId = info.Id;
            flowTempHist.AddDateTime = DateTime.Now;
            DevDb.Set<DevFlowTempHist>().Add(flowTempHist);
            this.SaveChanges();
            return flowTemp;

        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="flowTemp"></param>
        /// <returns></returns>
        public DevFlowTemp UpdateSave(DevFlowTemp flowTemp, DevFlowTempHist flowTempHist)
        {
           // var Hist = flowTemp.ToModel<DevFlowTemp, DevFlowTempHist>();
            Update(flowTemp);
            flowTempHist.TempId = flowTemp.Id;
            
            DevDb.Set<DevFlowTempHist>().Add(flowTempHist);
            this.SaveChanges();
            return flowTemp;

        }
        /// <summary>
        /// 根据条件判断流程是否唯一
        /// </summary>
        /// <param name="flowTemp">流程模板对象</param>
        /// <returns></returns>
        public string CheckFlowUnique(DevFlowTemp flowTemp)
        {
            string flowName = string.Empty;
            var querylist = DevDb.Set<DevFlowTemp>().AsNoTracking().
                Where(a => a.ObjType == flowTemp.ObjType && a.Id != flowTemp.Id).ToList();
            var depts = StringHelper.String2ArrayInt(flowTemp.DeptIds);
            var flowitems = StringHelper.String2ArrayInt(flowTemp.FlowItems);
            var categorys = StringHelper.String2ArrayInt(flowTemp.CategoryIds);
            foreach (var flow in querylist)
            {
                var tempdepts = StringHelper.String2ArrayInt(flow.DeptIds);
                var tempflowitems = StringHelper.String2ArrayInt(flow.FlowItems);
                var tempcategorys = StringHelper.String2ArrayInt(flow.CategoryIds);

                var indepts = depts.Intersect(tempdepts);
                var incategorys = categorys.Intersect(tempcategorys);
                var inflowitems = flowitems.Intersect(tempflowitems);
                if (indepts.Count() > 0 && incategorys.Count() > 0 && inflowitems.Count() > 0)
                {
                    flowName = flow.Name;
                    break;
                }


            }
            return flowName;

        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.Field)
            {
                case "IsValid"://状态
                    var state = Convert.ToByte(info.FieldVal);
                    sqlstr = $"update  dev_flow_temp set IsValid={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        /// 根据参数获取流程模板及审批实例
        /// 用于判断当前是否能够提交流程
        /// </summary>
        /// <param name="requestTemp">请求对象</param>
        /// <returns></returns>
        public ResponTempData GetFlowInfoByWhere(GetTempFlowRequestData requestTemp)
        {
            var tempId = 0;
            var query = from a in DevDb.Set<DevFlowTemp>().AsNoTracking()
                        where a.ObjType == requestTemp.ObjType
                        && a.IsDelete == 0 && a.IsValid == 1
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            FlowItems = a.FlowItems,
                            CategoryIds = a.CategoryIds,
                            DeptIds = a.DeptIds
                        };

            var list = query.ToList();
            foreach (var item in list)
            {
                var arrflowitem = StringHelper.String2ArrayInt(item.FlowItems);
                var arrcateIds = StringHelper.String2ArrayInt(item.CategoryIds);
                var deptIdsIds = StringHelper.String2ArrayInt(item.DeptIds);
                if (requestTemp.ObjType == (int)FlowObjEnums.Contract
                    || requestTemp.ObjType == (int)FlowObjEnums.InvoiceIn
                    || requestTemp.ObjType == (int)FlowObjEnums.InvoiceOut
                    || requestTemp.ObjType == (int)FlowObjEnums.payment)
                {
                    if (arrflowitem.Contains(requestTemp.FlowItem)
                    && arrcateIds.Contains(requestTemp.ObjCateId)
                    && (deptIdsIds.Contains(requestTemp.DeptId)))
                    {
                        tempId = item.Id;
                        break;
                    }
                }
                else
                {
                    if (arrflowitem.Contains(requestTemp.FlowItem)
                   && arrcateIds.Contains(requestTemp.ObjCateId)
                   )
                    {
                        tempId = item.Id;
                        break;
                    }
                }



            }
            var tempHist = DevDb.Set<DevFlowTempHist>().AsNoTracking().Where(a => a.TempId == tempId).OrderByDescending(a => a.Id).FirstOrDefault();
            var tempHistId = tempHist == null ? 0 : tempHist.Id;

            var appinst = DevDb.Set<DevAppInst>().AsNoTracking().Where(a => a.Mission == requestTemp.FlowItem && a.ObjType == requestTemp.ObjType && a.AppObjId == requestTemp.ObjId).OrderByDescending(a => a.Id).FirstOrDefault();
            return new ResponTempData()
            {
                TempId = tempId,
                TempHistId = tempHistId,
                InstId = (appinst == null || (appinst.AppState == 3)) ? 0 : appinst.Id


            };



        }

        /// <summary>
        /// 判断流程节点是否匹配完成
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="amount">金额</param>
        /// <param name="flowType">流程类型</param>
        /// <returns></returns>
        public int SubmitCheckFlow(RequestCheckFlow requestCheckFlow)
        {
            if ((requestCheckFlow.TempId ?? 0) <= 0 || (requestCheckFlow.FlowType ?? 0) <= 0)
            {
                return -1;
            }
            else
            {
                var FlowTempNodes = DevDb.Set<DevFlowTempNode>()
                     .Where(a => a.TempId == requestCheckFlow.TempId).ToList();
                var FlowTempNodeInfos = DevDb.Set<DevFlowTempNodeInfo>()
                    .Where(a => a.TempId == requestCheckFlow.TempId).ToList();
                var TempNodeLines = DevDb.Set<DevTempNodeLine>()
                    .Where(a => a.TempId == requestCheckFlow.TempId).ToList();
                var startnode = FlowTempNodes.Where(a => a.Type == (int)NodeTypeEnum.NType0).FirstOrDefault();
                var endnode = FlowTempNodes.Where(a => a.Type == (int)NodeTypeEnum.NType1).FirstOrDefault();

                if (startnode == null || endnode == null)
                {
                    return -2;
                }
                else
                {
                    //需要节点校验金额的
                    if (requestCheckFlow.FlowType == (int)FlowObjEnums.Contract
                        || requestCheckFlow.FlowType == (int)FlowObjEnums.InvoiceIn
                        || requestCheckFlow.FlowType == (int)FlowObjEnums.InvoiceOut
                        || requestCheckFlow.FlowType == (int)FlowObjEnums.payment)
                    {
                        var nodeId = FindNode(TempNodeLines, FlowTempNodeInfos, startnode.StrId, endnode.StrId, (requestCheckFlow.Amount ?? 0));
                        var isest = TempNodeLines.Any(a => a.From == nodeId && a.To == endnode.StrId);
                        if (!isest)
                        {
                            return -3;//流程节点没有匹配完成
                        }
                    }
                    else
                    {
                        var existnode = FlowTempNodes.Where(a =>
                          a.StrId != startnode.StrId &&
                          a.StrId != endnode.StrId).Any();
                        var existnodeinfo = FlowTempNodeInfos
                            .Where(a => a.NodeStrId != startnode.StrId &&
                        a.NodeStrId != endnode.StrId).Any();
                        if (!existnode || !existnodeinfo)
                        {
                            return -4;//
                        }


                    }



                }

            }

            return 0;

        }
        /// <summary>
        /// 查找当前满足条件的节点
        /// </summary>
        /// <param name="tempNodeLines">节点连接线</param>
        /// <param name="flowTempNodeInfos">节点集合</param>
        /// <param name="FromNode">上级节点</param>
        /// <param name="endnodestr">结束节点</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        private string FindNode
            (IList<DevTempNodeLine> tempNodeLines, IList<DevFlowTempNodeInfo> flowTempNodeInfos,
            string FromNode, string endnodestr, decimal amount)
        {
            //下级节点ID
            var tonodes = tempNodeLines.Where(a => a.From == FromNode)
                   .Select(a => a.To).ToList();
            //下级节点信息
            var tonodeinfos = flowTempNodeInfos.Where(a =>
            tonodes.Contains(a.NodeStrId) && a.NodeStrId != endnodestr).ToList();
            var nodestr = "";
            foreach (var item in tonodeinfos)
            {
                if ((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 1)
                {
                    if (amount >= (item.Min ?? 0) && amount <= (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 0)
                {
                    if (amount >= (item.Min ?? 0) && amount < (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 1)
                {
                    if (amount > (item.Min ?? 0) && amount <= (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 0)
                {
                    if (amount > (item.Min ?? 0) && amount < (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }



            }
            if (!string.IsNullOrEmpty(nodestr))
            {
                FindNode(tempNodeLines, flowTempNodeInfos
                    , nodestr, endnodestr, amount);
            }

            return FromNode;//返回最后满足条件的节点



        }



    }
}
