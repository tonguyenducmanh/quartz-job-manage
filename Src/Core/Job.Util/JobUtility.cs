using Job.Model;
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
        private static string _sroucePath = Environment.CurrentDirectory;

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
       
    }

}
