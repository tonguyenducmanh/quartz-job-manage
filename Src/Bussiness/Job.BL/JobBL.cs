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
                NameValueCollection props = JobUtility.GetQuartzConfig();
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
                        IJobDetail job = JobBuilder.Create<HelloJob>()
                                     .WithIdentity("job1", "group1")
                                     .Build();
                        ITrigger trigger = TriggerBuilder.Create()
                                         .WithIdentity("trigger1", "group1")
                                         .StartNow()
                                         .WithSimpleSchedule(x => x
                                         .WithIntervalInSeconds(JobUtility.ConfigGlobal.SleepBetweenTwoTime)
                                         .RepeatForever())
                                         .Build();

                        // Tell Quartz to schedule the job using our trigger
                        await scheduler.ScheduleJob(job, trigger);
                        createSuccess = true;
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
                        JobKey jobKey = new JobKey("job1", "group1");
                        TriggerKey triggerKey = new TriggerKey("trigger1", "group1");
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

        #endregion
    }
}
