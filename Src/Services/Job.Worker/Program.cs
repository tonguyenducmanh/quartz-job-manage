using Quartz.Logging;
using Job.Worker.Log;
using Microsoft.Extensions.Hosting;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
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


            ISchedulerFactory? schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
            IScheduler? scheduler = await schedulerFactory.GetScheduler();

            // define the job and tie it to our HelloJob class
            IJobDetail? job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger? trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);


            // will block until the last running job completes
            await builder.RunAsync();
        }

    }


}