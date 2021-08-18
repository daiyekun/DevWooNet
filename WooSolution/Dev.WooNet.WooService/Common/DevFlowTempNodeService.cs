using Dev.WooNet.AutoMapper.Extend;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.FlowModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{
    /// <summary>
    /// 流程节点
    /// </summary>
    public partial class DevFlowTempNodeService
    {

        #region 加载节点
        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempId">模板Id</param>
        /// <returns></returns>
        public FlowNodeData LoadNodes(int tempId)
        {
            FlowNodeData dataJson = new FlowNodeData();
            var flowtemp = DevDb.Set<DevFlowTemp>().Where(a => a.Id == tempId).FirstOrDefault();
            dataJson.nodes = GetFlowNodeView(tempId);
            dataJson.lines = GetLineView(tempId);
            dataJson.areas = GetAreaView(tempId);
            dataJson.title = flowtemp == null ? "" : flowtemp.Name;
            dataJson.initNum = 16;
            return dataJson;
        }

        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempinfo">模板对象</param>
        /// <returns></returns>
        public FlowNodeData LoadNodes(DevFlowTemp tempinfo)
        {
            FlowNodeData dataJson = new FlowNodeData();
            // var flowtemp = Db.Set<FlowTemp>().Where(a => a.Id == tempinfo).FirstOrDefault();
            dataJson.nodes = GetFlowNodeView(tempinfo.Id);
            dataJson.lines = GetLineView(tempinfo.Id);
            dataJson.areas = GetAreaView(tempinfo.Id);
            dataJson.title = tempinfo == null ? "" : tempinfo.Name;
            dataJson.initNum = 16;
            return dataJson;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, FlowTempNodeViewDTO> GetFlowNodeView(int tempId)
        {
            var nodes = DevDb.Set<DevFlowTempNode>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, FlowTempNodeViewDTO> dicnodes = new Dictionary<string, FlowTempNodeViewDTO>();
            foreach (var node in nodes)
            {
                var viewnode= AutoMapperHelper.MapTo<DevFlowTempNode, FlowTempNodeViewDTO>(node);
               // var viewnode = node.ToModel<DevFlowTempNode, FlowTempNodeViewDTO>();
                viewnode.type = EmunUtility.GetDesc(typeof(NodeTypeEnum), (node.Type));
                viewnode.alt = node.Alt == 1 ? true : false;
                viewnode.marked = node.Marked == 1 ? true : false;
                dicnodes.Add(viewnode.strid, viewnode);

            }
            return dicnodes;
        }

        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, TempNodeLineViewDTO> GetLineView(int tempId)
        {
            var lines = DevDb.Set<DevTempNodeLine>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeLineViewDTO> dicnodes = new Dictionary<string, TempNodeLineViewDTO>();
            foreach (var line in lines)
            {
                var viewline = AutoMapperHelper.MapTo<DevTempNodeLine, TempNodeLineViewDTO>(line);
                //var viewline = line.ToModel<DevTempNodeLine, TempNodeLineViewDTO>();
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Marked == 1 ? true : false;
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="nodestrId">匹配上的节点ID集合</param>
        private Dictionary<string, TempNodeLineViewDTO> GetLineView(int tempId, IList<string> nodestrIds)
        {
            var lines = DevDb.Set<DevTempNodeLine>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeLineViewDTO> dicnodes = new Dictionary<string, TempNodeLineViewDTO>();
            foreach (var line in lines)
            {
                var viewline = AutoMapperHelper.MapTo<DevTempNodeLine, TempNodeLineViewDTO>(line);
               // var viewline = line.ToModel<DevTempNodeLine, TempNodeLineViewDTO>();
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Dash == 1 ? true : false;
                if (!nodestrIds.Any(a => a == viewline.to) && !nodestrIds.Any(a => a == viewline.from))
                {
                    viewline.dash = true;//细线
                }
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, TempNodeAreaViewDTO> GetAreaView(int tempId)
        {
            var areas = DevDb.Set<DevTempNodeArea>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeAreaViewDTO> dicnodes = new Dictionary<string, TempNodeAreaViewDTO>();
            foreach (var area in areas)
            {
                var viewarea = AutoMapperHelper.MapTo<DevTempNodeArea, TempNodeAreaViewDTO>(area);
              //  var viewarea = area.ToModel<DevTempNodeArea, TempNodeAreaViewDTO>();
                viewarea.color = EmunUtility.GetDesc(typeof(ArearColorEnum), (area.Color));
                viewarea.alt = area.Alt == 1 ? true : false;

                dicnodes.Add(viewarea.strid, viewarea);

            }
            return dicnodes;
        }

        #endregion

        #region 加载节点重装
        /// <summary>
        /// 提交流程时显示流程图
        /// </summary>
        /// <param name="submitWfRes">提交流程时参数对象</param>
        /// <returns></returns>
        public FlowNodeData LoadNodes(SubmitWfRequest submitWfRes)
        {
            FlowNodeData dataJson = new FlowNodeData();
            var flowtemp = DevDb.Set<DevFlowTemp>().Where(a => a.Id == submitWfRes.TempId).FirstOrDefault();
            switch (flowtemp.ObjType)
            {
                case (int)FlowObjEnums.Customer:
                case (int)FlowObjEnums.Supplier:
                case (int)FlowObjEnums.Other:
                case (int)FlowObjEnums.project:
               

                    dataJson = LoadNodes(flowtemp);
                    break;
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    {
                        dataJson = LoadNodes(submitWfRes.TempId);
                        var listnodes = FlowServoceUtility.GetNodeStrIds(submitWfRes, this.DevDb);
                        dataJson.lines = GetLineView(submitWfRes.TempId, listnodes);
                    }
                    break;


            }
            return dataJson;
        }

        #endregion
        /// <summary>
        /// 提交流程时显示流程图程序入口
        /// </summary>
        /// <param name="submitWfRes">提交流程是参数对象</param>
        /// <returns></returns>
        public FlowNodeData SubmitLoadNodes(SubmitWfRequest submitWfRes)
        {
            return LoadNodes(submitWfRes);
        }


        #region 保存节点
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowNodeData">流程节点信息</param>
        /// <param name="tempId">流程模板ID</param>
        /// <returns></returns>
        public int AddFlowNodes(FlowNodeData flowNodeData, int tempId)
        {
            //删除
            DeleteFlowNodes(tempId);
            var histTemp = DevDb.Set<DevFlowTempHist>().AsTracking().Where(a=>a.TempId== tempId).OrderByDescending(a => a.Id).FirstOrDefault();
            var histTempId = histTemp == null ? 0 : histTemp.Id;
            AddNode(flowNodeData, tempId, histTempId);
            AddLine(flowNodeData, tempId, histTempId);
            AddArea(flowNodeData, tempId, histTempId);
            return this.DevDb.SaveChanges();
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="histTempId">历史模板ID</param>
        /// <param name="tempId">模板ID</param>
        /// <param name="flowNodeData">节点数据</param>
        private void AddNode(FlowNodeData flowNodeData, int tempId, int histTempId)
        {
            var tmpnodes = flowNodeData.nodes;
            foreach (var key in tmpnodes.Keys)
            {
                var node = tmpnodes[key];
                var tempNode = AutoMapperHelper.MapTo<FlowTempNodeViewDTO, DevFlowTempNode>(node);
                tempNode.Alt = Convert.ToByte(node.alt ? 1 : 0);
                tempNode.Marked = Convert.ToByte(node.marked ? 1 : 0);
                tempNode.Type = EmunUtility.GetValue(typeof(NodeTypeEnum), node.type);
                tempNode.TempId = tempId;
                this.DevDb.Set<DevFlowTempNode>().Add(tempNode);
                //创建历史
                // var tempNodeHist = tempNode.ToModel<DevFlowTempNode, DevFlowTempNodeHist>();
                var tempNodeHist = AutoMapperHelper.MapTo<DevFlowTempNode, DevFlowTempNodeHist>(tempNode);
                tempNodeHist.TempHistId = histTempId;
                this.DevDb.Set<DevFlowTempNodeHist>().Add(tempNodeHist);


            }
        }
        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="histTempId">模板历史ID</param>
        /// <param name="flowNodeData">保存数据</param>
        private void AddLine(FlowNodeData flowNodeData, int tempId, int histTempId)
        {
            var lines = flowNodeData.lines;
            foreach (var key in lines.Keys)
            {
                var line = lines[key];

               // var templine = line.ToModel<TempNodeLineViewDTO, DevTempNodeLine>();
                var templine = AutoMapperHelper.MapTo<TempNodeLineViewDTO, DevTempNodeLine>(line);
                templine.Alt = Convert.ToByte(line.alt ? 1 : 0);
                templine.Marked = Convert.ToByte(line.marked ? 1 : 0);
                templine.Type = EmunUtility.GetValue(typeof(NodeLineTypeEnum), line.type);
                templine.Dash = Convert.ToByte(line.dash ? 1 : 0);
                templine.TempId = tempId;

                this.DevDb.Set<DevTempNodeLine>().Add(templine);
                //创建历史
                //var tempnodelinehist = templine.ToModel<DevTempNodeLine, DevTempNodeLineHist>();
                var tempnodelinehist = AutoMapperHelper.MapTo<DevTempNodeLine, DevTempNodeLineHist>(templine);
                tempnodelinehist.TempHistId = histTempId;
                this.DevDb.Set<DevTempNodeLineHist>().Add(tempnodelinehist);
            }
        }
        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="flowNodeData"></param>
        private void AddArea(FlowNodeData flowNodeData, int tempId, int histTempId)
        {
            var areas = flowNodeData.areas;
            foreach (var key in areas.Keys)
            {
                var area = areas[key];

                //var temparea = area.ToModel<TempNodeAreaViewDTO, DevTempNodeArea>();
                var temparea = AutoMapperHelper.MapTo<TempNodeAreaViewDTO, DevTempNodeArea>(area);
                temparea.Alt = Convert.ToByte(area.alt ? 1 : 0);
                temparea.Color = EmunUtility.GetValue(typeof(ArearColorEnum), area.color);
                temparea.TempId = tempId;
                this.DevDb.Set<DevTempNodeArea>().Add(temparea);
                //创建历史
               // var tempareahist = temparea.ToModel<DevTempNodeArea, DevTempNodeAreaHist>();
                var tempareahist = AutoMapperHelper.MapTo<DevTempNodeArea, DevTempNodeAreaHist>(temparea);
                tempareahist.TempHistId = histTempId;
                this.DevDb.Set<DevTempNodeAreaHist>().Add(tempareahist);


            }
        }

        /// <summary>
        /// 清除节点数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        public int ClearFlowNodes(int tempId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"delete from dev_flow_temp_node where TempId={tempId};");
            sqlstr.Append($"delete from dev_flow_temp_node_hist where TempId={tempId};");
            sqlstr.Append($"delete from dev_flow_temp_node_info where TempId={tempId};");
            sqlstr.Append($"delete from  dev_flow_temp_node_info_hist where TempId={tempId};");

            sqlstr.Append($"delete from dev_temp_node_line where TempId={tempId};");
            sqlstr.Append($"delete from dev_temp_node_line_hist where TempId={tempId};");
            sqlstr.Append($"delete from dev_temp_node_area where TempId={tempId};");
            sqlstr.Append($"delete from dev_temp_node_area_hist where TempId={tempId};");
          
            return ExecuteSqlCommand(sqlstr.ToString());


        }

        private int DeleteFlowNodes(int tempId)
        {
            StringBuilder sqlstr = new StringBuilder();
            //节点
            sqlstr.Append($"delete from  dev_flow_temp_node where TempId={tempId};");
            //节点信息
            sqlstr.Append($"delete from dev_flow_temp_node_info where TempId={tempId};");
            //节点连线
            sqlstr.Append($"delete from dev_temp_node_line where TempId={tempId};");
            //区域
            sqlstr.Append($"delete from dev_temp_node_area where TempId={tempId};");

            return ExecuteSqlCommand(sqlstr.ToString());
        }

        #endregion
    }
}
