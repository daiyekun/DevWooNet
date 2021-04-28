﻿using System;
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
    }
}