using AutoMapper;
using JWTLogin.Models.ViewModel;
using JWTLogin.Service.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Infrastructure.Mapping
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            this.CreateMap<UserResultModel, UserViewModel>();
        }
       
    }
}
