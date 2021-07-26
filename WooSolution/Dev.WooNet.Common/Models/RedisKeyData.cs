using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{
    /// <summary>
    /// Redis Key
    /// </summary>
    public class RedisKeyData
    {
        /// <summary>
        /// Redis 根，用于存储Redis区别于其他Reids
        /// </summary>
        public  static string RedisBaseRoot = "dev";
        /// <summary>
        /// 数据字典
        /// </summary>
        public static string DataDic = "datadic";
        /// <summary>
        /// 数据字典列表
        /// </summary>
        public static string DataDicList = "datadiclist";
        /// <summary>
        /// 部门
        /// </summary>
        public static string Depart = "depart";
        /// <summary>
        /// 部门列表
        /// </summary>
        public static string Departlist = "departList";
        /// <summary>
        /// 用户
        /// </summary>
        public static string Devusers = "users";
        /// <summary>
        /// 系统菜单
        /// </summary>
        public static  string SysModellist = "sysmodelList";
        /// <summary>
        /// Token
        /// </summary>
        public static string DevToken = "Token";
        /// <summary>
        /// 国家
        /// </summary>
        public static string Country = "Country";
        /// <summary>
        /// 省
        /// </summary>
        public static string Province = "Province";
        /// <summary>
        /// 市
        /// </summary>
        public static string City = "City";
        /// <summary>
        /// 币种
        /// </summary>
        public static string Currency = "Currency";
        /// <summary>
        /// 地址
        /// </summary>
        public static string DevAddress = "DevAddress";




    }
}
