using Dev.WooNet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Utility
{

    /// <summary>
    /// redis 操作
    /// </summary>
    public class DevRedisUtility
    {
        /// <summary>
        /// 公共根据ID从redis获取名称
        /// </summary>
        /// <param name="redisKey">resiskey</param>
        /// <param name="Id">数据ID</param>
        /// <param name="filed">字段名称</param>
        /// <returns>字段值</returns>
        public static string GetResisVale(string redisKey,int Id,string filed)
        {
            return RedisUtility.HashGet($"{redisKey}:{Id}", filed).ToString();

        }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>部门名称</returns>
        public static string GetDeptName(int deptId)
        {
           return RedisUtility.HashGet($"{RedisKeys.RedisdeptKey}:{deptId}","Name").ToString();
        }
    }
}
