using JWTLogin.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<IEnumerable<UserDataModel>> GetUser();

        Task<IEnumerable<UserDataModel>> InsertUser();
    }
}
