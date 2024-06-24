using Job.Util;
using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// class test job chào buổi tối
    /// </summary>
    public class GoodEveningJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"{JobUtility.GetDateTimeNow()} Lời nhắn gửi từ class {nameof(GoodEveningJob)}: Chào buổi tối !!!");
            Thread.Sleep(JobUtility.ConfigGlobal.SleepThreadAfterOneJob);
        }
    }
}
