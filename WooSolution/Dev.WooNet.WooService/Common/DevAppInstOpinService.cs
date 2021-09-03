using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.FlowModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{
    /// <summary>
    /// 审批意见
    /// </summary>
    public partial class DevAppInstOpinService
    {


        #region 同意时提交意见

        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns></returns>
        public int SubmintOption(SubmitOptionInfo submitOption)
        {
            var result = 1;
            var appinst = DevDb.Set<DevAppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();
            //var tempappinst = appinst;
            var currNodeInfo = DevDb.Set<DevAppInstNodeInfo>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            var currNode = DevDb.Set<DevAppInstNode>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();

            SaveOption(submitOption, appinst);
            if (currNodeInfo.Nrule == (int)NodeNruleEnum.All)
            {//全部通过
             //当前节点需要参加的审批人员数
                result = AllApprove(submitOption, result, appinst, currNodeInfo, currNode);

            }
            else if (currNodeInfo.Nrule == (int)NodeNruleEnum.AtWill)
            {//任意通过
                result = ApproveToNextNode(submitOption, result, appinst, currNodeInfo, currNode);
            }



            DevDb.SaveChanges();

            return result;


        }
        /// <summary>
        /// 全部通过
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="result">状态标识</param>
        /// <param name="appinst">审批实例</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <returns></returns>
        private int AllApprove(SubmitOptionInfo submitOption, int result, DevAppInst appinst, DevAppInstNodeInfo currNodeInfo, DevAppInstNode currNode)
        {
            int currNodeAppUserNumber = this.DevDb.Set<DevAppGroupUser>().AsNoTracking()
              .Where(a => a.InstId == appinst.Id && a.NodeStrId == appinst.CurrentNodeStrId).Count();
            //已经审批通过人数
            int currAppedNumber = this.DevDb.Set<DevAppInstOpin>().AsNoTracking()
                .Where(a => a.NodeStrId == appinst.CurrentNodeStrId
                && a.InstId == appinst.Id).Count();
            if ((currAppedNumber + 1) == currNodeAppUserNumber)
            {//表示最后一个审批人
                result = ApproveToNextNode(submitOption, result, appinst, currNodeInfo, currNode);

            }//else如果不是最后一个人只需要添加意见就好，而添加意见直接放到最后。此时不需要做什么

            return result;
        }

        /// <summary>
        /// 节点通过为“全部通过”，并且是最后一个人审批的时候
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="result">状态标识</param>
        /// <param name="appinst">审批实例</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <returns></returns>
        private int ApproveToNextNode(SubmitOptionInfo submitOption, int result, DevAppInst appinst, DevAppInstNodeInfo currNodeInfo, DevAppInstNode currNode)
        {
            List<DevAppInstNodeLine> listlines = this.DevDb.Set<DevAppInstNodeLine>()
                .Where(a => a.From == appinst.CurrentNodeStrId && a.InstId == appinst.Id).Select(a => a).ToList();
            var NextNodeIds = listlines.Select(a => a.To).ToList();
            var NextNodes = this.DevDb.Set<DevAppInstNode>()
              .Where(a => NextNodeIds.Contains(a.NodeStrId) && a.InstId == appinst.Id).Select(a => a).ToList();
            if (NextNodes.Count() == 1)
            {
                LineFlowMapping(submitOption, currNodeInfo, currNode, listlines, NextNodes, appinst);

            }
            else
            {//多个分支节点,目前设计不会走
                result = BranchFlowMapping(submitOption, currNodeInfo, currNode, listlines, NextNodes);

            }

            return result;
        }

        /// <summary>
        /// 多分支
        /// </summary>
        /// <param name="submitOption">提交意见对象</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <param name="listlines">节点连线集合</param>
        /// <param name="NextNodes">下一个节点集合</param>
        /// <returns></returns>
        private int BranchFlowMapping(SubmitOptionInfo submitOption, DevAppInstNodeInfo currNodeInfo, DevAppInstNode currNode, List<DevAppInstNodeLine> listlines, List<DevAppInstNode> NextNodes)
        {
            var NextNodeIds = NextNodes.Select(a => a.NodeStrId).ToList();
            List<DevAppInstNodeInfo> NextNodeInfos = this.DevDb.Set<DevAppInstNodeInfo>().Where(a => NextNodeIds.Contains(a.NodeStrId) && a.InstId == submitOption.InstId).Select(a => a).ToList();
            var strNodeId = GetNextNodeStrId(submitOption, NextNodeInfos);
            UpdateObjectInfo updata = InitUpdateData(submitOption);
            if (string.IsNullOrEmpty(strNodeId))
            {//标识下一个节点集合包含结束节点
                var endint = (int)NodeTypeEnum.NType1;
                if (NextNodes.Select(a => a.Type).Contains(endint))
                {
                    var endnode = NextNodes.Where(a => a.Type == endint).FirstOrDefault(); updata.WfState = 2;
                    //修改数据状态
                    UpdateObjectState(updata);
                    endnode.Marked = 1;
                    var endToline = listlines.Where(a => a.To == endnode.NodeStrId).FirstOrDefault();
                    endToline.Marked = 1;

                }
                else
                {
                    return -1;//没有找到满足条件的分支节点
                }

            }
            else
            {//找找到分支节点
                var findNextNode = NextNodes.Where(a => a.NodeStrId == strNodeId).FirstOrDefault();
                findNextNode.Marked = 1;
                findNextNode.NodeState = 1;
                var findNextNodeInfo = NextNodeInfos.Where(a => a.NodeStrId == strNodeId).FirstOrDefault();
                findNextNodeInfo.NodeState = 1;
                var findnexToline = listlines.Where(a => a.To == strNodeId).FirstOrDefault();
                findnexToline.Marked = 1;


            }
            currNode.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNodeInfo.NodeState = (int)NodeStateEnum.YiTongGuo;

            return 1;
        }

        /// <summary>
        /// 直线程匹配
        /// </summary>
        /// <param name="submitOption">审批意见</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <param name="listlines">下一节点连线集合</param>
        /// <param name="NextNodes">下一节点集合（分支就会参数集合）</param>
        /// <param name="appinst">当前审批实例对象</param>
        private void LineFlowMapping(SubmitOptionInfo submitOption, DevAppInstNodeInfo currNodeInfo, DevAppInstNode currNode, List<DevAppInstNodeLine> listlines, List<DevAppInstNode> NextNodes, DevAppInst appinst)
        {
            UpdateObjectInfo updata = InitUpdateData(submitOption);
            if (NextNodes.First().Type == (int)NodeTypeEnum.NType1)
            { //表示审批结束，当前节点已经是最后一个审批节点
                updata.WfState = 2;
                appinst.AppState = 2;//通过
                //修改数据状态
                // UpdateObjectState(updata);
            }
            else
            {
                updata.WfState = 1;
                updata.WfCurrNodeName = NextNodes.FirstOrDefault().Name;//当前节点
            }
            //修改数据状态
            UpdateObjectState(updata);
            //当前节点
            currNode.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNodeInfo.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNode.CompDateTime = DateTime.Now;
           
            //下一个节点
            var nextNode = NextNodes.FirstOrDefault();
            nextNode.Marked = 1;
            nextNode.NodeState = 1;
            nextNode.ReceDateTime = DateTime.Now;
            DevDb.Entry<DevAppInstNode>(nextNode).State = EntityState.Modified;
            if (nextNode.Type != (int)NodeTypeEnum.NType1 && nextNode.Type != (int)NodeTypeEnum.NType0)
            {
                var nextnodeInfo = this.DevDb.Set<DevAppInstNodeInfo>()
                    .Where(a => a.InstId == submitOption.InstId && a.NodeStrId == nextNode.NodeStrId).FirstOrDefault();
                
                nextnodeInfo.NodeState = 1;
            }
            //审批实例
            appinst.CurrentNodeId = nextNode.Id;
            appinst.CurrentNodeName = nextNode.Name;
            appinst.CurrentNodeStrId = nextNode.NodeStrId;
            appinst.CompleteDateTime = DateTime.Now;
            DevDb.Entry<DevAppInst>(appinst).State = EntityState.Modified;
            //连线颜色
            var appinstline = listlines.FirstOrDefault();
            appinstline.Marked = 1;
            //节点
            DevDb.Entry<DevAppInstNode>(currNode).State = EntityState.Modified;
            //节点信息
            DevDb.Entry<DevAppInstNodeInfo>(currNodeInfo).State = EntityState.Modified;
            //节点连线
            DevDb.Entry<DevAppInstNodeLine>(appinstline).State = EntityState.Modified;


        }




        /// <summary>
        /// 初始化一些Hash
        /// </summary>
        /// <typeparam name="T">当前实体类型</typeparam>
        /// <param name="t1">实体对象</param>
        /// <param name="hashkey">hashKey</param>
        /// <param name="func"></param>
        public static void SetRedisHash(string t1, int UserID, string hashkey, Func<string, int, string> func)
        {
            Type t = t1.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var v = PropertyUtility.GetObjectPropertyValue(t1, p.Name);
                var key = func.Invoke(hashkey, UserID);
                RedisUtility.HashUpdate(key, p.Name, v);
            }
        }
        /// <summary>
        /// 初始化修改数据对象
        /// </summary>
        /// <param name="submitOption">提交审批意见</param>
        /// <returns></returns>
        private UpdateObjectInfo InitUpdateData(SubmitOptionInfo submitOption)
        {
            var updata = new UpdateObjectInfo();
            updata.ObjId = submitOption.ObjId;
            updata.ObjType = (FlowObjEnums)submitOption.ObjType;
            return updata;
        }

        /// <summary>
        /// 查找满足条件的节点Id 
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="NextNodeInfos">下一个节点信息集合</param>
        /// <returns></returns>
        private string GetNextNodeStrId(SubmitOptionInfo submitOption, List<DevAppInstNodeInfo> NextNodeInfos)
        {
            string nodestrId = "";
            foreach (var node in NextNodeInfos)
            {
                if ((node.IsMin == 0 && node.IsMax == 0)
                    && (submitOption.ObjMoney > node.Min && submitOption.ObjMoney < node.Max))
                {

                    nodestrId = node.NodeStrId;
                    break;

                }
                else if ((node.IsMin == 1 && node.IsMax == 0)
                   && (submitOption.ObjMoney >= node.Min && submitOption.ObjMoney < node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }
                else if ((node.IsMin == 1 && node.IsMax == 1)
                   && (submitOption.ObjMoney >= node.Min && submitOption.ObjMoney <= node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }
                else if ((node.IsMin == 0 && node.IsMax == 1)
                    && (submitOption.ObjMoney > node.Min && submitOption.ObjMoney <= node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }

            }

            return nodestrId;
        }

        /// <summary>
        /// 保存同意意见
        /// </summary>
        private void SaveOption(SubmitOptionInfo submitOption, DevAppInst appInst)
        {
            var option = GetOtpionInfo(submitOption, appInst);//意见对象
            option.Result = 2;//同意
            this.DevDb.Set<DevAppInstOpin>().Add(option);
        }
        /// <summary>
        /// 获取审批意见对象
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <param name="submitOption">提交意见对象</param>
        /// <returns>返回审批意见实体对象</returns>
        private DevAppInstOpin GetOtpionInfo(SubmitOptionInfo submitOption, DevAppInst appInst)
        {
            DevAppInstOpin appInstOpin = new DevAppInstOpin();
            appInstOpin.InstId = submitOption.InstId;
            appInstOpin.NodeId = appInst.CurrentNodeId;
            appInstOpin.NodeStrId = appInst.CurrentNodeStrId;
            appInstOpin.AddUserId = submitOption.SubmitUserId;
            appInstOpin.AddDateTime = DateTime.Now;
            appInstOpin.Opinion = submitOption.Option;

            return appInstOpin;
        }


        /// <summary>
        /// 修改审批对象状态
        /// </summary>
        private void UpdateObjectState(UpdateObjectInfo updateObjectInfo)
        {
            switch (updateObjectInfo.ObjType)
            {
                case FlowObjEnums.Customer://客户
                case FlowObjEnums.Supplier://供应商
                case FlowObjEnums.Other://其他对方
                    {
                        var company = this.DevDb.Set<DevCompany>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            company.WnodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            company.Wstatus = 2;//审批通过
                            company.Dstatus = (int)CompanyStateEnum.ShenHeTongGuo;//数据状态
                            company.WnodeName = "";
                            company.FlowTo = null;
                           
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            company.Wstatus = 3;//被打回
                        }
                        //修改标识
                        DevDb.Entry<DevCompany>(company).State = EntityState.Modified;
                    }
                    break;

                //case FlowObjEnums.project://项目
                //    {
                //        var projInfo = this.Db.Set<DevProjectManager>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                //        if (updateObjectInfo.WfState == 1)
                //        {
                //            projInfo.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                //        }
                //        else if (updateObjectInfo.WfState == 2)
                //        {
                //            projInfo.WfState = 2;//审批通过
                //            projInfo.Pstate = (int)ProjStateEnum.Approve;//审批通过
                //            projInfo.WfCurrNodeName = "";
                //            projInfo.WfItem = null;
                //        }
                //        else if (updateObjectInfo.WfState == 3)
                //        {//被打回
                //            projInfo.WfState = 3;//被打回
                //        }
                //    }
                //    break;
                //case FlowObjEnums.Contract://合同
                //    {
                //        var continfo = this.Db.Set<ContractInfo>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                //        if (updateObjectInfo.WfState == 1)
                //        {
                //            continfo.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                //        }
                //        else if (updateObjectInfo.WfState == 2)
                //        {
                //            continfo.WfState = 2;//审批通过
                //            continfo.ContState = (int)ContractState.Approve;//审批通过
                //            continfo.WfCurrNodeName = "";
                //            continfo.WfItem = null;
                //        }
                //        else if (updateObjectInfo.WfState == 3)
                //        {//被打回
                //            continfo.WfState = 3;//被打回
                //        }
                //    }
                //    break;
                //case FlowObjEnums.payment://付款
                //    {
                //        var actFinance = this.Db.Set<ContActualFinance>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                //        if (updateObjectInfo.WfState == 1)
                //        {
                //            actFinance.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                //        }
                //        else if (updateObjectInfo.WfState == 2)
                //        {
                //            actFinance.WfState = 2;//审批通过
                //            actFinance.Astate = (int)ActFinanceStateEnum.Submitted;//(int)ActFinanceStateEnum.Approved;//审批通过
                //            actFinance.WfCurrNodeName = "";
                //            actFinance.WfItem = null;
                //        }
                //        else if (updateObjectInfo.WfState == 3)
                //        {//被打回
                //            actFinance.WfState = 3;//被打回
                //        }
                //    }
                //    break;
                //case FlowObjEnums.InvoiceIn://收票
                //case FlowObjEnums.InvoiceOut://开票
                //    {
                //        var contInvoice = this.Db.Set<ContInvoice>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                //        if (updateObjectInfo.WfState == 1)
                //        {
                //            contInvoice.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                //        }
                //        else if (updateObjectInfo.WfState == 2)
                //        {
                //            contInvoice.WfState = 2;//审批通过
                //            contInvoice.InState = (int)InvoiceStateEnum.Submitted;//(int)ActFinanceStateEnum.Approved;//审批通过
                //            contInvoice.WfCurrNodeName = "";
                //            contInvoice.WfItem = null;
                //        }
                //        else if (updateObjectInfo.WfState == 3)
                //        {//被打回
                //            contInvoice.WfState = 3;//被打回
                //        }
                //    }
                //    break;

            }




        }

        #endregion

        #region 不同意时提交意见

        /// <summary>
        /// 不同意时清理缓存
        /// </summary>
        /// <param name="submitOption">意见</param>
        /// <returns></returns>
        public int SubmintDisagreeOption(SubmitOptionInfo submitOption)
        {
            var appinst = DevDb.Set<DevAppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();
            appinst.AppState = 3;
            appinst.CompleteDateTime = DateTime.Now;
            var currNodeInfo = DevDb.Set<DevAppInstNodeInfo>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            currNodeInfo.NodeState = 3;

            var currNode = DevDb.Set<DevAppInstNode>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            currNode.NodeState = 3;
            currNode.CompDateTime = DateTime.Now;
            var objdata = InitUpdateData(submitOption);
            objdata.WfState = 3;
            UpdateObjectState(objdata);
            var opion = GetOtpionInfo(submitOption, appinst);
            opion.Result = 5;
            this.DevDb.Set<DevAppInstOpin>().Add(opion);
            this.DevDb.SaveChanges();

            return 1;
        }

        #endregion

        ///// <summary>
        ///// 审批意见
        ///// </summary>
        ///// <typeparam name="s"></typeparam>
        ///// <param name="pageInfo"></param>
        ///// <param name="whereLambda"></param>
        ///// <param name="orderbyLambda"></param>
        ///// <param name="isAsc"></param>
        ///// <returns></returns>
        public AjaxListResult<DevAppInstOpinDTO> GetOptinionList<s>(PageInfo<DevAppInstOpin> pageInfo, Expression<Func<DevAppInstOpin, bool>> whereLambda, Expression<Func<DevAppInstOpin, s>> orderbyLambda, bool isAsc)
        {

            var tempquery = DevDb.Set<DevAppInstOpin>().Include(a=>a.Node).AsNoTracking().Where<DevAppInstOpin>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevAppInstOpin>))
            { //分页
                tempquery = tempquery.Skip<DevAppInstOpin>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevAppInstOpin>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            NodeId = a.NodeId,
                            NodeStrId = a.NodeStrId,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            Opinion = a.Opinion,
                            NodeName =a.Node.Name,
                            ReceDateTime =a.Node.ReceDateTime,//节点收到日期


                        };
            var local = from a in query.AsEnumerable()
                        select new DevAppInstOpinDTO
                        {
                            Id = a.Id,
                            AddDateTime = a.AddDateTime,
                            Opinion = a.Opinion,
                            NodeName = a.NodeName,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId),
                            ReceDateTime =a.ReceDateTime,
                        };
            return new AjaxListResult<DevAppInstOpinDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };

        }
    }
}
