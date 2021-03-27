using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
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
    /// 数据字典
    /// </summary>
    public partial class DevDatadicService
    {
        private string RedisKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDic}";

        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevDatadicDTO> GetList<s>(PageInfo<DevDatadic> pageInfo, Expression<Func<DevDatadic, bool>> whereLambda,
            Expression<Func<DevDatadic, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevDatadic>().AsTracking().Where<DevDatadic>(whereLambda).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevDatadic>))
            { //分页
                tempquery = tempquery.Skip<DevDatadic>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevDatadic>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Pid = a.Pid,//pid
                            Sort = a.Sort,//排序
                            TypeInt = a.TypeInt,//类别ID
                            Remark = a.Remark,//备注
                            IsDelete = a.IsDelete,
                            


                        };
            var local = from a in query.AsEnumerable()
                        select new DevDatadicDTO
                        {
                           
                            Id = a.Id,
                            Name = a.Name,//名称
                            Pid = a.Pid,//pid
                            Sort = a.Sort,//排序
                            TypeInt = a.TypeInt,//类别ID
                            Remark = a.Remark,//备注
                            IsDelete = a.IsDelete,

                        };
            return new AjaxListResult<DevDatadicDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="datadic">字典枚举值</param>
        /// <returns>返回枚举</returns>
       public IList<DevDatadicDTO> GetAll()
        {
            IList<DevDatadicDTO> list = RedisUtility.StringGetToList<DevDatadicDTO>($"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDicList}");
            if (list == null)
            {
                var query = from a in this.DevDb.Set<DevDatadic>().AsTracking()
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,//名称
                                Pid = a.Pid,//pid
                                Sort = a.Sort,//排序
                                TypeInt = a.TypeInt,//类别ID
                                Remark = a.Remark,//备注
                                IsDelete = a.IsDelete,//移动电话
                            };
                var local = from a in query.AsEnumerable()
                            select new DevDatadicDTO
                            {

                                Id = a.Id,
                                Name = a.Name,//名称
                                Pid = a.Pid,//pid
                                Sort = a.Sort,//排序
                                TypeInt = a.TypeInt,//类别ID
                                Remark = a.Remark,//备注
                                IsDelete = a.IsDelete,//移动电话

                            };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync($"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDicList}", list);
            }

            return list;

        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">字典枚举</param>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{this.RedisKey}";
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevDatadicDTO>($"{curdickey}", (a, c) =>
                    {
                        return $"{a}:{c}";
                    });
                }
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
            }
           

        }


    }
}
