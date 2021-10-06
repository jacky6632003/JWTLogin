using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.Model.ConditionModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<UserDataModel> GetUser(UserConditionModel userConditionModel);

        Task<int> InsertUser(UserConditionModel userConditionModel);
    }
}
