using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
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
    /// 用户实现
    /// </summary>
    public partial class DevUserinfoService: BaseService<DevUserinfo>, IDevUserinfoService
    {

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public DevListInfo<DevUserinfoDTO> GetList<s>(PageInfo<DevUserinfo> pageInfo, Expression<Func<DevUserinfo, bool>> whereLambda,
             Expression<Func<DevUserinfo, s>> orderbyLambda, bool isAsc)
         {
            var tempquery = this.DevDb.Set<DevUserinfo>().AsTracking().Where<DevUserinfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevUserinfo>))
            { //分页
                tempquery = tempquery.Skip<DevUserinfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevUserinfo>(pageInfo.PageSize);
            }
          
        
        var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex=a.Sex,//性别
                            Age=a.Age,//年龄
                            Tel=a.Tel,//电话
                            Mobile=a.Mobile,//移动电话
                            Email=a.Email,//邮件
                            EntryDatetime=a.EntryDatetime,//出生日期
                            IdNo=a.IdNo,//身份证号
                            DepId=a.DepId,//部门ID
                            Ustate=a.Ustate,//状态
                            Mstart=a.Mstart,
                            WxCode=a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,
                       

                        };
            var local = from a in query.AsEnumerable()
                        select new DevUserinfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            SexDic= EmunUtility.GetDesc(typeof(UserStateEnum), a.Sex??2),
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            StateDic = EmunUtility.GetDesc(typeof(UserStateEnum), a.Ustate),
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,

                        };
            return new DevListInfo<DevUserinfoDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        }
}
