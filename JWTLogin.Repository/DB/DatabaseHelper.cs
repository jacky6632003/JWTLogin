using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JWTLogin.Repository.DB
{
    public class DatabaseHelper : IDatabaseHelper
    {
        public DatabaseHelper(string sQLConnectionString, string mySQLConnectionString)
        {
            this.SQLConnectionString = sQLConnectionString;

            this.MySQLConnectionString = mySQLConnectionString;
        }

        /// <summary>
        ///SQL連線字串
        /// </summary>
        public string SQLConnectionString { get; }

        /// <summary>
        /// MySQL
        /// </summary>
        public string MySQLConnectionString { get; }

 

        public IDbConnection GetConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            return conn;
        }

        public IDbConnection GetMySQLConnection(string connectionString)
        {
            var conn = new MySqlConnection(connectionString);

            return conn;
        }
    }
}
