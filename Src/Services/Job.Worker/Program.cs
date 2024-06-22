using Quartz.Logging;
using Job.Worker.Log;
using Microsoft.Extensions.Hosting;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using Job.BL;
using Job.Util;
using Quartz.Impl;
using System.Collections.Specialized;

namespace Job.Worker
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            // thêm log cho quartz
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            IHost? builder = Host.CreateDefaultBuilder()
            .ConfigureServices((cxt, services) =>
            {
                services.AddQuartz();
                services.AddQuartzHostedService(opt =>
                {
                    opt.WaitForJobsToComplete = true;
                });
            }).Build();


            NameValueCollection props = JobUtility.QuartzConfig;
            if (props?.Count > 0)
            {
                StdSchedulerFactory stdScheduler = new StdSchedulerFactory(props);
                IScheduler? scheduler = await stdScheduler.GetScheduler();
                if (scheduler != null)
                {

                    await scheduler.Start();
                }
            }

            // will block until the last running job completes
            await builder.RunAsync();
        }

    }


}