using AutoMapper;
using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.Interface;
using JWTLogin.Service.Interface;
using JWTLogin.Service.ResultModel;
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

        public AccountService(IMapper mapper, IAccountRepository accountRepository)
        {
            this._mapper = mapper;
            this._accountRepository = accountRepository;
        }
        public async Task<IEnumerable<UserResultModel>> GetUser()
        {
            var data = await this._accountRepository.GetUser();

            var result = this._mapper.Map<IEnumerable<UserDataModel>, IEnumerable<UserResultModel>>(data);

            return result;
        }

        public Task<IEnumerable<UserResultModel>> InsertUser()
        {
            throw new NotImplementedException();
        }
    }
}
