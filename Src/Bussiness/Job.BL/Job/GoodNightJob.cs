using Job.Util;
using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// class test job chúc ngủ ngon
    /// </summary>
    public class GoodNightJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"{JobUtility.GetDateTimeNow()} Lời nhắn gửi từ class {nameof(GoodNightJob)}: Chúc ngủ ngon !!!");
            Thread.Sleep(JobUtility.ConfigGlobal.SleepThreadAfterOneJob);
        }
    }
}
