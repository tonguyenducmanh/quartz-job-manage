using Job.BL;

namespace Job.Test.BL
{
    /// <summary>
    /// Class test job DL
    /// </summary>
    [TestClass]
    public class JobBLTest
    {

        #region FakeObject

        private JobBLFake? _objectTest;

        private JobBLFake ObjectTest
        {
            get
            {
                if (_objectTest == null)
                {
                    _objectTest = new JobBLFake();
                }
                return _objectTest;
            }
        }

        #endregion

        #region TestMethods

        /// <summary>
        /// Kiểm tra xem có kết nối được với db thành công không bằng cách trả về size của db
        /// </summary>
        [TestMethod]
        public void GetCurrentSizeDB_returnCurrentSize()
        {
            int dbSize = ObjectTest.GetCurrentSizeDB();
            Assert.IsTrue(dbSize > 0);
        }

        /// <summary>
        /// kiểm tra xem có bảng lệnh quartz trong database không
        /// </summary>
        [TestMethod]
        public void GetSchedule_Success()
        {
            bool hasScheduler = ObjectTest.CheckHasScheduler().GetAwaiter().GetResult();
            Assert.IsTrue(hasScheduler);
        }

        #endregion
    }


    public class JobBLFake : JobBL
    {
        public JobBLFake() : base() { }
    }
}
