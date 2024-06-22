﻿using Quartz;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// Class test việc chạy job
    /// </summary>
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.OutputEncoding = Encoding.UTF8;
            await Console.Out.WriteLineAsync($"Lời nhắn gửi từ class {nameof(HelloJob)}: Xin chào !!!");
        }
    }
}
