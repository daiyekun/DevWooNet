﻿using Dev.WooNet.Common.Models;
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
        /// <summary>
        /// 根据角色菜单集合
        /// </summary>
        /// <returns></returns>
        IList<DevModelCheck> GetModelChecks(int roleId);
        /// <summary>
        /// 保存角色模块
        /// </summary>
        /// <param name="rolemodel">角色模块对象</param>

        IList<DevRoleModule> SaveRolemodel(RoleModel rolemodel);
        /// <summary>
        /// 根据用户获取菜单权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>

        IList<WinuiMenu> GetWinDeskMenus(int userId);
        /// <summary>
        /// 开始菜单-查询系统菜单为是的
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns>系统菜单</returns>
        IList<WinuiMenu> GetWinStartMenus(int userId);
        /// <summary>
        /// 系统菜单  layuiTree
        /// </summary>
        /// <returns>返回系统菜单 Layui Tree</returns>
        IList<LayuiTree> GetLayuiTreeData();


        }
}
