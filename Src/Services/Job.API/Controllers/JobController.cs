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
            return hasAnyJob ? Ok(hasAnyJob) : NotFound();
        }

        #endregion
    }
}
