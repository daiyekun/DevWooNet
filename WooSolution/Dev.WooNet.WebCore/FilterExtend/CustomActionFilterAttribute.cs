using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WebCore.FilterExtend
{
    public class CustomActionFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.Headers.Add("Cache-Control", "public,max-age=6000");
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuted{this.Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuting{this.Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuting{this.Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuted{this.Order}");
        }

    }
}
