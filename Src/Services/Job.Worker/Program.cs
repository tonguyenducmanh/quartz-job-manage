using Quartz.Logging;
using Job.Worker.Log;

namespace Job.Worker
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
        }

    }

    
}