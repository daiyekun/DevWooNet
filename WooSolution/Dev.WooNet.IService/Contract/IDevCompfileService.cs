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
    /// 合同对方-附件
    /// </summary>
    public partial interface IDevCompfileService
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
        AjaxListResult<DevCompfileDTO> GetList<s>(PageInfo<DevCompfile> pageInfo, Expression<Func<DevCompfile, bool>> whereLambda,
            Expression<Func<DevCompfile, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        DevCompfileDTO GetInfoById(int Id);
    }
}
