using Dev.WooNet.IWooService;
using Dev.WooNet.WooService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.WooNet.WebAPI.Extend
{

    /// <summary>
    /// 服务注册扩展类，避免所有服务注册都写到Startup 
    /// 使得Startup 比较臃肿
    /// </summary>
    public static class WebServicesExtend
    {
        public static void AddDevServices(this IServiceCollection services)
        {
            services.AddTransient<IDevUserinfoService, DevUserinfoService>();
            services.AddTransient<IDevDatadicService, DevDatadicService>();
            services.AddTransient<IDevDepartmentService, DevDepartmentService>();
            services.AddTransient<IDevRoleService, DevRoleService>();
            services.AddTransient<IDevUserRoleService, DevUserRoleService>();
            services.AddTransient<IDevSysmodelService, DevSysmodelService>();
            services.AddTransient<IDevFlowTempService, DevFlowTempService>();
            services.AddTransient<IDevFlowTempNodeService, DevFlowTempNodeService>();
            services.AddTransient<IDevFlowTempNodeInfoService, DevFlowTempNodeInfoService>();
            services.AddTransient<IDevFlowGroupService, DevFlowGroupService>();
            services.AddTransient<IDevFlowGroupuserService, DevFlowGroupuserService>();
            services.AddTransient<IDevAppInstService, DevAppInstService>();
            services.AddTransient<IDevAppInstNodeService, DevAppInstNodeService>();
            services.AddTransient<IDevAppInstOpinService, DevAppInstOpinService>();
            






            #region 合同系统相关注册
            //合同对方
            services.AddTransient<IDevCompanyService, DevCompanyService>();
            services.AddTransient<IDevCompcontactService, DevCompcontactService>();
            services.AddTransient<IDevCompfileService, DevCompfileService>();
            services.AddTransient<IDevCompdescService, DevCompdescService>();
            services.AddTransient<IDevCountryService, DevCountryService>();

            

            #endregion





        }

    }
}
