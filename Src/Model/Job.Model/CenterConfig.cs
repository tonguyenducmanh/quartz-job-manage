namespace Job.Model
{
    /// <summary>
    /// Cấu hình dùng chung cho toàn bộ các project
    /// </summary>
    public class CenterConfig
    {
        /// <summary>
        /// connection string kết nối tới database PostgreSQL
        /// </summary>
        public string? PostgreDBCnn { get; set; }

        /// <summary>
        /// cấu hình tên jobStore
        /// </summary>
        public string? JobStoreType { get; set; }

        /// <summary>
        /// tên loại driver ứng với từng loại Database SQL
        /// </summary>
        public string? DriverDelegateType { get; set; }

        /// <summary>
        /// Tiền tố tên bảng trong database
        /// </summary>
        public string? TablePrefix { get; set; }

        public string? InstanceName { get; set; }

        public string? InstanceId { get; set; }

        public string? ThreadPoolType { get; set; }

        public string? ThreadCount { get; set; }

        public string? MisfireThreshold { get; set; }

        public string? UseProperties { get; set; }

        public string? QuartzProvider { get; set; }
    }
}
