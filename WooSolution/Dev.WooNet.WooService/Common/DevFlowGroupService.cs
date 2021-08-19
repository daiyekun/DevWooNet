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
    /// 审批组
    /// </summary>
   public partial class DevFlowGroupService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevFlowGroupDTO> GetList<s>(PageInfo<DevFlowGroup> pageInfo, Expression<Func<DevFlowGroup, bool>> whereLambda,
             Expression<Func<DevFlowGroup, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevFlowGroup>().AsTracking().Where<DevFlowGroup>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevFlowGroup>))
            { //分页
                tempquery = tempquery.Skip<DevFlowGroup>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevFlowGroup>(pageInfo.PageSize);
            }

            IList<UserTemp> listUser = DevDb.Set<DevUserinfo>().Where(a => a.IsDelete != 1).Select(a => new UserTemp
            {
                Id = a.Id,
                Name = a.Name,
                ShowName = a.ShowName
            }).ToList();
            IList<DevFlowGroupuser> groupUsers = DevDb.Set<DevFlowGroupuser>().ToList();

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark=a.Remark,
                            AddUserId=a.AddUserId,
                            AddDateTime=a.AddDateTime,
                            Gstate= a.Gstate,
                            




                        };
            var local = from a in query.AsEnumerable()
                        select new DevFlowGroupDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            AddDateTime = a.AddDateTime,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId),
                            GstateDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.Gstate),
                            UserNames = GetGroupUsers(a.Id, listUser, groupUsers)



                        };
            return new AjaxListResult<DevFlowGroupDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 根据Id 信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevFlowGroupDTO GetInfoById(int Id)
        {
            var query = from a in this.DevDb.Set<DevFlowGroup>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            Gstate = a.Gstate,





                        };
            var local = from a in query.AsEnumerable()
                        select new DevFlowGroupDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            AddDateTime = a.AddDateTime,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId),
                            GstateDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.Gstate),

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
        /// 保存组用户
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="GroupId">组ID</param>
        /// <returns></returns>
        public int SaveGroupUser(int GroupId, string Ids)
        {
            var userIds = StringHelper.String2ArrayInt(Ids);
            string sqlstr = $"delete from dev_flow_groupuser where GroupId={GroupId} and UserId in({Ids})";
            ExecuteSqlCommand(sqlstr);
            IList<DevFlowGroupuser> urloes = new List<DevFlowGroupuser>();
            foreach (var id in userIds)
            {
                var grole = new DevFlowGroupuser();
                grole.GroupId = GroupId;
                grole.UserId = id;

                urloes.Add(grole);

            }

            DevDb.Set<DevFlowGroupuser>().AddRange(urloes);
            SaveChanges();
            return urloes.Count();

        }
    }

   
}
