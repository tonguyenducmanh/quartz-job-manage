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
    }
}
