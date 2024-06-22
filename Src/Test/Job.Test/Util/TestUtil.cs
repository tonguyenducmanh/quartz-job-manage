using Job.Model;
using Job.Util;

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
    }
}
