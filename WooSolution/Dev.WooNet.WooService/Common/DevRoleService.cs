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
    /// 角色管理
    /// </summary>
    public partial  class DevRoleService
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
        public AjaxListResult<DevRoleDTO> GetList<s>(PageInfo<DevRole> pageInfo, Expression<Func<DevRole, bool>> whereLambda,
             Expression<Func<DevRole, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevRole>().AsTracking().Where<DevRole>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevRole>))
            { //分页
                tempquery = tempquery.Skip<DevRole>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevRole>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code=a.Code,//编号
                            Remark=a.Remark,//说明


                        };
            var local = from a in query.AsEnumerable()
                        select new DevRoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            Remark = a.Remark,//说明

                        };
            return new AjaxListResult<DevRoleDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<DevRoleDTO> GetAll()
        {
            var query = from a in this.DevDb.Set<DevRole>().AsTracking()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            Remark = a.Remark,//说明
                           


                        };
            var local = from a in query.AsEnumerable()
                        select new DevRoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            Remark = a.Remark,//说明

                        };
            return local.ToList();
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
                var curdickey = $"{RedisKeys.RedisUserKey}";
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevRoleDTO>($"{curdickey}", (a, c) =>
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
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <returns></returns>
        public DevRole SaveRole(DevRole roleinfo)
        {
            DevRole resul = null;
            
            if (roleinfo.Id > 0)
            {//修改
                Update(roleinfo);
                resul = roleinfo;

            }
            else
            {
               

                resul = Add(roleinfo);
            }
            SetRedisHash();
            return resul;



        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">需要删除的ID</param>
        /// <returns></returns>
        public int DelRole(string Ids)
        {
            string sqlstr = $"update dev_role set IsDelete=1 where Id in({Ids})";
            var resl = ExecuteSqlCommand(sqlstr);

            SetRedisHash();
            return resl;

        }
        /// <summary>
        /// 根据Id 查询用户信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevRoleDTO GetRoleById(int Id)
        {
            var query = from a in this.DevDb.Set<DevRole>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            Remark = a.Remark,//说明


                        };
            var local = from a in query.AsEnumerable()
                        select new DevRoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            Remark = a.Remark,//说明

                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 保存角色用户
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public int SaveRoleUser(int RoleId,string Ids)
        {
            var userIds = StringHelper.String2ArrayInt(Ids);
            string sqlstr = $"delete from dev_user_role where Rid={RoleId} and Uid in({Ids})";
            ExecuteSqlCommand(sqlstr);
            IList<DevUserRole> urloes = new List<DevUserRole>();
            foreach (var id in userIds)
            {
                var urole = new DevUserRole();
                urole.Rid = RoleId;
                urole.Uid = id;

                urloes.Add(urole);

            }

            DevDb.Set<DevUserRole>().AddRange(urloes);
            SaveChanges();
            return urloes.Count();

        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="Ids">当前用户ID</param>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public int DeleteRoleUser(int RoleId, string Ids)
        {
            var userIds = StringHelper.String2ArrayInt(Ids);
            string sqlstr = $"delete from dev_user_role where Rid={RoleId} and Uid in({Ids})";
            ExecuteSqlCommand(sqlstr);
            return 1;

        }
       






    }
}
