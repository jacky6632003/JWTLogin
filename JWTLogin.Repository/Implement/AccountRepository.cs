using Dapper;
using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.DB;
using JWTLogin.Repository.Interface;
using JWTLogin.Repository.Model.ConditionModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;


namespace JWTLogin.Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public AccountRepository(IDatabaseHelper databaseHelper)
        {
            this._databaseHelper = databaseHelper;
        }
        public async Task<UserDataModel> GetUser(UserConditionModel userConditionModel)
        {
            using (SqlConnection conn = (_databaseHelper.GetConnection(this._databaseHelper.SQLConnectionString)) as SqlConnection)
            {
                var result = await conn.QueryAsync<UserDataModel>(GetUserSQL(),new
                {
                    Password = userConditionModel.Password,
                    Username = userConditionModel.Username
                }
                    );
                return result.ToList().FirstOrDefault();
            }
        }

        public async Task<int> InsertUser(UserConditionModel userConditionModel)
        {
            using (SqlConnection conn = (_databaseHelper.GetConnection(this._databaseHelper.SQLConnectionString)) as SqlConnection)
            {
                var result = await conn.ExecuteAsync(InsertUserSQL(), new
                {
                    Password = userConditionModel.Password,
                    Username = userConditionModel.Username
                }
                    );
                return result;
            }
        }

        private string GetUserSQL()
        {
            return $@"
SELECT [Username]
      ,[Password]
FROM [Account].[dbo].[User]
where [Username]=@Username and Password=@Password";
        }

        private string InsertUserSQL()
        {
            return $@"INSERT INTO [dbo].[User]
           ([Username]
           ,[Password])
     VALUES
           (@Username
           ,@Password)";
        }
    }
}
