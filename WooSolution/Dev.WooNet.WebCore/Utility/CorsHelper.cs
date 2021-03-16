using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NF.Common.Utility;

namespace Dev.WooNet.WebCore.Utility
{
    /// <summary>
    /// 操作跨域
    /// </summary>
    public static class CorsHelper
    {
        public static void AddDevCors(this IServiceCollection services, IConfiguration Configuration)
        {

            //services.AddCors(o => o.AddPolicy("AllowSpecificOrigin",
            //          builder =>
            //          {
            //              builder.WithOrigins(StringHelper.Strint2ArrayString1(Configuration.GetConnectionString("CorsOrigins")).ToArray())
            //          .AllowAnyMethod()
            //        .AllowAnyHeader();
            //        //.AllowCredentials();


            //          }));
            //定义配置跨域处理
            //services.AddCors(options =>
            //{
            //    //完全公开,不支持cookie传递
            //    options.AddPolicy("any", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            //    });

            //    ////指定域名公开，可以支持cookie
            //    //options.AddPolicy("all", policy =>
            //    //{
            //    //    policy.WithOrigins(
            //    //        "null", 
            //    //        "http://localhost:8088",
            //    //         "http://localhost:8081"
            //    //        )
            //    //        .AllowAnyMethod()
            //    //        .AllowAnyHeader()
            //    //        .AllowCredentials();
            //    //});
            //});






        }
    }
}
