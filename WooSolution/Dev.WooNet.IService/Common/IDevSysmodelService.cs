using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.ExtendModel;
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
    /// 系统模块操作
    /// </summary>
    public partial  interface IDevSysmodelService
    {

        /// <summary>
        /// 获取所有系统模块
        /// </summary>
        /// <returns></returns>
         IList<DevSysmodelDTO> GetListAll();
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetModelTreeSelect();
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <returns></returns>
        DevSysmodel SaveData(DevSysmodel info);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">需要删除的ID</param>
        /// <returns></returns>
        int DelSysModel(string Ids);
        /// <summary>
        /// 根据Id 查询信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        DevSysmodelDTO GetSysModelById(int Id);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">字段对象</param>
        /// <returns></returns>
        int UpdateField(UpdateField updateField);
        
        }
}
