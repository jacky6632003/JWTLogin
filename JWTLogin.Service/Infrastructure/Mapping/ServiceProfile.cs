using AutoMapper;
using JWTLogin.Repository.DataModel;
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

        }
    }
}
