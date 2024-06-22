using System.Data;
using Dapper;
using Job.Util;
using Npgsql;

namespace Job.DL
{
    /// <summary>
    /// class dl
    /// </summary>
    public class JobDL
    {
        #region Constructor
        
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public JobDL() { }

        #endregion

        #region Methods

        /// <summary>
        /// Khởi tạo kết nối tới PostgreSQL
        /// </summary>
        private IDbConnection CreateConnection()
        {
            string? connectionString = JobUtility.ConfigGlobal?.PostgreDBCnn;
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// lấy ra kích thước của db hiện tại
        /// </summary>
        /// <returns></returns>
        public int GetCurrentSizeDB()
        {
            using IDbConnection? cnn = CreateConnection();
            string sql = @"
                        SELECT 
                            pg_database_size(pd.datname)::numeric AS size 
                        FROM 
                            pg_database pd 
                        where 
                            pd.datname = current_database();";
            return cnn.ExecuteScalar<int>(sql);
        }

        #endregion
    }
}
