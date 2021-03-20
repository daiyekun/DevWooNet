using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Middleware;
using Dev.WooNet.WooService;
using log4net;
using log4net.Config;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.WooNet.WebApi
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
            //添加日志
            Log4netHelper.Repository = LogManager.CreateRepository("DevLog4Repository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/Config/log4net.config"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "业务功能API", Version = "v1" });
            });
           
            #region 服务注入
            var connectionString = Configuration.GetConnectionString("MysqlConn");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DevDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));
            services.AddTransient<IDevUserinfoService, DevUserinfoService>();
            services.AddTransient<IDevDatadicService, DevDatadicService>();
            #endregion
            
            #region 跨域
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            #endregion 跨域
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dev.WooNet.WebApi v1"));
            }
            //app.UsePreOptionsRequest();
            app.UseRouting();

            app.UseAuthorization();
            //跨域
            app.UseCors("default");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
