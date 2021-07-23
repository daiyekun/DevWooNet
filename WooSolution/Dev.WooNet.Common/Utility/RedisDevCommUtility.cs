using Dev.WooNet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Utility
{

    /// <summary>
    /// 获取基础信息值
    /// </summary>
   public class RedisDevCommUtility
    {
        /// <summary>
        /// 获取显示名称
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns>用户显示名称</returns>
        public static string GetUserName(int UserId, string fieldName = "ShowName")
        {
            return UserId <= 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisUserKey}:{UserId}", fieldName).ToString();
        }

        /// <summary>
        /// 国家名称
        /// </summary>
        /// <returns></returns>
        public static string GetCountryName(int? CountryId)
        {
            return (CountryId ?? -1) < 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisCountryKey}:{CountryId}", "Name").ToString();
        }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public static string GetProvinceNameName(int? ProvinceId)
        {
            return (ProvinceId ?? -1) < 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisProvinceKey}:{ProvinceId}", "Name").ToString();
        }
        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public static string GetCityName(int? CityId)
        {
            return (CityId ?? -1) < 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisCityKey}:{CityId}", "Name").ToString();
        }
        /// <summary>
        /// 币种
        /// </summary>
        /// <param name="CurrencyId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetCurrencyName(int? CurrencyId, string fileName = "ShortName")
        {
            return (CurrencyId ?? -1) < 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisCurrencyKey}:{CurrencyId}", fileName).ToString();
        }
        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>返回部门名称</returns>
        public static string GetDeptName(int deptId, string fieldName = "Name")
        {
            return deptId <= 0 ? "" : RedisUtility.HashGet($"{RedisKeys.RedisdeptKey}:{deptId}", fieldName).ToString();
        }
        
    }
}
