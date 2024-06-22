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
        private static string _sroucePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

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
        /// lấy ra cấu hình quartz
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetQuartzConfig()
        {
            NameValueCollection configQuartz = new NameValueCollection();
            if (ConfigGlobal != null)
            {
                configQuartz.Add("quartz.scheduler.instanceName", ConfigGlobal.InstanceName);
                configQuartz.Add("quartz.scheduler.instanceId",ConfigGlobal.InstanceId);
                configQuartz.Add("quartz.threadPool.type", ConfigGlobal.ThreadPoolType);
                configQuartz.Add("quartz.threadPool.threadCount", ConfigGlobal.ThreadCount);
                configQuartz.Add("quartz.jobStore.misfireThreshold", ConfigGlobal.MisfireThreshold);
                configQuartz.Add("quartz.jobStore.useProperties", ConfigGlobal.UseProperties);
                configQuartz.Add("quartz.jobStore.type", ConfigGlobal.JobStoreType);
                configQuartz.Add("quartz.jobStore.driverDelegateType", ConfigGlobal.DriverDelegateType);
                configQuartz.Add("quartz.jobStore.dataSource", "default");
                configQuartz.Add("quartz.jobStore.tablePrefix", ConfigGlobal.TablePrefix);
                configQuartz.Add("quartz.dataSource.default.connectionString", ConfigGlobal.PostgreDBCnn);
                configQuartz.Add("quartz.dataSource.default.provider", ConfigGlobal.QuartzProvider);
                configQuartz.Add("quartz.serializer.type", "json");
            }
            return configQuartz;
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
