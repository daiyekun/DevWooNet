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
        private object DevData = null;
        public DevResultJson(object data) {
            DevData = data;


        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.SerializeObject(this.DevData));
            return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Count());

        }
    }
}
