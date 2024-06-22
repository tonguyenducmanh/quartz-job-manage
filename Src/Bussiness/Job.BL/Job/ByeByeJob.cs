using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// class test job tạm biệt
    /// </summary>
    public class ByeByeJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"Lời nhắn gửi từ class {nameof(ByeByeJob)}: Tạm biệt !!!");
        }
    }
}
