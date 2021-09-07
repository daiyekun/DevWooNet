using Dev.WooNet.AutoMapper.Extend;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebAPI.Extend;
using Dev.WooNet.WebAPI.Utility;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Middleware;
using Dev.WooNet.WooService;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

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
            //�����־
            Log4netHelper.Repository = LogManager.CreateRepository("DevLog4Repository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/Config/log4net.config"));
            #region JWt
            services.AddTransient<ICustomJWTService, CustomHSJWTService>();
            services.Configure<ConfigInformation>(Configuration.GetSection("ConfigInformation"));
            services.AddTransient<HttpHelperService>();
            #endregion JWt

            #region Filter
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(CustomExceptionFilterAttribute));
                o.Filters.Add(typeof(CustomActionFilterAttribute));
            });
            #endregion
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ҵ����API", Version = "v1" });
            });
           
            #region ����ע��
            var connectionString = Configuration.GetConnectionString("MysqlConn");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DevDbContext>(options =>
                options.UseMySql(connectionString, serverVersion).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //ע�������չ��
            services.AddDevServices();
            #endregion
            services.AddHttpContextAccessor();

            #region jwtУ��  HS

            //pdf����
            services.AddWkhtmltopdf();

            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            Configuration.Bind("JWTTokenOptions", tokenOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme-->JwtBearerDefaults.AuthenticationScheme
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = false,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
                //��Ȩʧ�ܵ�ʱ�򷵻ؽ��
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>{
                        context.HandleResponse();

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.WriteAsync(JsonConvert.SerializeObject(new AjaxResult()
                        {
                            msg = "ûȨ�޷��ʽӿ�",
                            code = StatusCodes.Status401Unauthorized,
                            count = 0
                        }));
                        return Task.FromResult(0);

                    }

                };
            });

            #endregion


            #region ����
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            #endregion ����


            //�ر�ģ����֤��������״̬400��One or more validation errors occurred.
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            //services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.AddDevMapperFiles();
           
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
            //����
            app.UseStateAutoMapper();
            app.UseCors("default");

            #region jwt��Ȩ,����Cors���棬��Ȼ���ڿ�������
            app.UseAuthentication();
            #endregion

            app.UseRouting();
            //pdf�����ļ�exe
           // RotativaConfiguration.Setup(env.ContentRootPath);
            app.UseAuthorization();
           
            app.UseStaticFiles(new StaticFileOptions
            {

                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads",
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
