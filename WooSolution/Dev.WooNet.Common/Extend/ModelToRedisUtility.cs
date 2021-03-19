using Dev.WooNet.Common.Models;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dev.WooNet.Common.Utility
{
   public static class ModelToRedisExtend
    {
        

        /// <summary>
        /// 初始化一些Hash
        /// </summary>
        /// <typeparam name="T">当前实体类型</typeparam>
        /// <param name="t1">实体对象</param>
        /// <param name="hashkey">hashKey</param>
        /// <param name="func"></param>
        public static void SetRedisHash<T>(this T t1,string hashkey, Func<string, int, string> func)
            where T: IModelDTO
        {
            Type t = t1.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var v = PropertyUtility.GetObjectPropertyValue(t1, p.Name);
                var key = func.Invoke(hashkey, t1.Id);
                
                RedisUtility.HashUpdate(key, p.Name, v);
            }
        }
        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <typeparam name="T">当前实体类型</typeparam>
        /// <param name="t1">实体对象</param>
        /// <param name="hashkey">hashKey</param>
        /// <param name="func"></param>
        public static void DelRedisHash<T>(this T t1, string hashkey, Func<string, int, string> func)
            where T : IModelDTO
        {
            Type t = t1.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var key = func.Invoke(hashkey, t1.Id);

                RedisUtility.HashDelete(key, p.Name);
            }
        }
    }
}
