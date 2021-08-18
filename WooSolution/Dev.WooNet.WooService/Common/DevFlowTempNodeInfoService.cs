using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
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
                            GroupName = DevDb.Set<DevFlowGroup>().Where(c=>c.Id==a.GroupId).Any()? DevDb.Set<DevFlowGroup>().Where(c => c.Id == a.GroupId).FirstOrDefault().Name:"",
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
                            GroupName = a.GroupName,
                            TempId = a.TempId,
                            UserNames = GetUserNames(a.GroupId)


                        };

            return local.FirstOrDefault();


        }

        /// <summary>
        /// 用户名称
        /// </summary>
        /// <param name="groupId">组ID</param>
        /// <returns></returns>
        private string GetUserNames(int groupId)
        {
               var listIds = this.DevDb.Set<DevFlowGroupuser>().Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();
            var listuserName = this.DevDb.Set<DevUserinfo>().Where(a => listIds.Contains(a.Id)).Select(a => a.ShowName).ToList();
            return StringHelper.ArrayString2String(listuserName);
            
        }
    }
}
