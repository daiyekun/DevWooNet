using Dev.WooNet.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WebCore.Utility
{
    /// <summary>
    /// 自定义JSON
    /// </summary>
    public class DevResultJson : IActionResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        private object DevData = null;
        /// <summary>
        ///委托方法
        /// </summary>
        private Func<string, string> _func;
        public DevResultJson(object data)
        {
            DevData = data;


        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="data"></param>
        /// <param name="func">回调函数</param>
        public DevResultJson(object data, Func<string, string> func)
        {
            DevData = data;
            _func = func;

        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            byte[] bytes = null;
            if (_func != null)
            {
                var strjson = JsonUtility.SerializeObject(this.DevData);
                //生成字符串以后可能对字符串进行加工
                bytes = Encoding.UTF8.GetBytes(this._func.Invoke(strjson));
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes(JsonUtility.SerializeObject(this.DevData));
            }
            return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Count());

        }
    }
}
