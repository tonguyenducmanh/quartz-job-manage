using Job.Model;
using Job.Util;
using System.Collections.Specialized;

namespace Job.Test.Util
{
    /// <summary>
    /// Test các hàm util
    /// </summary>
    [TestClass]
    public class TestUtil
    {
        /// <summary>
        /// hàm kiểm thử cơ bản
        /// </summary>
        [TestMethod]
        public void TestConfig_ReturnConfig()
        {
            CenterConfig centerConfig = JobUtility.ConfigGlobal;
            Assert.IsTrue(centerConfig != null);
        }

        /// <summary>
        /// kiểm thử việc lấy ra cấu hình thư viện Quartz.Net
        /// </summary>
        [TestMethod]
        public void GetQuartzConfig_Success()
        {
            NameValueCollection quartzConfig = JobUtility.GetQuartzConfig();
            Assert.IsNotNull(quartzConfig);
        }
    }
}
