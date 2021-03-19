using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.WooNet.WorkerService
{
    /// <summary>
    /// redis 定时初始化
    /// </summary>
    public class RedisWorker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public RedisWorker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
            IJobDetail testJob = JobBuilder.Create<RedisJob>()
                     .WithIdentity("DevTest1", "Devgroup1")
                     .WithDescription("this is DevTest1")
                     .StoreDurably()
                     .Build();

            ITrigger trigger =
                        TriggerBuilder.Create()
                             .StartAt(DateTime.Now)//什么时候开始执行
                             .WithCronSchedule(DevQuartzConnModel.QuartzWxCron)// 时间表达式
                             .Build();

            await scheduler.ScheduleJob(testJob, trigger);
            await  scheduler.Start();
            await Task.CompletedTask;
        }
    }
}
