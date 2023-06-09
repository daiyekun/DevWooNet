﻿using Dev.WooNet.AutoMapper.Extend;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
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
    /// 流程实例
    /// </summary>
   public partial class DevAppInstService
    {

        #region 提交流程
        /// <summary>
        /// 提交审批流程
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>审批实例</returns>
        public DevAppInst SubmitWorkFlow(DevAppInst appInst)
        {
             var temphist = DevDb.Set<DevFlowTempHist>().AsNoTracking().Where(a => a.Id == appInst.TempHistId).OrderByDescending(a => a.Id).FirstOrDefault();
           //  appInst.TempHistId = appInst.TempHistId;
             //appInst.TempHistId = temphist != null ? temphist.Id : 0;
            var info = Add(appInst);
        //查询打回的审批实例
           var oldinstInfo = this.DevDb.Set<DevAppInst>().Where(a => a.AppObjId == info.AppObjId && a.ObjType == info.ObjType && a.AppState == 3).OrderByDescending(a => a.Id).FirstOrDefault();
            if (oldinstInfo != null)
            {
               oldinstInfo.NewInstId = info.Id;
            }
            this.SaveChanges();
            SaveWfNode(info, temphist);
            return appInst;
        }

        /// <summary>
        /// 保存节点
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <param name="temphist">模板历史</param>
        private void SaveWfNode(DevAppInst appInst, DevFlowTempHist temphist)
        {
            switch (appInst.ObjType)
            {
                case (int)FlowObjEnums.Customer:
                case (int)FlowObjEnums.Supplier:
                case (int)FlowObjEnums.Other:
                case (int)FlowObjEnums.project://不需要金额判断每个节点的
              
                    {
                        AddNode(appInst, temphist);
                        AddNodeInfo(appInst);
                        AddLine(appInst);
                    }
                    break;
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    {
                        AddNodeByAmount(appInst, temphist);

                    }
                    break;

            }
            AddArea(appInst);
            this.SaveChanges();
            SetCurrentNode(appInst);

        }
        /// <summary>
        /// 添加根据金额判断的流程节点
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <param name="temphist">模板历史</param>
        private void AddNodeByAmount(DevAppInst appInst, DevFlowTempHist temphist)
        {
            var subminfo = new SubmitWfRequest();
            subminfo.Amount = appInst.AppObjAmount ?? 0;
            subminfo.TempId = appInst.TempHistId ;
            var nodeIds = FlowServoceUtility.GetNodeStrIds(subminfo, this.DevDb);
            AddNode(appInst, temphist, nodeIds);
            AddNodeInfo(appInst, nodeIds);
            AddLine(appInst, nodeIds);
        }

        /// <summary>
        /// 提交流程时设置当前审批节点（第一个审批节点）
        /// </summary>
        ///<param name="appInst">审批实例对象</param>
        private void SetCurrentNode(DevAppInst appInst)
        {
            var listnodes = DevDb.Set<DevAppInstNode>().AsNoTracking().Where(a => a.InstId == appInst.Id).ToList();
            var stratNode = listnodes.FirstOrDefault(a => a.Type == 0);
            if (stratNode != null)
            {
                 var firstLine = DevDb.Set<DevAppInstNodeLine>().AsNoTracking().Where(a => a.InstId == appInst.Id && a.From == stratNode.NodeStrId).FirstOrDefault();
                
                if (firstLine != null)
                {
                    var currnode = listnodes.Where(a => a.NodeStrId == firstLine.To).FirstOrDefault();
                    if (currnode != null)
                    {
                        StringBuilder sqlstr = new StringBuilder();
                        sqlstr.Append($"update dev_app_inst_node_line set Marked=1 where Id={firstLine.Id};");
                        sqlstr.Append($"update dev_app_Inst set CurrentNodeId={currnode.Id},CurrentNodeStrId='{currnode.NodeStrId}',CurrentNodeName='{currnode.Name}',AppState=1 where Id={appInst.Id};");
                        //sqlstr.Append($"update AppInstHist set CurrentNodeId={currnode.Id},CurrentNodeStrId='{currnode.NodeStrId}',CurrentNodeName='{currnode.Name}',AppState=1 where Id={instHistId};");
                        //节点状态修改成审核中
                        sqlstr.Append($"update dev_app_inst_node set NodeState=1,Marked=1,ReceDateTime='{DateTime.Now}' where InstId={appInst.Id} and NodeStrId='{currnode.NodeStrId}';");
                        //sqlstr.Append($"update AppInstNodeHist set NodeState=1,Marked=1,ReceDateTime='{DateTime.Now}' where InstHistId={instHistId} and NodeStrId='{currnode.NodeStrId}';");
                        //节点信息状态和节点状态一致，冗余是为了后期查询减少连表查询
                        sqlstr.Append($"update dev_app_inst_node_info set NodeState=1 where InstId={appInst.Id} and NodeStrId='{currnode.NodeStrId}';");
                        //sqlstr.Append($"update AppInstNodeInfoHist set NodeState=1 where InstHistId={instHistId} and NodeStrId='{currnode.NodeStrId}';");
                        appInst.CurrentNodeStrId = currnode.NodeStrId;
                        appInst.CurrentNodeId = currnode.Id;
                        appInst.CurrentNodeName = currnode.Name;
                        ExecuteSqlCommand(sqlstr.ToString());
                       
                    }
                }
            }
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <param name="temphist">模板历史</param>
        private void AddNode(DevAppInst appInst, DevFlowTempHist temphist)
        {
            var list = this.DevDb.Set<DevFlowTempNode>().AsNoTracking().Where(a => a.TempId == appInst.TempId).ToList();
            IList<DevAppInstNode> listnodes = new List<DevAppInstNode>();
            foreach (var item in list)
            {
                var node = AutoMapperHelper.MapTo<DevFlowTempNode, DevAppInstNode>(item);
               
                node.InstId = appInst.Id;
                node.TempHistId = temphist != null ? temphist.Id : 0;
                listnodes.Add(node);

            }
            this.DevDb.Set<DevAppInstNode>().AddRange(listnodes);

        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <param name="nodeStrIds">满足条件的节点集合</param>
        /// <param name="temphist">流程模板历史对象</param>
        private void AddNode(DevAppInst appInst, DevFlowTempHist temphist, IList<string> nodeStrIds)
        {
            int ntype0 = (int)NodeTypeEnum.NType0;
            int ntype1 = (int)NodeTypeEnum.NType1;
            var arrayIds = nodeStrIds.ToArray();
            var list = this.DevDb.Set<DevFlowTempNodeHist>().AsNoTracking()
                .Where(a => (a.TempHistId == appInst.TempHistId && arrayIds.Contains(a.StrId))
                || (a.TempHistId == appInst.TempHistId && (a.Type == ntype0 || a.Type == ntype1))).ToList();
            IList<DevAppInstNode> listnodes = new List<DevAppInstNode>();
            foreach (var item in list)
            {
                var node = AutoMapperHelper.MapTo<DevFlowTempNodeHist, DevAppInstNode>(item);
                
                node.InstId = appInst.Id;
                node.TempHistId = temphist != null ? temphist.Id : 0;
                listnodes.Add(node);

            }
            this.DevDb.Set<DevAppInstNode>().AddRange(listnodes);

        }
        /// <summary>
        /// 添加连线
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        private void AddNodeInfo(DevAppInst appInst)
        {
            var listgroups = DevDb.Set<DevFlowGroup>().ToList();

            var listnodeinfos = DevDb.Set<DevFlowTempNodeInfo>().AsNoTracking().Where(a => a.TempId == appInst.TempId).ToList();
            IList<DevAppInstNodeInfo> listnodeInfo = new List<DevAppInstNodeInfo>();
            foreach (var nInfo in listnodeinfos)
            {

               
                var nodeInfo = AutoMapperHelper.MapTo<DevFlowTempNodeInfo, DevAppInstNodeInfo>(nInfo);
                nodeInfo.InstId = appInst.Id;
                nodeInfo.NodeState = 0;//默认都是未审核
                var groupinfo = listgroups.Where(a => a.Id == nInfo.GroupId).FirstOrDefault();
                nodeInfo.GroupName = groupinfo==null?"": groupinfo.Name;//nInfo.Group.Name;//组名称
                listnodeInfo.Add(nodeInfo);

            }
            this.DevDb.Set<DevAppInstNodeInfo>().AddRange(listnodeInfo);
            AddNodeGroupUser(appInst, listnodeinfos);
        }

        /// <summary>
        /// 添加连线
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <param name="nodeStrIds">满足条件的节点</param>
        private void AddNodeInfo(DevAppInst appInst, IList<string> nodeStrIds)
        {
            var listgroups = DevDb.Set<DevFlowGroup>().ToList();
            var tempId = appInst.TempId;
            var tempIds = nodeStrIds.ToArray();
            var listnodeinfos = this.DevDb.Set<DevFlowTempNodeInfo>().AsNoTracking()
                .Where(a => a.TempId == tempId && tempIds.Contains(a.NodeStrId)).ToList();
            IList<DevAppInstNodeInfo> listnodeInfo = new List<DevAppInstNodeInfo>();
            foreach (var nInfo in listnodeinfos)
            {

                var nodeInfo = AutoMapperHelper.MapTo<DevFlowTempNodeInfo, DevAppInstNodeInfo>(nInfo);
              
                nodeInfo.InstId = appInst.Id;
                nodeInfo.NodeState = 0;//默认都是未审核
                var groupinfo = listgroups.Where(a => a.Id == nInfo.GroupId).FirstOrDefault();
                nodeInfo.GroupName = groupinfo == null ? "" : groupinfo.Name;//nInfo.Group.Name;//组名称
              
                listnodeInfo.Add(nodeInfo);

            }
            this.DevDb.Set<DevAppInstNodeInfo>().AddRange(listnodeInfo);
            AddNodeGroupUser(appInst, listnodeinfos);
        }


        /// <summary>
        /// 添加节点组
        /// </summary>
        /// <param name="appInst">审批实例</param>
        private void AddNodeGroupUser(DevAppInst appInst, IList<DevFlowTempNodeInfo> tempNodeInfos)
        {
            var groupids = tempNodeInfos.Select(a => a.GroupId).ToList();
            var groups = this.DevDb.Set<DevFlowGroupuser>().Where(a => groupids.Contains(a.GroupId)).ToList();
            IList<DevAppGroupUser> appGroupUsers = new List<DevAppGroupUser>();
            foreach (var tempNodeInfo in tempNodeInfos)
            {
                var userids = groups.Where(a => a.GroupId == tempNodeInfo.GroupId).Select(a => a.UserId).ToList();
                //var userstrids = StringHelper.ArrayInt2String(userids);
                foreach (var uid in userids)
                {
                    DevAppGroupUser appGroupUser = new DevAppGroupUser();
                    appGroupUser.InstId = appInst.Id;
                    appGroupUser.NodeStrId = tempNodeInfo.NodeStrId;
                    appGroupUser.UserId = uid;
                    appGroupUser.GroupId = tempNodeInfo.GroupId;
                    appGroupUsers.Add(appGroupUser);
                }


            }
            this.DevDb.Set<DevAppGroupUser>().AddRange(appGroupUsers);



        }
        /// <summary>
        /// 添加连线
        /// </summary>
        /// <param name="appInst">实例对象</param>
        private void AddLine(DevAppInst appInst)
        {
            var listlines = DevDb.Set<DevTempNodeLine>().AsNoTracking().Where(a => a.TempId == appInst.TempId).ToList();
            IList<DevAppInstNodeLine> listline = new List<DevAppInstNodeLine>();
            foreach (var line in listlines)
            {
                var lineinfo = AutoMapperHelper.MapTo<DevTempNodeLine, DevAppInstNodeLine>(line);
               
                lineinfo.InstId = appInst.Id;
                listline.Add(lineinfo);
            }
            this.DevDb.Set<DevAppInstNodeLine>().AddRange(listline);

        }
        /// <summary>
        /// 添加连线
        /// </summary>
        /// <param name="appInst">实例对象</param>
        /// <param name="nodeStrIds">满足条件的节点集合</param>
        private void AddLine(DevAppInst appInst, IList<string> nodeStrIds)
        {
            var tempId = appInst.TempId;
            var nodeIds = nodeStrIds.ToArray();

            var listlines = this.DevDb.Set<DevTempNodeLine>().AsNoTracking()
                .Where(a => a.TempId == tempId && (nodeIds.Contains(a.To) || nodeIds.Contains(a.From))).ToList();
            IList<DevAppInstNodeLine> listline = new List<DevAppInstNodeLine>();
            foreach (var line in listlines)
            {
                var lineinfo = AutoMapperHelper.MapTo<DevTempNodeLine, DevAppInstNodeLine>(line);
                
                lineinfo.InstId = appInst.Id;
                listline.Add(lineinfo);
            }
            this.DevDb.Set<DevAppInstNodeLine>().AddRange(listline);

        }
        /// <summary>
        /// 区域ID
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        private void AddArea(DevAppInst appInst)
        {
            var listAreas = DevDb.Set<DevTempNodeArea>().AsNoTracking().Where(a => a.TempId == appInst.TempId).ToList();
            IList<DevAppInstNodeArea> listareas = new List<DevAppInstNodeArea>();
            foreach (var arra in listAreas)
            {
               
                var areainfo = AutoMapperHelper.MapTo<DevTempNodeArea, DevAppInstNodeArea>(arra);
                areainfo.InstId = appInst.Id;
                listareas.Add(areainfo);

            }

            this.DevDb.Set<DevAppInstNodeArea>().AddRange(listareas);

        }

        /// <summary>
        /// 提交流程修改对象流程信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns></returns>
        public int SubmitWfUpdateObjWfInfo(DevAppInst appInst)
        {
            if (appInst != null)
            {
                StringBuilder strsql = new StringBuilder();
                switch (appInst.ObjType)
                {
                    case (int)FlowObjEnums.Customer:
                    case (int)FlowObjEnums.Supplier:
                    case (int)FlowObjEnums.Other:
                        strsql.Append($"update dev_company set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                    case (int)FlowObjEnums.project:
                        strsql.Append($"update ProjectManager set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                    case (int)FlowObjEnums.Contract:
                        strsql.Append($"update ContractInfo set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                    case (int)FlowObjEnums.payment:
                        strsql.Append($"update ContActualFinance set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                    case (int)FlowObjEnums.InvoiceIn:
                        strsql.Append($"update ContInvoice set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                    case (int)FlowObjEnums.InvoiceOut:
                        strsql.Append($"update ContInvoice set Wstatus=1,FlowTo={appInst.Mission},WnodeName='{appInst.CurrentNodeName}'  where Id={appInst.AppObjId}");
                        break;
                  
                }

                return ExecuteSqlCommand(strsql.ToString());
            }
            return 0;
        }

        #endregion 提交流程


        #region 审批历史
       /// <summary>
       /// 审批历史
       /// </summary>
       /// <typeparam name="s"></typeparam>
       /// <param name="pageInfo">分页对象</param>
       /// <param name="sessionUserId">当前登录人员</param>
       /// <param name="whereLambda">where条件</param>
       /// <param name="orderbyLambda">排序条件</param>
       /// <param name="isAsc">是否正序</param>
       /// <returns></returns>
        public AjaxListResult<DevApproveHistListDTO> GetAppHistList<s>(PageInfo<DevAppInst> pageInfo, int sessionUserId, Expression<Func<DevAppInst, bool>> whereLambda, Expression<Func<DevAppInst, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = DevDb.Set<DevAppInst>().AsTracking().Where(whereLambda.Compile()).AsQueryable();
              pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (pageInfo is not NoPageInfo<DevAppInst>)
            { //分页
                tempquery = tempquery.Skip<DevAppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevAppInst>(pageInfo.PageSize);
            }
            

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Mission = a.Mission,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            StartUserId = a.StartUserId,
                            ObjType = a.ObjType,
                            CompleteDateTime = a.CompleteDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new DevApproveHistListDTO
                        {
                            Id = a.Id,
                            Mission = a.Mission,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission, a.ObjType),//审批事项
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            StartUserName = RedisDevCommUtility.GetUserName(a.StartUserId),
                            CompleteDateTime = a.CompleteDateTime
                        };
            return new AjaxListResult<DevApproveHistListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        #endregion

        #region  审批相关操作begin
        /// <summary>
        /// 根据用户和审批对象ID获取当前数据审批权限
        /// 
        /// </summary>
        /// <param name="reqFlowInfo"></param>
        /// <returns></returns>
        public AppFlowInceInfo GetAppFlowInceInfo(ReqFlowInfoData reqFlowInfo)
        {
            AppFlowInceInfo appFlowInce = new AppFlowInceInfo();
            //查询当前对象审批中实例
            var InceInfo = DevDb.Set<DevAppInst>().Where(a => a.AppObjId == reqFlowInfo.AppObjId && a.AppState == 1&&a.ObjType== reqFlowInfo.ObjType).OrderByDescending(a=>a.Id).FirstOrDefault();
            if (InceInfo!=null)
            {
                appFlowInce.InstId = InceInfo.Id;
                appFlowInce.NodeName = InceInfo.CurrentNodeName;//审批节点名称
                appFlowInce.NodeStrId = InceInfo.CurrentNodeStrId;//审批节点ID
                appFlowInce.NodeId = InceInfo.CurrentNodeId??0;//节点ID
                var nodeinfo = DevDb.Set<DevAppInstNodeInfo>().Where(a => a.InstId == InceInfo.Id
                  && a.NodeStrId == InceInfo.CurrentNodeStrId).FirstOrDefault();
                if (nodeinfo != null&& nodeinfo.NodeState==1)
                {//节点在审批中
                    var groupUserIds = DevDb.Set<DevAppGroupUser>()
                        .Where(a => a.GroupId == nodeinfo.GroupId
                        &&a.InstId== InceInfo.Id&&a.NodeStrId== InceInfo.CurrentNodeStrId).Select(a => a.UserId).ToList();

                    if(groupUserIds!=null&& groupUserIds.Contains(reqFlowInfo.CurrUserId))
                    {

                        appFlowInce.AppAuth = 1;//有审批权限
                    }
                    

                }
            }
            return appFlowInce;

        }

        #endregion 审批相关操作end

    }
}
