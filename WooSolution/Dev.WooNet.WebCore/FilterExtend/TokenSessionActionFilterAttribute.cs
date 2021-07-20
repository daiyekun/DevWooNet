using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WebCore.FilterExtend
{
   public class TokenSessionActionFilterAttribute: ActionFilterAttribute
    {
        //private ILogger<TokenSessionActionFilterAttribute> _logger = null;
        //public TokenSessionActionFilterAttribute(ILogger<TokenSessionActionFilterAttribute> logger)
        //{
        //    this._logger = logger;
        //}

        /// <summary>
        /// 方法执行时
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            var loginkey = context.HttpContext.Request.Headers["loginkey"].ToString();
            if (RedisUtility.KeyExists($"{RedisKeys.TokenRedis}:{loginkey}"))
            {
                TokenSessionUtility.SetTokenToRedis(loginkey, "1");//再延长30分钟
            }
            else
            {//跳转登录页面
                context.Result = new JsonResult(new AjaxResult()
                {
                    msg = "登录超时失效,退出重新登录",
                    code = 1001,
                    count = 0
                });

            }
           
        }


    }
}
