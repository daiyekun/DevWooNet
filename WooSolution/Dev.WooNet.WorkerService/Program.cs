using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.WooNet.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    // services.Configure<DevQuartzConnModel>(Configuration.GetSection("DevQuartzConn"));
                    GetDevQuartConfig(Configuration);
                    services.AddHostedService<RedisWorker>();
                    services.AddHostedService<Worker>();
                });

        /// <summary>
        /// ∂¡»°¥Ê¥¢≈‰÷√
        /// </summary>
        /// <param name="Configuration"></param>
        private static void GetDevQuartConfig(IConfiguration Configuration)
        {
            DevQuartzConnModel.QuartzUrl=Configuration.GetSection("DevQuartzConn:QuartzUrl").Value;
            DevQuartzConnModel.QuartzWxCron = Configuration.GetSection("DevQuartzConn:QuartzWxCron").Value;
        }
    }
}
