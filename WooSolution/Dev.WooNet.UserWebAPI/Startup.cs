using Dev.WooNet.Common;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WooService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Dev.WooNet.WebCore.Utility;
using Dev.WooNet.WebCore.Middleware;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.Common.Utility;

namespace Dev.WooNet.UserWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            #region Filter
            services.AddControllers(o =>
            {
                //o.Filters.Add(typeof(CORSFilter));
               // o.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
            #endregion
            services.AddCors(options =>
            {
                options.AddPolicy("any", corsbuilder =>
                {
                   // var corsPath = StringHelper.Strint2ArrayString1(Configuration.GetConnectionString("CorsOrigins")).ToArray();
                    corsbuilder.WithOrigins("http://localhost:8088")
                  .AllowAnyMethod()
            .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true); // =AllowAnyOrigin()

                    //.AllowCredentials();//指定处理cookie
                });
            });
            // services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "系统用户API", Version = "v1" });
            });

            #region 服务注入

            var connectionString = Configuration.GetConnectionString("MysqlConn");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DevDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));



            //services.AddTransient<DevDbContext, DevDbContext>();
            services.AddTransient<IDevUserinfoService, DevUserinfoService>();
            #endregion
            //注册跨域请求服务
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("default", policy =>
            //    {
            //        policy.SetIsOriginAllowed(a=>true)
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "系统用户API v1"));
            //}
            //app.UseMiddleware<CorsMiddleware>();

            app.UseRouting();

            app.UseCors("any");
            app.UseAuthorization();
            //app.UseCors("default");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
