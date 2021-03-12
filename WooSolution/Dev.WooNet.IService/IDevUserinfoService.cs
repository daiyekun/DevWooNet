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
        public DevListInfo<DevUserinfoDTO> GetList<s>(PageInfo<DevUserinfo> pageInfo, Expression<Func<DevUserinfo, bool>> whereLambda,
            Expression<Func<DevUserinfo, s>> orderbyLambda, bool isAsc);

    }
}
