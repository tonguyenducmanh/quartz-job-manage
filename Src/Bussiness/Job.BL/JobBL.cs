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
        private JobDL DLObject { 
            get 
            {
                if(_dlObject == null)
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

        /// <summary>
        /// lấy ra tên group dựa vào enum job
        /// </summary>
        private string GetGroupByJobType(int jobType)
        {
            string result = "";
            switch (jobType) 
            {
                case (int)JobEnum.ByeByeJob:
                    result = "groupbyebye";
                    break;
                case (int)JobEnum.HaveANightDayJob:
                    result = "grouphaveanightday";
                    break;
                case (int)JobEnum.HelloJob:
                    result = "grouphello";
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// lấy ra tên job dựa vào enum job
        /// </summary>
        private string GetJobNameByJobType(int jobType)
        {
            string result = "";
            switch (jobType)
            {
                case (int)JobEnum.ByeByeJob:
                    result = "jobbyebye";
                    break;
                case (int)JobEnum.HaveANightDayJob:
                    result = "jobhaveanightday";
                    break;
                case (int)JobEnum.HelloJob:
                    result = "jobhello";
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// tạo ra job
        /// </summary>
        private IJobDetail BuildJobDetail(int jobType)
        {
            IJobDetail result = null;
            switch (jobType)
            {
                case (int)JobEnum.ByeByeJob:
                    result = JobBuilder.Create<ByeByeJob>()
                                     .WithIdentity(GetJobNameByJobType(jobType), GetGroupByJobType(jobType))
                                     .Build();
                    break;
                case (int)JobEnum.HaveANightDayJob:
                    result = JobBuilder.Create<HaveANightDayJob>()
                                     .WithIdentity(GetJobNameByJobType(jobType), GetGroupByJobType(jobType))
                                     .Build();
                    break;
                case (int)JobEnum.HelloJob:
                    result = JobBuilder.Create<HelloJob>()
                                     .WithIdentity(GetJobNameByJobType(jobType), GetGroupByJobType(jobType))
                                     .Build();
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
                        IJobDetail job = BuildJobDetail(jobType);
                        ITrigger trigger = TriggerBuilder.Create()
                                         .WithIdentity(GetJobNameByJobType(jobType), GetGroupByJobType(jobType))
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
                        TriggerKey triggerKey = new TriggerKey(GetJobNameByJobType(jobType), GetGroupByJobType(jobType));
                        deleteSuccess = await scheduler.UnscheduleJob(triggerKey);
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
                        IJobDetail job = BuildJobDetail(jobType);
                        await scheduler.PauseJob(job.Key);
                        pauseSuccess = true;
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
                        IJobDetail job = BuildJobDetail(jobType);
                        await scheduler.ResumeJob(job.Key);
                        resumeSuccess = true;
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
