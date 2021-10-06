using AutoMapper;
using JWTLogin.Repository.DataModel;
using JWTLogin.Repository.Model.ConditionModel;
using JWTLogin.Service.Model;
using JWTLogin.Service.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTLogin.Service.Infrastructure.Mapping
{
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProfile"/> class.
        /// </summary>
        public ServiceProfile()
        {
            this.CreateMap<UserDataModel, UserResultModel>();


            this.CreateMap<UserInfoModel, UserConditionModel>();

        }
    }
}
