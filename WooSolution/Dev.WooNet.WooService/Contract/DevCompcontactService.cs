using Dev.WooNet.Common.Models;
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
    /// 合同对方联系人
    /// </summary>
    public partial  class DevCompcontactService
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
        public AjaxListResult<DevCompcontactDTO> GetList<s>(PageInfo<DevCompcontact> pageInfo, Expression<Func<DevCompcontact, bool>> whereLambda,
             Expression<Func<DevCompcontact, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevCompcontact>().AsTracking().Where<DevCompcontact>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevCompcontact>))
            { //分页
                tempquery = tempquery.Skip<DevCompcontact>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevCompcontact>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Dname = a.Dname,
                            RoleName = a.RoleName,
                            PhoneTel = a.PhoneTel,
                            PhoneNo = a.PhoneNo,
                            Fax = a.Fax,
                            Email = a.Email,
                            Remark = a.Remark,
                            Qq = a.Qq,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                          




                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompcontactDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Dname = a.Dname,
                            RoleName = a.RoleName,
                            PhoneTel = a.PhoneTel,
                            PhoneNo = a.PhoneNo,
                            Fax = a.Fax,
                            Email = a.Email,
                            Remark = a.Remark,
                            Qq = a.Qq,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,

                        };
            return new AjaxListResult<DevCompcontactDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
    }
}
