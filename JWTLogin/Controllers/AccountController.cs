using AutoMapper;
using JWTLogin.Models.ViewModel;
using JWTLogin.Service.Interface;
using JWTLogin.Service.Model;
using JWTLogin.Service.ResultModel;
using JWTLoginCommon.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ConstantsConfig _constantsConfig;
        private readonly AesCryptoConfig _aesCryptoConfig;

        public AccountController(ILogger<HomeController> logger, IMapper mapper, 
            IAccountService accountService, IOptions<ConstantsConfig> constantsConfig, IOptions<AesCryptoConfig> aesCryptoConfig)
        {
            _logger = logger;
            this._mapper = mapper;
            this._accountService = accountService;
            this._constantsConfig = constantsConfig.Value;
            this._aesCryptoConfig = aesCryptoConfig.Value;
        }
        [HttpGet]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserParamModel model)
        {
            if (ModelState.IsValid)
            {
                var info = this._mapper.Map<UserParamModel, UserInfoModel>(model);
                var data=await _accountService.InsertUser(info, _aesCryptoConfig);

                if (data == 1)
                {
                    ViewData["Signup"] = "註冊成功";
                }
                else
                {
                    ViewData["Signup"] = "註冊失敗";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParamModel model)
        {
            var info = this._mapper.Map<LoginParamModel, UserInfoModel>(model);
            var data= await this._accountService.GetUser(info, _aesCryptoConfig);
            var result = this._mapper.Map<UserResultModel, UserViewModel>(data);
            if (result != null)
            {
                var jwt=this._accountService.GetJwt(result.Username, _constantsConfig);
                return Redirect(@$"Home/Secret?access_token={jwt}");
            }
            else
            {
                ViewData["err"] = "帳號或密碼錯誤";
            }
           
            return View(data);
        }
    }
}
