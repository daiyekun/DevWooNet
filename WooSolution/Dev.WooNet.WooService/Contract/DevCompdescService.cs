using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model.DevDTO;
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
    /// 备忘录
    /// </summary>
    public partial class DevCompdescService
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
        public AjaxListResult<DevCompdescDTO> GetList<s>(PageInfo<DevCompdesc> pageInfo, Expression<Func<DevCompdesc, bool>> whereLambda,
             Expression<Func<DevCompdesc, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevCompdesc>().AsTracking().Where<DevCompdesc>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevCompdesc>))
            { //分页
                tempquery = tempquery.Skip<DevCompdesc>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevCompdesc>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Item=a.Item,
                            Remark = a.Remark,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,






                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompdescDTO
                        {
                            Id = a.Id,
                            Item = a.Item,
                            Remark = a.Remark,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            AddUserName= RedisDevCommUtility.GetUserName(a.AddUserId ?? 0)

                        };
            return new AjaxListResult<DevCompdescDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevCompdescDTO GetInfoById(int Id)
        {
            var query = from a in this.DevDb.Set<DevCompdesc>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Item = a.Item,
                            Remark=a.Remark,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            CompId = a.CompId,
                           


                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompdescDTO
                        {
                            Id = a.Id,
                            Item = a.Item,
                            Remark = a.Remark,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            CompId = a.CompId,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0)

                        };
            return local.FirstOrDefault();
        }

    }
}
