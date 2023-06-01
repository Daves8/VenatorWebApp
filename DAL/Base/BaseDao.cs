using System.Data.SqlClient;

namespace VenatorWebApp.DAL.Base
{
    public class BaseDao
    {
        protected readonly IConfiguration _config;
        protected readonly string _connectionString;

        public BaseDao(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}
