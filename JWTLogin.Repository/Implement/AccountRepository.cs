using Dapper;
using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.DB;
using JWTLogin.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace JWTLogin.Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public AccountRepository(IDatabaseHelper databaseHelper)
        {
            this._databaseHelper = databaseHelper;
        }
        public async Task<IEnumerable<UserDataModel>> GetUser()
        {
            using (SqlConnection conn = (_databaseHelper.GetConnection(this._databaseHelper.SQLConnectionString)) as SqlConnection)
            {
                var result = await conn.QueryAsync<UserDataModel>("");
                return result;
            }
        }

        public Task<IEnumerable<UserDataModel>> InsertUser()
        {
            throw new NotImplementedException();
        }
    }
}
