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
    /// 合同对方
    /// </summary>
    public partial interface IDevCompanyService
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
        AjaxListResult<DevCompanyDTO> GetList<s>(PageInfo<DevCompany> pageInfo, Expression<Func<DevCompany, bool>> whereLambda,
             Expression<Func<DevCompany, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <returns></returns>
        DevCompany SaveCompany(DevCompany info);
        /// <summary>
        /// 根据Id 信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        DevCompanyDTO GetInfoById(int Id);
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        int ClearItemData(int currUserId);
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        int UpdateItems(int Id, int currUserId);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">删除Ids</param>
        /// <returns></returns>
        AjaxResult DelCompany(string Ids);
        



        }
}
