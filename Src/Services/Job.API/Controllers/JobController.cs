using Job.BL;
using Microsoft.AspNetCore.Mvc;

namespace Job.API.Controllers
{
    /// <summary>
    /// Controller xử lý các lệnh thêm sửa xóa đọc danh sách các job cho worker chạy
    /// </summary>
    [ApiController]
    [Route("v1/job")]
    public class JobController : ControllerBase
    {

        private readonly ILogger<JobController> _logger;

        #region Contructor

        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Declare

        /// <summary>
        /// đối tượng BL xử lý nghiệp vụ về job chỉ khai báo 1 lần
        /// </summary>
        private JobBL? _blObject;

        /// <summary>
        /// Đối tượng BL xử lý nghiệp vụ về job
        /// </summary>
        public JobBL BLObject
        {
            get 
            {
                if(_blObject == null)
                {
                    _blObject = new JobBL();
                }
                return _blObject;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// test rằng controller này chạy được
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("Test job controller thành công");
        }


        /// <summary>
        /// Kiểm tra xem có scheduler nào không
        /// </summary>
        /// <returns></returns>
        [HttpGet("check_has_scheduler")]
        public IActionResult CheckHasJob() 
        {
            bool hasAnyJob = BLObject.CheckHasScheduler().GetAwaiter().GetResult();
            string resultText = hasAnyJob ? "Đã có scheduler trong database":"Không ó scheduler";
            return Ok(resultText);
        }

        /// <summary>
        /// thêm mới 1 job và 1 trigger đi kèm nếu chưa tồn tại
        /// </summary>
        /// <returns></returns>
        [HttpGet("create/{jobType}")]
        public async Task<IActionResult> CreateIfNotExistsJob(int jobType) 
        {
            bool createJobSuccess = await BLObject.CreateJobByType(jobType);
            string resultText = createJobSuccess ? "Tạo job thành công" : "Tạo job không thành công";
            return Ok(resultText);
        }

        /// <summary>
        /// xóa 1 job và 1 trigger đi kèm nếu tồn tại
        /// </summary>
        /// <returns></returns>
        [HttpGet("delete/{jobType}")]
        public async Task<IActionResult> DeleteIfExistsJob(int jobType)
        {
            bool deleteJobSuccess = await BLObject.DeleteIfExistsJob(jobType);
            string resultText = deleteJobSuccess ? "Xóa job thành công" : "Xóa không thành công";
            return Ok(resultText);
        }

        /// <summary>
        /// tạm ngưng 1 job
        /// </summary>
        /// <returns></returns>
        [HttpGet("pause/{jobType}")]
        public async Task<IActionResult> PauseJobByType(int jobType)
        {
            bool pauseJobSuccess = await BLObject.PauseJobByType(jobType);
            string resultText = pauseJobSuccess ? "Tạm ngưng 1 job thành công" : "Tạm ngưng 1 job không thành công";
            return Ok(resultText);
        }

        /// <summary>
        /// tiếp tục 1 job
        /// </summary>
        /// <returns></returns>
        [HttpGet("resume/{jobType}")]
        public async Task<IActionResult> ResumeJobByType(int jobType)
        {
            bool pauseJobSuccess = await BLObject.ResumeJobByType(jobType);
            string resultText = pauseJobSuccess ? "Tiếp tục chạy 1 job thành công" : "Tiếp tục chạy 1 job không thành công";
            return Ok(resultText);
        }
        #endregion
    }
}
