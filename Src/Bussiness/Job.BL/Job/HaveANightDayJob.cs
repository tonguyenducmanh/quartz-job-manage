using Job.Util;
using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// 1 chiếc lời chúc cho ngày mới tốt lành class
    /// </summary>
    public class HaveANightDayJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"{JobUtility.GetDateTimeNow()} Lời nhắn gửi từ class {nameof(HaveANightDayJob)}: Have a night day !!!");
            Thread.Sleep(JobUtility.ConfigGlobal.SleepThreadAfterOneJob);
        }
    }
}
