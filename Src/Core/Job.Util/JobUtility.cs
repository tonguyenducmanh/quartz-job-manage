using Job.Model;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.Json;

namespace Job.Util
{
    /// <summary>
    /// Class chứa các hàm utility
    /// </summary>
    public static class JobUtility
    {
        /// <summary>
        /// root path của container
        /// </summary>
        private static string _sroucePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

        /// <summary>
        /// biến lưu trữ cấu hình
        /// </summary>
        private static CenterConfig? _centerConfig;

        /// <summary>
        /// Cấu hình cho toàn bộ solution
        /// </summary>
        public static CenterConfig ConfigGlobal { 
            get 
            {
                if (_centerConfig == null) 
                {
                    _centerConfig = ReadConfig();
                }
                return _centerConfig;
            } 
        }
        
        /// <summary>
        /// cấu hình chạy thư viện quartz
        /// </summary>
        private static NameValueCollection? _quartzConfig;

        /// <summary>
        /// cấu hình chạy thư viện quartz
        /// </summary>
        public static NameValueCollection QuartzConfig
        {
            get
            {
                if(_quartzConfig == null)
                {
                    _quartzConfig = GetQuartzConfig();
                }
                return _quartzConfig;
            }
        }

        /// <summary>
        /// lấy ra cấu hình quartz
        /// </summary>
        /// <returns></returns>
        private static NameValueCollection GetQuartzConfig()
        {
            Dictionary<string, string> configQuartz = ConfigGlobal?.ConfigQuartz ?? new Dictionary<string, string>();
            NameValueCollection result = new NameValueCollection();
            if (configQuartz != null)
            {
                result = new NameValueCollection(configQuartz.Count);
                foreach (var k in configQuartz)
                {
                    result.Add(k.Key, k.Value);
                }
            }
            return result;
        }

        /// <summary>
        /// đọc ra config ứng dụng
        /// </summary>
        /// <returns></returns>
        private static CenterConfig ReadConfig()
        {

            string appSettingPath = Path.Combine(_sroucePath, "Config", "appsettings.json");
            using StreamReader reader = new(appSettingPath);
            var json = reader.ReadToEnd();
            CenterConfig? config = JsonSerializer.Deserialize<CenterConfig>(json);
            return config ?? new CenterConfig();
        }
       
        /// <summary>
        /// Lấy ra thời điểm hiện tại
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeNow()
        {
            return DateTime.Now.ToString("H:mm:ss");
        }
    }

}
