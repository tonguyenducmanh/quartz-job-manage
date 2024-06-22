using System.Collections.Specialized;

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
        /// cấu hình cho thư viện quartz đọc job trong database
        /// </summary>
        public Dictionary<string, string> ConfigQuartz { get; set; }
    }
}
