using Job.Util;
using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// class test job chào buổi sáng
    /// </summary>
    public class GoodMorningJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"{JobUtility.GetDateTimeNow()} Lời nhắn gửi từ class {nameof(GoodMorningJob)}: Chào buổi sáng !!!");
            Thread.Sleep(JobUtility.ConfigGlobal.SleepThreadAfterOneJob);
        }
    }
}
