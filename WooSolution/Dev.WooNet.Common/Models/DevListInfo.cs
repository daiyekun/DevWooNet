﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{
    /// <summary>
    /// 列表返回承载对象
    /// </summary>
    /// <typeparam name="T">泛型：表对象</typeparam>
   public class DevListInfo<T>where T :class, new()
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code=0;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg = "";
        /// <summary>
        /// 条数
        /// </summary>
        public int count=0;
        /// <summary>
        /// 数据集合
        /// </summary>
        public IList<T> data = null;
    }
}