using JWTLogin.Service.Model;
using JWTLogin.Service.ResultModel;
using JWTLoginCommon.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin.Service.Interface
{
    public interface IAccountService
    {
        Task<UserResultModel> GetUser(UserInfoModel userInfoModel, AesCryptoConfig aesCryptoConfig);

        Task<int> InsertUser(UserInfoModel userInfoModel, AesCryptoConfig aesCryptoConfig);

        string GetJwt(string userName, ConstantsConfig constantsConfig);
    }
}
