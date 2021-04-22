using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{
    /// <summary>
    /// 用户操作接口
    /// </summary>
    public partial interface IDevUserinfoService:IBaseService<DevUserinfo>
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        AjaxListResult<DevUserinfoDTO> GetList<s>(PageInfo<DevUserinfo> pageInfo, Expression<Func<DevUserinfo, bool>> whereLambda,
            Expression<Func<DevUserinfo, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
         IList<DevUserinfoDTO> GetAll();
        /// <summary>
        /// 设置用户Redis
        /// </summary>
        void SetRedisHash();
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userinfo">用户实体</param>
        /// <returns></returns>
        DevUserinfo SaveUser(DevUserinfo userinfo);
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Ids">需要删除的用户ID</param>
        /// <returns></returns>
        int DelUser(string Ids);
        /// <summary>
        /// 根据Id 查询用户信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        DevUserinfoDTO GetUserById(int Id);

    }
}
