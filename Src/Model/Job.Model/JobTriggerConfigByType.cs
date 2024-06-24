namespace Job.Model
{
    /// <summary>
    /// cấu hình job và trigger theo type
    /// </summary>
    public class JobTriggerConfigByType
    {
        /// <summary>
        /// tên job
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// group job
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// tên trigger
        /// </summary>
        public string TriggerName { get; set; }

        /// <summary>
        /// group trigger
        /// </summary>
        public string TriggerGroup { get; set; }
    }
}
