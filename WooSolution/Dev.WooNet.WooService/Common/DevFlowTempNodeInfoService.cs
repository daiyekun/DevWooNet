using Dev.WooNet.AutoMapper.Extend;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
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
    /// 节点信息
    /// </summary>
    public partial  class DevFlowTempNodeInfoService
    {
        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <returns></returns>
        public FlowTempNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int tempId)
        {
            var listgroups = DevDb.Set<DevFlowGroup>().Select(a => a).ToList();
            IList<UserTemp> listUser = DevDb.Set<DevUserinfo>().Where(a => a.IsDelete != 1).Select(a => new UserTemp
            {
                Id = a.Id,
                Name = a.Name,
                ShowName = a.ShowName
            }).ToList();
            IList<DevFlowGroupuser> groupUsers = DevDb.Set<DevFlowGroupuser>().ToList();
            var query = from a in DevDb.Set<DevFlowTempNodeInfo>().AsNoTracking()
                        where a.NodeStrId == nodeStrId
                               && a.TempId == tempId
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
                            
                            TempId = a.TempId
                        };
            var local = from a in query.AsEnumerable()
                        select new FlowTempNodeInfoViewDTO
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
                            GroupName = listgroups.Where(c => c.Id == a.GroupId).Any() ? listgroups.Where(c => c.Id == a.GroupId).FirstOrDefault().Name : "",
                            TempId = a.TempId,
                            UserNames = GetGroupUsers(a.GroupId, listUser, groupUsers)


                        };

            return local.FirstOrDefault();


        }

       
        /// <summary>
        /// 返回组里所有用户
        /// </summary>
        /// <returns></returns>
        private string GetGroupUsers(int groupId, IList<UserTemp> listUser, IList<DevFlowGroupuser> groups)
        {

            var userIds = groups.Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();
            var userNames = listUser.Where(a => userIds.Contains(a.Id)).Select(a => a.ShowName).ToList();
            return StringHelper.ArrayString2String(userNames);
        }

        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowTempNodeInfo">保存节点信息</param>
        /// <returns></returns>
        public int SaveFlowTempNodeInfo(DevFlowTempNodeInfo flowTempNodeInfo)
        {
            string sqlstr = $"delete from  dev_flow_temp_node_info where NodeStrId='{flowTempNodeInfo.NodeStrId}'";
            ExecuteSqlCommand(sqlstr);
            DevDb.Set<DevFlowTempNodeInfo>().Add(flowTempNodeInfo);
            var histTemp = DevDb.Set<DevFlowTempHist>().Where(a => a.TempId == flowTempNodeInfo.TempId)
                .OrderByDescending(a => a.Id).FirstOrDefault();
            var tempNodeInfoHist = AutoMapperHelper.MapTo<DevFlowTempNodeInfo, DevFlowTempNodeInfoHist>(flowTempNodeInfo);
            tempNodeInfoHist.TempHistId = histTemp != null ? histTemp.Id : 0;
            DevDb.Set<DevFlowTempNodeInfoHist>().Add(tempNodeInfoHist);
            return DevDb.SaveChanges();

        }
    }
}
