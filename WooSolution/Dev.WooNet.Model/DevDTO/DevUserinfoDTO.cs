using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{
    /// <summary>
    /// 显示类
    /// </summary>
    public class DevUserinfoDTO: DevUserinfo,IModelDTO,IDevEntityHandle
    {
        /// <summary>
        /// 姓名描述
        /// </summary>
        public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDic { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        

        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();

            var obj = this.GetType().GetProperty(propName);
            fieldinfo.FileType = obj.PropertyType;
            fieldinfo.FileValue = obj.GetValue(this, null);

            return fieldinfo;
        }


    }

    /// <summary>
    /// 系统登录返回
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 登录标识
        /// </summary>
        public int Reult { get; set; } = 0;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoginUser LoginUser
        {
            get;set;
        }
    }
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        
    }

}
