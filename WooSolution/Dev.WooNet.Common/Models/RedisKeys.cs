using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{
    /// <summary>
    /// redis key
    /// </summary>
    public class RedisKeys
    {
        /// <summary>
        /// 部门key
        /// </summary>
        public static readonly string RedisdeptKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Depart}";
        /// <summary>
        /// 部门listkey
        /// </summary>
        public static readonly string  Reisdeptredilist = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Departlist}";//列表
       /// <summary>
       /// 用户key
       /// </summary>
        public static readonly string RedisUserKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Devusers}";
        /// <summary>
        /// 系统菜单集合
        /// </summary>
        public static readonly string RedisSysModellist= $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.SysModellist}";

        /// <summary>
        /// token redis key
        /// </summary>
        public static readonly string TokenRedis = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DevToken}";
        /// <summary>
        /// 国家
        /// </summary>
        public static readonly string RedisCountryKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Country}";
        /// <summary>
        /// 省
        /// </summary>
        public static readonly string RedisProvinceKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Province}";
        /// <summary>
        /// 市
        /// </summary>
        public static readonly string RedisCityKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.City}";
        /// <summary>
        /// 币种
        /// </summary>
        public static readonly string RedisCurrencyKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.Currency}";
        /// <summary>
        /// 数据字典
        /// </summary>
        public static readonly string RedisDataDicKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDic}";

        /// <summary>
        /// 省-市级联字符串
        /// </summary>
        public static readonly string redisAddressKey = $"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DevAddress}";
    }
}
