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
    /// 合同对方联系人
    /// </summary>
    public partial interface IDevCompcontactService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        AjaxListResult<DevCompcontactDTO> GetList<s>(PageInfo<DevCompcontact> pageInfo, Expression<Func<DevCompcontact, bool>> whereLambda,
             Expression<Func<DevCompcontact, s>> orderbyLambda, bool isAsc);
    }
}
