﻿using BuySell.Business.Domain.Entities;
using BuySell.CommonModels.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.CommonModels.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInfoRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserInfo User
        {
            get
            {
                var user = new UserInfo()
                {
                    UserId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value ?? ""),
                };
                return user;
            }
        }

        
    } 
}

 
 
 