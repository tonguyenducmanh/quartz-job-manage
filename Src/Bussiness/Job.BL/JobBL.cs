using Job.DL;
using Job.Model;
using Job.Util;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Text;

namespace Job.BL
{
    /// <summary>
    /// class job bl
    /// </summary>
    public class JobBL
    {
        #region Constructor

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public JobBL() { }

        #endregion

        #region DL Create


        /// <summary>
        /// Đối tượng DL khai báo 1 lần
        /// </summary>
        private JobDL? _dlObject;

        /// <summary>
        /// đối tượng DL để query
        /// </summary>
        private JobDL DLObject
        {
            get
            {
                if (_dlObject == null)
                {
                    _dlObject = new JobDL();
                }
                return _dlObject;
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// lấy ra giá trị kích thước hiện tại của db
        /// </summary>
        public int GetCurrentSizeDB()
        {
            return DLObject.GetCurrentSizeDB();
        }

        private async Task<IScheduler?> GetScheduler()
        {
            IScheduler? scheduler = null;
            try
            {
                NameValueCollection props = JobUtility.QuartzConfig;
                if (props?.Count > 0)
                {
                    StdSchedulerFactory stdScheduler = new StdSchedulerFactory(props);
                    scheduler = await stdScheduler.GetScheduler();
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Đã có lỗi xảy ra khi load scheduler từ database {ex}");
            }
            return scheduler;
        }

        /// <summary>
        /// kiểm tra xem có scheduler nào không
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckHasScheduler()
        {
            bool hasScheduler = false;
            try
            {
                hasScheduler = await GetScheduler() != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return hasScheduler;
        }

        #region Stupid codes

        private JobTriggerConfigByType? GetJobTriggerConfig(int jobType)
        {
            JobTriggerConfigByType? result = null;
            switch (jobType)
            {
                case (int)JobEnum.ByeByeJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "ByeByeJob",
                        JobGroup = "GroupJobOne",
                        TriggerName = "ByeByeTrigger",
                        TriggerGroup = "GroupTriggerOne"
                    };
                    break;
                case (int)JobEnum.HaveANightDayJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "HaveANightDayJob",
                        JobGroup = "GroupJobOne",
                        TriggerName = "HaveANightDayTrigger",
                        TriggerGroup = "GroupTriggerOne"
                    };
                    break;
                case (int)JobEnum.HelloJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "HelloJob",
                        JobGroup = "GroupJobOne",
                        TriggerName = "HelloTrigger",
                        TriggerGroup = "GroupTriggerOne"
                    };
                    break;
                case (int)JobEnum.GoodAfternoonJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "GoodAfternoonJob",
                        JobGroup = "GroupJobTwo",
                        TriggerName = "GoodAfternoonTrigger",
                        TriggerGroup = "GroupTriggerTwo"
                    };
                    break;
                case (int)JobEnum.GoodEveningJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "GoodEveningJob",
                        JobGroup = "GroupJobTwo",
                        TriggerName = "GoodEveningTrigger",
                        TriggerGroup = "GroupTriggerTwo"
                    };
                    break;
                case (int)JobEnum.GoodMorningJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "GoodMorningJob",
                        JobGroup = "GroupJobTwo",
                        TriggerName = "GoodMorningTrigger",
                        TriggerGroup = "GroupTriggerTwo"
                    };
                    break;
                case (int)JobEnum.GoodNightJob:
                    result = new JobTriggerConfigByType()
                    {
                        JobName = "GoodNightJob",
                        JobGroup = "GroupJobTwo",
                        TriggerName = "GoodNightTrigger",
                        TriggerGroup = "GroupTriggerTwo"
                    };
                    break;
            }
            return result;
        }

        /// <summary>
        /// tạo ra job
        /// </summary>
        private JobBuilder GetJobBuilder(int jobType)
        {
            JobBuilder result = null;
            switch (jobType)
            {
                case (int)JobEnum.ByeByeJob:
                    result = JobBuilder.Create<ByeByeJob>();
                    break;
                case (int)JobEnum.HaveANightDayJob:
                    result = JobBuilder.Create<HaveANightDayJob>();
                    break;
                case (int)JobEnum.HelloJob:
                    result = JobBuilder.Create<HelloJob>();
                    break;
                case (int)JobEnum.GoodMorningJob:
                    result = JobBuilder.Create<GoodMorningJob>();
                    break;
                case (int)JobEnum.GoodEveningJob:
                    result = JobBuilder.Create<GoodEveningJob>();
                    break;
                case (int)JobEnum.GoodAfternoonJob:
                    result = JobBuilder.Create<GoodAfternoonJob>();
                    break;
                case (int)JobEnum.GoodNightJob:
                    result = JobBuilder.Create<GoodNightJob>();
                    break;
                default:
                    break;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// thêm job
        /// </summary>
        public async Task<bool> CreateJobByType(int jobType)
        {
            bool createSuccess = false;
            try
            {
                bool hasEnum = Enum.IsDefined(typeof(JobEnum), jobType);
                if (hasEnum)
                {
                    IScheduler? scheduler = await GetScheduler();
                    if (scheduler != null)
                    {
                        JobTriggerConfigByType? configJob = GetJobTriggerConfig(jobType);
                        if (configJob != null)
                        {
                            IJobDetail job = GetJobBuilder(jobType).WithIdentity(configJob.JobName, configJob.JobGroup).Build();
                            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity(configJob.TriggerName, configJob.TriggerGroup)
                                             .StartNow()
                                             .WithSimpleSchedule(x => x
                                             .WithIntervalInSeconds(JobUtility.ConfigGlobal.SleepBetweenTwoTime)
                                             .RepeatForever())
                                             .Build();

                            bool existedJob = await scheduler.CheckExists(job.Key);
                            if (existedJob)
                            {
                                createSuccess = false;
                            }
                            else
                            {
                                await scheduler.ScheduleJob(job, trigger);
                                createSuccess = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Đã có lỗi xảy ra khi thêm job {ex}");
            }
            return createSuccess;
        }


        /// <summary>
        /// xóa job
        /// </summary>
        public async Task<bool> DeleteIfExistsJob(int jobType)
        {
            bool deleteSuccess = false;
            try
            {
                bool hasEnum = Enum.IsDefined(typeof(JobEnum), jobType);
                if (hasEnum)
                {
                    IScheduler? scheduler = await GetScheduler();
                    if (scheduler != null)
                    {
                        JobTriggerConfigByType? configJob = GetJobTriggerConfig(jobType);
                        if (configJob != null)
                        {
                            TriggerKey triggerKey = new TriggerKey(configJob.TriggerName, configJob.TriggerGroup);
                            deleteSuccess = await scheduler.UnscheduleJob(triggerKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Đã có lỗi xảy ra khi xóa job {ex}");
            }
            return deleteSuccess;
        }

        /// <summary>
        /// tạm dừng 1 job theo key
        /// </summary>
        public async Task<bool> PauseJobByType(int jobType)
        {
            bool pauseSuccess = false;
            try
            {
                bool hasEnum = Enum.IsDefined(typeof(JobEnum), jobType);
                if (hasEnum)
                {
                    IScheduler? scheduler = await GetScheduler();
                    if (scheduler != null)
                    {
                        JobTriggerConfigByType? configJob = GetJobTriggerConfig(jobType);
                        if (configJob != null)
                        {
                            IJobDetail job = GetJobBuilder(jobType).WithIdentity(configJob.JobName, configJob.JobGroup).Build();
                            await scheduler.PauseJob(job.Key);
                            pauseSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Đã có lỗi xảy ra khi tạm dừng 1 job {ex}");
            }
            return pauseSuccess;
        }


        /// <summary>
        /// tiếp tục 1 job theo key
        /// </summary>
        public async Task<bool> ResumeJobByType(int jobType)
        {
            bool resumeSuccess = false;
            try
            {
                bool hasEnum = Enum.IsDefined(typeof(JobEnum), jobType);
                if (hasEnum)
                {
                    IScheduler? scheduler = await GetScheduler();
                    if (scheduler != null)
                    {
                        JobTriggerConfigByType? configJob = GetJobTriggerConfig(jobType);
                        if (configJob != null)
                        {
                            IJobDetail job = GetJobBuilder(jobType).WithIdentity(configJob.JobName, configJob.JobGroup).Build();
                            await scheduler.ResumeJob(job.Key);
                            resumeSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"Đã có lỗi xảy ra khi tiếp tục chạy 1 job đã tạm dừng {ex}");
            }
            return resumeSuccess;
        }

        #endregion
    }
}
