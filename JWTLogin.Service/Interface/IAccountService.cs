using JWTLogin.Service.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin.Service.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<UserResultModel>> GetUser();

        Task<IEnumerable<UserResultModel>> InsertUser();
    }
}
