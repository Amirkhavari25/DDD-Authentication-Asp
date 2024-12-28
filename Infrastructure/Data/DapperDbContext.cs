using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DapperDbContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        
        public IDbConnection CreateConnection()
        {
        
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }

        
        public void Dispose()
        {
    
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close(); 
                _connection.Dispose();
            }
        }
    }
}
