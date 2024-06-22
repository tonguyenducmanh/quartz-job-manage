using Job.DL;
using Job.Util;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

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
        private JobDL _dlObject;

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

        /// <summary>
        /// kiểm tra xem có scheduler nào không
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckHasScheduler()
        {
            bool hasScheduler = false;
            try
            {
                NameValueCollection props = JobUtility.GetQuartzConfig();
                if (props?.Count > 0)
                {
                    StdSchedulerFactory stdScheduler = new StdSchedulerFactory(props);
                    IScheduler? scheduler = await stdScheduler.GetScheduler();
                    hasScheduler = scheduler != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return hasScheduler;
        }

        #endregion
    }
}
