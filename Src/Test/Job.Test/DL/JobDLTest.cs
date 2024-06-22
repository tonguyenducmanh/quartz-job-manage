using Job.DL;

namespace Job.Test.DL
{
    /// <summary>
    /// Class test job DL
    /// </summary>
    [TestClass]
    public class JobDLTest
    {

        #region FakeObject

        private JobDLFake? _objectTest;

        private JobDLFake ObjectTest
        {
            get
            {
                if(_objectTest == null)
                {
                    _objectTest = new JobDLFake();
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

        #endregion
    }

    public class JobDLFake : JobDL
    {
        public JobDLFake() : base() { }
    }
}
