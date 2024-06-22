using Quartz;

namespace Job.Worker
{
    /// <summary>
    /// Class test việc chạy job
    /// </summary>
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
