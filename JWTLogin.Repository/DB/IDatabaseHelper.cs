using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JWTLogin.Repository.DB
{
    public interface IDatabaseHelper
    {
        /// <summary>
        /// SQL連線字串
        /// </summary>
        string SQLConnectionString { get; }

        /// <summary>
        /// MySQL
        /// </summary>
        string MySQLConnectionString { get; }

        IDbConnection GetConnection(string connectionString);

        IDbConnection GetMySQLConnection(string connectionString);
    }
}
