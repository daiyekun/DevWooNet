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
    /// 数据字典
    /// </summary>
    public partial interface IDevDatadicService
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
        AjaxListResult<DevDatadicDTO> GetList<s>(PageInfo<DevDatadic> pageInfo, Expression<Func<DevDatadic, bool>> whereLambda,
            Expression<Func<DevDatadic, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <returns></returns>
        List<DevDatadicDTO> GetAll();
        /// <summary>
        /// 设置数据字典
        /// </summary>
        /// <param name="datadic"></param>
        void SetRedisHash();

    }
}
