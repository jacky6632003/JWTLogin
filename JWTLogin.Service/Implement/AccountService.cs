using AutoMapper;
using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.Interface;
using JWTLogin.Repository.Model.ConditionModel;
using JWTLogin.Service.Interface;
using JWTLogin.Service.Model;
using JWTLogin.Service.ResultModel;
using JWTLoginCommon.Config;
using JWTLoginCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTLogin.Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly JwtHelpers _jwtHelpers;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, JwtHelpers jwtHelpers)
        {
            this._mapper = mapper;
            this._accountRepository = accountRepository;
            this._jwtHelpers = jwtHelpers;
        }

        /// <summary>
        /// JWT
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="constantsConfig"></param>
        /// <returns></returns>
        public string GetJwt(string userName, ConstantsConfig constantsConfig)
        {
           var jwt= _jwtHelpers.GenerateToken(userName.Trim(), constantsConfig);
            return jwt;
        }

        /// <summary>
        /// getUser
        /// </summary>
        /// <param name="userInfoModel"></param>
        /// <returns></returns>
        public async Task<UserResultModel> GetUser(UserInfoModel userInfoModel, AesCryptoConfig aesCryptoConfig)
        {
            var cond = this._mapper.Map<UserInfoModel, UserConditionModel>(userInfoModel);
            cond.Password = AesCryptoHelper.Encrypt(aesCryptoConfig.AesKey, aesCryptoConfig.AesIv, userInfoModel.Password);
            var data = await this._accountRepository.GetUser(cond);

            var result = this._mapper.Map<UserDataModel,UserResultModel>(data);

            return result;
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="userInfoModel"></param>
        /// <param name="aesCryptoConfig"></param>
        /// <returns></returns>
        public async Task<int> InsertUser(UserInfoModel userInfoModel, AesCryptoConfig aesCryptoConfig)
        {
            var cond = this._mapper.Map<UserInfoModel, UserConditionModel>(userInfoModel);
            var userDate = await this._accountRepository.GetUserNoPassword(cond.Username);
            //已有Username 不能註冊
            if (userDate != null)
            {
                return 0;
            }
            cond.Password=AesCryptoHelper.Encrypt(aesCryptoConfig.AesKey, aesCryptoConfig.AesIv, userInfoModel.Password);
            var data = await this._accountRepository.InsertUser(cond);

            return data;
        }
    }
}
