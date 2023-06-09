﻿using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{

    /// <summary>
    /// 权限类
    /// </summary>
   public class PermissionInfo
    {
        /// <summary>
        /// 角色权限
        /// </summary>
        public List<DevRolePession> RolePermissions = new List<DevRolePession>();
        ///// <summary>
        ///// 用户权限
        ///// </summary>
        //public List<UserPermission> UserPermissions = new List<UserPermission>();
        /// <summary>
        /// 拥有权限类型集合
        /// </summary>
        public List<int> listFuntypes = new List<int>();
    }
    /// <summary>
    /// 操作权限实体类
    /// </summary>
    public class PermissionDataInfo
    {
        /// <summary>
        /// 状态码0：有权限操作，1无权限操作，3：部分有权限,4部分状态不允许
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 权限消息
        /// </summary>
        public string Msg { get; set; } = string.Empty;
        /// <summary>
        /// 允许操作的数据ID集合
        /// </summary>
        public IList<int> OptionIds = new List<int>();
        /// <summary>
        /// 不允许操作的对象描述集合（名称、编号....）
        /// </summary>
        public IList<string> noteAllow = new List<string>();
        /// <summary>
        /// 获取权限描述
        /// </summary>
        /// <param name="code">权限状态码</param>
        /// <returns></returns>
        public string GetOptionMsg(int code)
        {
            switch (code)
            {
                case 0:
                    return "有权限";

                case 1:
                    return "无权限";
                case 3:
                    return "部分数据无权限";
                case 4:
                    return "部分数据状态无权限操作";
                default:
                    return "未知数据状态码:" + code;

            }

        }


    }

}
