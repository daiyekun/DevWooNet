using Dev.WooNet.AutoMapper.Extend;
using Dev.WooNet.Common.Utility;
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
    /// 审批实例节点
    /// </summary>
   public partial class DevAppInstNodeService
    {
        #region 查看页面加载流图片
        /// <summary>
        /// 根据实例加载流程图 
        /// </summary>
        /// <param name="instId">实例ID</param>
        /// <returns></returns>
        public AppFlowNodeDataJson LoadFlowChart(int instId)
        {
            AppFlowNodeDataJson dataJson = new AppFlowNodeDataJson();
            dataJson.nodes = GetFlowNodeView(instId);
            dataJson.lines = GetLineView(instId);
            dataJson.areas = GetAreaView(instId);
            // dataJson.title = flowtemp == null ? "" : flowtemp.Name;
            dataJson.initNum = 16;
            return dataJson;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeViwDTO> GetFlowNodeView(int instId)
        {
            var nodes = DevDb.Set<DevAppInstNode>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeViwDTO> dicnodes = new Dictionary<string, AppInstNodeViwDTO>();
            foreach (var node in nodes)
            {
                var viewnode = AutoMapperHelper.MapTo<DevAppInstNode, AppInstNodeViwDTO>(node);
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
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeLineViwDTO> GetLineView(int instId)
        {
            var lines = DevDb.Set<DevAppInstNodeLine>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeLineViwDTO> dicnodes = new Dictionary<string, AppInstNodeLineViwDTO>();
            foreach (var line in lines)
            {
                var viewline = AutoMapperHelper.MapTo<DevAppInstNodeLine, AppInstNodeLineViwDTO>(line);
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Dash == 1 ? true : false;
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeAreaViewDTO> GetAreaView(int instId)
        {
            var areas = DevDb.Set<DevAppInstNodeArea>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeAreaViewDTO> dicnodes = new Dictionary<string, AppInstNodeAreaViewDTO>();
            foreach (var area in areas)
            {
              
                var viewarea = AutoMapperHelper.MapTo<DevAppInstNodeArea, AppInstNodeAreaViewDTO>(area);
                viewarea.color = EmunUtility.GetDesc(typeof(ArearColorEnum), (area.Color));
                viewarea.alt = area.Alt == 1 ? true : false;

                dicnodes.Add(viewarea.strid, viewarea);

            }
            return dicnodes;
        }

        #endregion

        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <param name="instId">实例节点ID</param>
        /// <returns></returns>
        public AppInstNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int instId)
        {
            //var listgroups = DevDb.Set<DevFlowGroup>().Select(a => a).ToList();
            IList<UserTemp> listUser = DevDb.Set<DevUserinfo>().Where(a => a.IsDelete != 1).Select(a => new UserTemp
            {
                Id = a.Id,
                Name = a.Name,
                ShowName = a.ShowName
            }).ToList();
            //IList<DevFlowGroupuser> groupUsers = DevDb.Set<DevFlowGroupuser>().ToList();
            var listusergroups = DevDb.Set<DevAppGroupUser>().Where(a => a.InstId == instId).ToList();
            var query = from a in this.DevDb.Set<DevAppInstNodeInfo>().AsNoTracking()
                            //join b in this.Db.Set<AppGroupUser>().AsNoTracking()
                            //on a.NodeStrId equals b.NodeStrId
                        where a.NodeStrId == nodeStrId
                               && a.InstId == instId
                        select new
                        {
                            Id = a.Id,
                            NodeStrId = a.NodeStrId,
                            Nrule = a.Nrule,
                            ReviseText = a.ReviseText,
                            Max = a.Max,
                            Min = a.Min,
                            IsMax = a.IsMax,
                            IsMin = a.IsMin,
                            GroupId = a.GroupId,
                            GroupName = a.GroupName,
                            InstId = a.InstId,
                            NodeState = a.NodeState


                        };
            var local = from a in query.AsEnumerable()
                        select new AppInstNodeInfoViewDTO
                        {
                            Id = a.Id,
                            NodeStrId = a.NodeStrId,
                            Nrule = a.Nrule,
                            ReviseText = a.ReviseText,
                            Max = a.Max,
                            Min = a.Min,
                            IsMax = a.IsMax,
                            IsMin = a.IsMin,
                            GroupId = a.GroupId,
                            GroupName =a.GroupName,
                            InstId = a.InstId,
                            UserNames = GetUserNames(listUser, listusergroups,a.GroupId),// GetUserNames(a.GroupId, a.InstId),
                            StateDic = EmunUtility.GetDesc(typeof(NodeStateEnum), a.NodeState),

                        };

            return local.FirstOrDefault();


        }

        private string GetUserNames(IList<UserTemp> users,IList<DevAppGroupUser> usergroups,int groupId)
        {

            var listIds = usergroups.Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();
            var listuserName = users.Where(a => listIds.Contains(a.Id)).Select(a => a.ShowName).ToList();
            return StringHelper.ArrayString2String(listuserName);


        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        private string GetUserNames(int groupId, int instId)
        {
            
            var listIds = DevDb.Set<DevAppGroupUser>().Where(a => a.GroupId == groupId && a.InstId == instId).Select(a => a.UserId).ToList();
            var listuserName = DevDb.Set<DevUserinfo>().Where(a => listIds.Contains(a.Id)).Select(a => a.ShowName).ToList();
            return StringHelper.ArrayString2String(listuserName);


        }
    }
}
