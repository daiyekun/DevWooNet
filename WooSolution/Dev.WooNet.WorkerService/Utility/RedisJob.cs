using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WorkerService
{
    /// <summary>
    /// Redis初始化
    /// </summary>
    public class RedisJob : IJob
    {
        private string url = $"{DevQuartzConnModel.QuartzUrl}/api/DevCommon/InitRedisData";
        public Task Execute(IJobExecutionContext context)
        {
            
            WebClient client = new WebClient();
            string desc = client.DownloadString(url);
            return Task.CompletedTask;
        }
    
    }
}
