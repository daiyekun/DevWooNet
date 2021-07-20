using Dev.WooNet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Utility
{


    /// <summary>
    /// token session 相关操作
    /// </summary>
    public class TokenSessionUtility
    {
        /// <summary>
        /// 产生session 感觉
        /// </summary>
        public static void SetTokenToRedis(string key, string val)
        {
            if (RedisUtility.KeyExists(key))
            {//存在加时间
                RedisUtility.KeyDelete(key);
                RedisUtility.StringSet($"{RedisKeys.TokenRedis}:{key}", val, new TimeSpan(0, 30, 0));//表述30分钟过期
            }
            else
            {
                RedisUtility.StringSet($"{RedisKeys.TokenRedis}:{key}", val, new TimeSpan(0, 30, 0));//表述30分钟过期
            }

        }
    }
}
